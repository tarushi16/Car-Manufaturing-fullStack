using Car_Manufacturing.Models;
using Car_Manufacturing.Models.Authentication;
using Car_Manufacturing.Repositories.Authentication;
using BCrypt.Net;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Car_Manufacturing.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthRepository _authRepository;

        public AuthenticationService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // Register a new user and return an AuthResponse with a token
        public async Task<AuthResponse> RegisterUserAsync(RegisterRequest model)
        {
            var existingUser = await _authRepository.GetUserByUsernameAsync(model.Username);
            if (existingUser != null)
                throw new Exception("User already exists");

            // Add user via repository (password hashing should be handled properly)
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = passwordHash;

            var user = await _authRepository.AddUserAsync(model);

            var token = await GenerateJwtTokenAsync(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }

        // Authenticate the user and return an AuthResponse with a token
        public async Task<AuthResponse> AuthenticateUserAsync(LoginRequest model)
        {
            var user = await _authRepository.GetUserByUsernameAsync(model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new Exception("Invalid username or password");

            var token = await GenerateJwtTokenAsync(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }

        // Check if the user exists by username
        public async Task<bool> IsUserExistsAsync(string username)
        {
            var user = await _authRepository.GetUserByUsernameAsync(username);
            return user != null; // Return true if the user exists, false otherwise
        }

        // Get user details by user ID
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _authRepository.GetUserByIdAsync(userId); // Correct method name
        }


        // Generate JWT token for the user
        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
