
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.BL.Services
{
    public class SubCategoryService
    {
        private readonly SubCategoryRepository _subCategoryRepository;

        public SubCategoryService(SubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
        {
            return await _subCategoryRepository.GetAllAsync();
        }

        public async Task AddSubCategoryAsync(SubCategory subCategory)
        {
            await _subCategoryRepository.AddAsync(subCategory);
        }
    }
}
