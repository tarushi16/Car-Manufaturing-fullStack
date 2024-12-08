using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Method to generate JWT token
        public async Task<string> GenerateTokenAsync(string userId, string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),  // User ID claim
                new Claim(ClaimTypes.Name, userName)           // User name claim
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));  // Secret key to sign the token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  // Signing credentials

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),  // Token expiration time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);  // Return the JWT token as a string
        }

        // Method to validate JWT token
        public ClaimsPrincipal ValidateToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));  // Secret key to validate the token

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],  // Issuer
                    ValidAudience = _configuration["Jwt:Audience"],  // Audience
                    IssuerSigningKey = key  // Secret key to verify the signature
                }, out SecurityToken validatedToken);

                return principal;  // Return the claims principal if token is valid
            }
            catch
            {
                return null;  // Return null if the token is invalid
            }
        }
    }
}
