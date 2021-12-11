using eCommerce_Customer_Rating_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eCommerce_Customer_Rating_API.Controllers
{
    //[Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly HubtelApplicationDbContext _context;

        public TokenController(IConfiguration configuration, HubtelApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Token")]
        public async Task<IActionResult> Post(PhoneNumber telephone)
        {
            if (telephone.phoneNumber != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.MobilePhone, telephone.phoneNumber.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Subject"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: signIn);

                var expiration = DateTime.UtcNow.AddHours(2);

                var clientToken = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiresAt = expiration

                };

                return Ok(clientToken);

               
                
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        //[HttpGet]
        private async Task<Auth> GetUser(string username, string password)
        {
            return await _context.Auth!.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }


    }
}
