//using AI.LearningPlatform.DAL.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace AI.LearningPlatform.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class LessonsController : ControllerBase
//    {
//        private readonly LessonService _lessonService;

//        public LessonsController(LessonService lessonService)
//        {
//            _lessonService = lessonService;
//        }

//        /// <summary>
//        /// Generates a new lesson based on user's input and saves it to their learning history.
//        /// </summary>
//        [HttpPost("generate")]
//        public async Task<ActionResult<Lesson>> GenerateLesson([FromBody] GenerateLessonRequest request)
//        {
//            if (string.IsNullOrWhiteSpace(request.UserId) ||
//                (string.IsNullOrWhiteSpace(request.CategoryId) && string.IsNullOrWhiteSpace(request.SubCategoryId) && string.IsNullOrWhiteSpace(request.PromptText)))
//            {
//                return BadRequest("User ID and at least one of Category ID, SubCategory ID, or Prompt Text are required.");
//            }

//            var newLesson = await _lessonService.GenerateAndSaveLessonAsync(
//                request.UserId,
//                request.CategoryId,
//                request.SubCategoryId,
//                request.PromptText
//            );

//            return CreatedAtAction(nameof(GetLessonById), new { id = newLesson.Id }, newLesson);
//        }

//        /// <summary>
//        /// Gets the learning history for a specific user.
//        /// </summary>
//        [HttpGet("history/{userId}")]
//        public async Task<ActionResult<List<Lesson>>> GetLearningHistory(string userId)
//        {
//            if (string.IsNullOrWhiteSpace(userId))
//            {
//                return BadRequest("User ID is required.");
//            }

//            var lessons = await _lessonService.GetUserLearningHistoryAsync(userId);
//            if (lessons == null || lessons.Count == 0)
//            {
//                return NotFound($"No learning history found for user ID: {userId}");
//            }

//            return Ok(lessons);
//        }

//        /// <summary>
//        /// Gets a specific lesson by its ID.
//        /// </summary>
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Lesson>> GetLessonById(string id)
//        {
//            var lesson = await _lessonService.GetLessonByIdAsync(id);
//            if (lesson == null)
//            {
//                return NotFound();
//            }
//            return Ok(lesson);
//        }
//    }

//    // Request model for GenerateLesson endpoint
//    public class GenerateLessonRequest
//    {
//        public string UserId { get; set; }
//        public string CategoryId { get; set; } // Optional, can be empty if prompt is detailed
//        public string SubCategoryId { get; set; } // Optional, can be empty if prompt is detailed
//        public string PromptText { get; set; } // The user
//    }
//}
