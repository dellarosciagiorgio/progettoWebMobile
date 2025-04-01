using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using Web.Factories;
using Application.Abstraction.Services;
using Web.Models;
using System.Text.Json;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services
            , IConfiguration config)
        {
            AllowedIpsConfig allowedIpsConfig;
            //ATTIVAZIONE CORS verso il frontend
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                allowedIpsConfig = config.GetSection("AllowedIpsConfig").Get<AllowedIpsConfig>();
            }
            else
            {
                // Legge gli IP consentiti dal file di configurazione
                var allowedIpsJson = config["AllowedIpsConfig:AllowedIps"];
                var allowedIps = string.IsNullOrEmpty(allowedIpsJson)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(allowedIpsJson);
                
                allowedIpsConfig = new AllowedIpsConfig()
                {
                    AllowedIps = allowedIps
                };
            }
            if (allowedIpsConfig == null)
            {
                throw new Exception("AllowedIpsConfig non configurato");
            }
            // Configura CORS con gli IP letti dal file di configurazione
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCORS", policy =>
                {
                    policy.WithOrigins(allowedIpsConfig.AllowedIps.ToArray())
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });



            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = "Ruolo"
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = async (context) =>
                        {
                            var identity = context.Principal.Identity as ClaimsIdentity;
                            var emailClaim = identity.FindFirst(ClaimTypes.Email);
                            //var emailClaim = identity.Claims.Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
                            if (emailClaim != null)
                            {
                                string userEmail = emailClaim.Value;
                                var utenteService = context.HttpContext.RequestServices.GetRequiredService<ILoginService>();
                                var utente = await utenteService.GetUtenteByEmailAsync(userEmail);
                                var userInfo = await utenteService.GetUserInformation(utente);
                                identity.AddClaim(new Claim("Ruolo", utente.Ruolo.ToString()));
                                identity.AddClaim(new Claim("sub", userInfo.Id.ToString()));
                            }
                        }
                    };
                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IS_ADM", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Ruolo", ["Admin"]);

                });
                opt.AddPolicy("IS_ACQ", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Ruolo", ["Acquirente"]);
                });
                opt.AddPolicy("IS_ORG", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Ruolo", ["Organizzatore"]);

                });                
            });
            services.AddEndpointsApiExplorer();

            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = (context) =>
                    {
                        return new BadRequestResultFactory(context);
                    };
                });
            return services;
        }
    }
}
