using BookStore.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Services
{
    public class UserService
    {

        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey key;
        private readonly string issuer;


        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
            var keyValue = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyValue))
            {
                throw new ArgumentNullException(nameof(keyValue), "JWT key is missing in configuration");
            }

            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
            issuer = configuration["Jwt:Issuer"];
            if (string.IsNullOrEmpty(issuer))
            {
                throw new ArgumentNullException(nameof(issuer), "JWT issuer is missing in configuration");
            }
        }

        public string GenerateJwtToken(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty");
            }

            var expirationMinutes = configuration["Jwt:ExpirationMinutes"];
            if (string.IsNullOrEmpty(expirationMinutes))
            {
                throw new ArgumentNullException(nameof(expirationMinutes), "JWT expiration minutes is missing in configuration");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expirationMinutes)),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


        public async Task<bool> ValidateTokenAsync(string token, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out var validatedToken);

                return principal.Identity.Name == username && validatedToken is JwtSecurityToken;
            }
            catch
            {
                return false;
            }
        }

    }
}

