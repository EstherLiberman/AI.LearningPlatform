using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI.LearningPlatform.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Name) || string.IsNullOrWhiteSpace(newUser.Phone))
                return BadRequest("שם וטלפון הם שדות חובה.");

            await _userService.AddUserAsync(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

    }
}
