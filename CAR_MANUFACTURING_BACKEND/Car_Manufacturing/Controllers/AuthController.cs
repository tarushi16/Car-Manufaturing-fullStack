using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Car_Manufacturing.Models.Authentication;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Login endpoint
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Find the user by username
           
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);


            // Check if user exists and if the password matches
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Prepare the response with the token and user details
            var authResponse = new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };

            return Ok(authResponse);
        }

        // Register endpoint (using RegisterRequest)
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            //Check if the user already exists
           var existingUser = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Username is already taken." });
            }

            // Hash the password before storing it
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create a new user and save it to the database
            var user = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Role = request.Role,
                Email = request.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Prepare the response with the token and user details
            var authResponse = new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };

            return Ok(authResponse);
        }

        // Method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            // Create claims based on user data
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),  // Username
                new Claim(ClaimTypes.Role, user.Role),      // Role
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) // User ID
            };

            // Secret key for JWT signing and validation (should be in appsettings.json)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate JWT token with the claims and expiration time
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Set expiration time (e.g., 1 hour)
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Return the token as string
        }
    }
}
