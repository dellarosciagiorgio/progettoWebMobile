using Application.Abstraction.Services;
using Application.Factories;
using Application.Mapper;
using Application.Models.Requests;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DetailedEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _loginService.LoginAsync(request);
            var ut = await _loginService.GetUserInformation(user);
            var token = GenerateJwtToken(ut);

            return Ok(
                ResponseFactory
                .WithSuccess(token)
             );
        }
        private string GenerateJwtToken(UserGenerico user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Email", user.User.Email));
            claims.Add(new Claim("Ruolo", user.User.Ruolo.ToString()));
            claims.Add(new Claim("sub", user.User.IdUser.ToString()));
            //mettere altre claims

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }
    }
}
