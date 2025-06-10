using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI.LearningPlatform.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly SubCategoryService _subCategoryService;

        public SubCategoriesController(SubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubCategory>>> Get()
        {
            var subcategories = await _subCategoryService.GetAllSubCategoriesAsync();
            return Ok(subcategories);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SubCategory subCategory)
        {
            await _subCategoryService.AddSubCategoryAsync(subCategory);
            return Ok();
        }
    }
}
