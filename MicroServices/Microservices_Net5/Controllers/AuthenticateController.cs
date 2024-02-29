using Microservices_Net5.Helper;
using Microservices_Net5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microservices_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("generateToken")]
        public async Task<IActionResult> generateToken([FromBody] LoginModel user)
        {
            var token = JwtBearerConfiguration.GenerateToken(_configuration);
            return Ok(new
            {
                token = token
            });

            //var issuer = _configuration["JWT:ValidIssuer"];
            //var audience = _configuration["JWT:ValidAudience"];
            //var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[]
            //    {
            //    new Claim("Id", Guid.NewGuid().ToString()),
            //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
            //    new Claim("Roles","user roles listing here"),
            //    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            // }),
            //    Expires = DateTime.UtcNow.AddMinutes(15),
            //    Issuer = issuer,
            //    Audience = audience,
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
            //};
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var stringToken = tokenHandler.WriteToken(token);
            //return Ok(new
            //{
            //    token = stringToken
            //});
        }
    }
}
