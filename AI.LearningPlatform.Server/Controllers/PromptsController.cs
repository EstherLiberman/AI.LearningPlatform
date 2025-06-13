using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI.LearningPlatform.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptsController : ControllerBase
    {
        private readonly PromptService _promptService;

        public PromptsController(PromptService promptService)
        {
            _promptService = promptService;
        }

        // GET: api/Prompts
        [HttpGet]
        public async Task<ActionResult<List<Prompt>>> Get()
        {
            var prompts = await _promptService.GetAllPromptsAsync();
            return Ok(prompts);
        }

        // GET: api/Prompts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Prompt>> GetById(string id)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
                return NotFound();

            return Ok(prompt);
        }

        // POST: api/Prompts
        //[HttpPost]
        //public async Task<ActionResult<Prompt>> Post(Prompt prompt)
        //{
        //    await _promptService.AddPromptAsync(prompt);

        //    // מחזיר תשובת 201 עם מיקום האובייקט החדש
        //    return CreatedAtAction(nameof(GetById), new { id = prompt.Id }, prompt);
        //}
        [HttpPost]
        public async Task<ActionResult<Prompt>> Post(Prompt prompt)
        {
            await _promptService.AddPromptAsync(prompt);

            var aiResponse = await _promptService.GetAiResponseForPromptAsync(prompt.Id);

            prompt.GeneratedContent = aiResponse;

            await _promptService.UpdatePromptAsync(prompt);

            return CreatedAtAction(nameof(GetById), new { id = prompt.Id }, prompt);
        }

        // GET: api/Prompts/{id}/generate
        [HttpGet("{id}/generate")]
        public async Task<ActionResult<string>> GenerateContent(string id)
        {
            var result = await _promptService.GetAiResponseForPromptAsync(id);
            return Ok(result);
        }

        // GET: api/Prompts/by-user?name=...&phone=...
        [HttpGet("by-user")]
        public async Task<ActionResult<List<Prompt>>> GetByUserNameAndPhone([FromQuery] string name, [FromQuery] string phone)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
                return BadRequest("יש לספק גם שם וגם טלפון.");

            var prompts = await _promptService.GetPromptsByUserNameAndPhoneAsync(name, phone);

            if (prompts == null || !prompts.Any())
                return NotFound("לא נמצאו פרומפטים עבור המשתמש.");

            return Ok(prompts);
        }



    }
}


