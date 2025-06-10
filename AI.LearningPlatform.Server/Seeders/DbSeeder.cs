using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;

namespace AI.LearningPlatform.Server.NewFolder
{
    public class DbSeeder
    {
        private readonly UserRepository _userRepo;
        private readonly CategoryRepository _categoryRepo;
        private readonly SubCategoryRepository _subCategoryRepo;
        private readonly PromptRepository _promptRepo;

        public DbSeeder(UserRepository userRepo, CategoryRepository categoryRepo,
                        SubCategoryRepository subCategoryRepo, PromptRepository promptRepo)
        {
            _userRepo = userRepo;
            _categoryRepo = categoryRepo;
            _subCategoryRepo = subCategoryRepo;
            _promptRepo = promptRepo;
        }

        public async Task SeedAsync()
        {
            // Seed Users
            var users = await _userRepo.GetAllUsersAsync();
            if (!users.Any())
            {
                await _userRepo.AddAsync(new User { Name = "משתמש ראשון", Phone = "0500000000" });
            }

            // Seed Categories
            var categories = await _categoryRepo.GetAllAsync();
            if (!categories.Any())
            {
                await _categoryRepo.AddAsync(new Category { Name = "כללי" });
            }

        }
    }
}
