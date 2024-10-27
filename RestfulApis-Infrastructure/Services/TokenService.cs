using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestfulApis_Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public string GenerateUserToken(User user)
        {
            var tkey = Encoding.UTF8.GetBytes(_configuration["JWTToken:Key"]!);
            var issuer = _configuration["JWTToken:Issuer"];
            var audience = _configuration["JWTToken:Audience"];
            var expirationMinutes = int.TryParse(_configuration["JWTToken:ExpirationMinutes"], out int expMinutes) ? expMinutes : 60;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tkey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
