using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services
            , IConfiguration config)
        {


            services.AddEndpointsApiExplorer();
            services.AddControllersWithViews()
                /*
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = (context) =>
                    {
                        //return new BadRequestResultFactory(context);
                    };
                })
                */
                ;

            //Se vogliamo usare il Jwt
            /*
            IdentityModelEventSource.ShowPII = true;
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    
                });

            */

            services.AddAuthorization( 
                /*opt =>
            {
                opt.AddPolicy("IS_ADM", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("MY_ROLE", ["ADMIN"]);

                });
            }*/
                );

            //services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
