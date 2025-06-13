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

        [HttpGet("search")]
        public async Task<ActionResult<User>> GetByNameAndPhone([FromQuery] string name, [FromQuery] string phone)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
                return BadRequest("יש לספק גם שם וגם טלפון.");

            var user = await _userService.GetUserByNameAndPhoneAsync(name, phone);

            if (user == null)
                return NotFound("לא נמצא משתמש עם הפרטים שסופקו.");

            return Ok(user);
        }

    }
}
