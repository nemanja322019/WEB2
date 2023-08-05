using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication.Enums;
using WebApplication.Interfaces;

namespace WebApplication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfigurationSection _secretKey;
        public TokenService(IConfiguration config)
        {
            _secretKey = config.GetSection("SecretKey");
        }
        public string CreateToken(int id, string username, UserTypes userType)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, userType.ToString().ToLower()));
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim("id", id.ToString()));

            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value)),
                SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:7123",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20), 
                    signingCredentials: signinCredentials
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
