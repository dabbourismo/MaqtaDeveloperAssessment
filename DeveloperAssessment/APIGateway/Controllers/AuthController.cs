using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.CustomModels;
using Models.DBModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGateway.Controllers
{

    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("Invalid client request");
            }
            var cred = _config.GetSection("Credintals");
            if (loginUser.Username == cred["username"] && loginUser.Password == cred["password"])
            {
                var jwtKey = _config.GetSection("keys");
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey["jwtKey"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: jwtKey["ValidIssuer"],
                    audience: jwtKey["ValidAudience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(Convert.ToDouble(jwtKey["Expires"])),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }

    }
}
