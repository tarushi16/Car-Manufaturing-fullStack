using Car_Manufacturing.Models;
using Car_Manufacturing.Models.Authentication;
using System.Threading.Tasks;

namespace Car_Manufacturing.Services.Authentication
{
    public interface IAuthenticationService
    {
        // Register a new user and return an AuthResponse with a token
        Task<AuthResponse> RegisterUserAsync(RegisterRequest model);

        // Authenticate the user and return an AuthResponse with a token
        Task<AuthResponse> AuthenticateUserAsync(LoginRequest model);

        // Check if the user exists by username
        Task<bool> IsUserExistsAsync(string username);

        // Get user details by user ID
        Task<User> GetUserByIdAsync(int userId);

        // Generate JWT token for the user
        Task<string> GenerateJwtTokenAsync(User user);
    }
}
