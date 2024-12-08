using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Car_Manufacturing.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories.Authentication
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to find a user by username
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        // Method to find a user by user ID
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // Check if the user exists by username
        public async Task<bool> IsUserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        // Method to add a new user to the database
        public async Task<User> AddUserAsync(RegisterRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.Password, // Password should be hashed before adding it
                Role = request.Role,
                Email = request.Email
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
