using API_Authentication.Dtos;
using API_Authentication.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly Jwtsettings _jwtsettings;

        public LoginController (IConfiguration configuration, Jwtsettings jwtsettings)
        {
            _configuration = configuration;
            _jwtsettings = jwtsettings;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult LoginUser(LoginDto input)
        {
            try
            {
                var user = checkUser(input);
                if (user != null)
                {
                    var token = Createtokens(user);
                    return Ok(token);
                }
                return Ok("Không tìm thấy user");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private User checkUser(LoginDto userLogin)
        {
            var currentUser = DataDto.Users.FirstOrDefault(o => o.Email.ToLower() == userLogin.Email.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

        private string Createtokens(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtsettings.Issuer,
                audience: _jwtsettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtsettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
