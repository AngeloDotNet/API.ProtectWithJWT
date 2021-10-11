using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Protect_With_JWT.Models.InputModels;
using API_Protect_With_JWT.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SequentialGuid;

namespace API_Protect_With_JWT.Controllers
{
    [ApiController]  
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration configuration;  
        private readonly IOptionsMonitor<JwtOptions> jwtOptionsMonitor;

        public AuthenticateController(IConfiguration configuration, IOptionsMonitor<JwtOptions> jwtOptionsMonitor)  
        {  
            this.configuration = configuration;
            this.jwtOptionsMonitor = jwtOptionsMonitor;  
        } 

        [HttpPost]  
        [Route("login")]  
        public IActionResult Login([FromBody] LoginModel model)  
        {  
            var options = this.jwtOptionsMonitor.CurrentValue;

            if (model.Username == "admin" && model.Password == "Password123")  
            {  
                var userRoles = "Admin";  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, model.Username),  
                    new Claim(JwtRegisteredClaimNames.Jti, SequentialGuidGenerator.Instance.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim("Ruolo", userRoles));
                }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret));  
                var token = new JwtSecurityToken(  
                    issuer: options.ValidIssuer,  
                    audience: options.ValidAudience,
                    expires: DateTime.Now.AddDays(options.Expires),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );  
  
                return Ok(new 
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }  

            return Unauthorized();  
        }  
    }
}