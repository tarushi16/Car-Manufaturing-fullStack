using Car_Manufacturing.Models;
using Car_Manufacturing.Models.Authentication;
using System.Threading.Tasks;

namespace Car_Manufacturing.Repositories.Authentication
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(RegisterRequest request);
        Task<User> GetUserByIdAsync(int userId); // Correct method name
    }
}
