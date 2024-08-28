using Microservices_Net5.Helper;
using Microservices_Net5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Microservices_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate token base on user login
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The token will be reponed and it have timeout</returns>
        [HttpPost]
        [Route("generateToken")]
        public IActionResult generateToken([FromBody] LoginModel user)
        {
            var token = JwtBearerConfiguration.GenerateToken(_configuration);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Example to using Token to get user information base on token
        /// </summary>
        /// <returns>User inforamtion</returns>
        [Authorize]
        [HttpGet("profile")]
        public IActionResult UserProfile()
        {
            // Retrieve user information based on the authenticated user's claims
            var userId = User.Identity.Name;
            // Return user profile information
            return Ok(new { UserId = userId, Message = "This is the user's profile" });
        }

    }
}
