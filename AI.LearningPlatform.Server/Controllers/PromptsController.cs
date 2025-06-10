using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.Server.Controllers;
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

        [HttpGet]
        public async Task<ActionResult<List<Prompt>>> Get()
        {
            var prompts = await _promptService.GetAllPromptsAsync();
            return Ok(prompts);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Prompt prompt)
        {
            await _promptService.AddPromptAsync(prompt);
            return Ok();
        }

  
            //private readonly OpenAiService _openAiService;

            //public PromptsController(OpenAiService openAiService)
            //{
            //    _openAiService = openAiService;
            //}

            //[HttpPost("generate")]
            //public async Task<IActionResult> GenerateLesson([FromBody] LessonRequest request)
            //{
            //    var result = await _openAiService.GetLessonLikeResponseAsync(request.Topic, request.Prompt);
            //    return Ok(new { lesson = result });
            //}
        

    }
}




