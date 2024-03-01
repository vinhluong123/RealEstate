using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices_Net5.Helper
{
    public static class JwtBearerConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        // Extract the token from the request headers
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                        // Validate the token
                        var principal = ValidateToken(token, configuration["Jwt:Secret"]);

                        if (principal != null)
                        {
                            // If the token is valid, set the principal in the context
                            context.Principal = principal;
                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        // This event occurs after the token has been successfully validated
                        // You can perform additional checks or add custom claims to the user's identity

                        // For example, you might want to add custom claims based on user roles or permissions
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        identity.AddClaim(new Claim("custom_claim", "some_value"));

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        // This event occurs when the authentication process fails due to an invalid token,
                        // expired token, or any other authentication-related errors

                        var errorMessage = context.Exception.Message;
                        // Log the authentication failure
                        //logger.LogError("Authentication failed: " + context.Exception.Message);

                        return Task.CompletedTask;
                    }

                };
            });
        }


        public static ClaimsPrincipal ValidateToken(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch (Exception ex)
            {
                return null; // Token validation failed
            }
        }

        /// <summary>
        /// Generate token

        /// </summary>
        /// <param name="_configuration"></param>
        /// <returns></returns>
        public static string GenerateToken(this IConfiguration configuration)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "lhvinh"),
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim("Name", "lhvinh"),
                    new Claim("Roles","user roles listing here"),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }


      
    }
}
