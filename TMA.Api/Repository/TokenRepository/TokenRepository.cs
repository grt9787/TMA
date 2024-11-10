using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TMA.Api.Model;

namespace TMA.Api.Repository
{
    public class TokenRepository: ITokenRepository
{
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponse GenerateToken(User user, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiry = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expiry,
                Claims = claims
            };
        }

    }
}
