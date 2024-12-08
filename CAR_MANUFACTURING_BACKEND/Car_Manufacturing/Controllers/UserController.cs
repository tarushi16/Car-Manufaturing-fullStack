using Car_Manufacturing.Models;
using Car_Manufacturing.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }
            return Ok(user);
        }

        // GET: api/user/username/{username}
        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = $"User with username '{username}' not found." });
            }
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, createdUser);
        }

        // PUT: api/user/{userId}
        [HttpPut("{userId}")]
        public async Task<ActionResult<User>> UpdateUser(int userId, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.UpdateUserAsync(userId, updatedUser);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }
            return Ok(user);
        }

        // DELETE: api/user/{userId}
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var success = await _userService.DeleteUserAsync(userId);
            if (!success)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }
            return NoContent();
        }

        // PATCH: api/user/deactivate/{userId}
        [HttpPatch("deactivate/{userId}")]
        public async Task<ActionResult> DeactivateUser(int userId)
        {
            var success = await _userService.DeactivateUserAsync(userId);
            if (!success)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }
            return NoContent();
        }
    }
}
