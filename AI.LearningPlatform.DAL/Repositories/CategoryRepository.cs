using AI.LearningPlatform.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.DAL.Repositories
{
    public class CategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categories.Find(category => true).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id) =>
            await _categories.Find<Category>(category => category.Id == id).FirstOrDefaultAsync();

        public async Task CreateCategoryAsync(Category newCategory) =>
            await _categories.InsertOneAsync(newCategory);

        public async Task UpdateCategoryAsync(string id, Category updatedCategory) =>
            await _categories.ReplaceOneAsync(category => category.Id == id, updatedCategory);

        public async Task DeleteCategoryAsync(string id) =>
            await _categories.DeleteOneAsync(category => category.Id == id);

        public async Task AddAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
        }

    }
}

