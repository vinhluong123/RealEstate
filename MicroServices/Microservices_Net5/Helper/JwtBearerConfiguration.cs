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
        public static void AddJwtBearerConfiguration(IServiceCollection services, IConfiguration configuration)
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
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                        context.Token = token ;
                        var isvalidate = ValidateToken(token, configuration["Jwt:Secret"]);
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
            catch (Exception)
            {
                return null; // Token validation failed
            }
        }


        #region TempCode
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
                    new Claim("Roles","user roles listing here"),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ////Create Security Token object by giving required parameters
            //var secToken = new JwtSecurityToken(
            //    signingCredentials: credentials,
            //    issuer: _configuration["JWT:ValidIssuer"],
            //    audience: _configuration["JWT:ValidAudience"],
            //    claims: new[]
            //    {
            //        new Claim(JwtRegisteredClaimNames.Sub, "lhvinh"),
            //        new Claim("Id", Guid.NewGuid().ToString()),
            //        new Claim("Roles","user roles listing here"),
            //        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            //    },
            //    expires: DateTime.UtcNow.AddMinutes(15)
            //    );

            //var token = new JwtSecurityTokenHandler().WriteToken(secToken);
            //return token;
        }
        public static void SetupJWTServices(IServiceCollection services)
        {
            string key = "my_secret_key_12345"; //this should be same which is used while creating token      
            var issuer = "http://mysite.com";  //this should be same which is used while creating token  

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.RequireHttpsMetadata = false;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = issuer,
                  ValidAudience = issuer,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
              };

              options.Events = new JwtBearerEvents
              {
                  //OnAuthenticationFailed = context =>
                  //{
                  //    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                  //    {
                  //        context.Response.Headers.Add("Token-Expired", "true");
                  //    }
                  //    return Task.CompletedTask;
                  //},
                  OnChallenge = context =>
                  {
                      context.HandleResponse();
                      context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                      context.Response.ContentType = "application/json";

                      // Ensure we always have an error and error description.
                      if (string.IsNullOrEmpty(context.Error))
                          context.Error = "invalid_token";
                      if (string.IsNullOrEmpty(context.ErrorDescription))
                          context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                      // Add some extra context for expired tokens.
                      if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                      {
                          var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                          context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                          context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";
                      }

                      return context.Response.WriteAsync(JsonSerializer.Serialize(new
                      {
                          error = context.Error,
                          error_description = context.ErrorDescription
                      }));
                  }
              };
          });
        }
        private static bool ValidateToken1(string token, string issuer, string audience, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(issuer, audience, secret);
            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return true;
        }

        private static TokenValidationParameters GetValidationParameters(string issuer, string audience, string secret)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)) // The same key as the one that generate the token
            };
        }
        #endregion


    }
}
