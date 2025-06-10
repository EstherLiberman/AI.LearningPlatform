using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI.LearningPlatform.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            await _categoryService.AddCategoryAsync(category);
            return Ok();
        }
    }
}
