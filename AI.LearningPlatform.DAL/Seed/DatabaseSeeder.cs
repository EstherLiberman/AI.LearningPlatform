
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;

namespace AI.LearningPlatform.DAL.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            UserRepository userRepo,
            CategoryRepository categoryRepo,
            SubCategoryRepository subCategoryRepo,
            PromptRepository promptRepo)
        {
            var users = await userRepo.GetAllUsersAsync();
            if (!users.Any())
            {
                await userRepo.AddAsync(new User { Name = "אסתי", Phone = "0500000001" });
                await userRepo.AddAsync(new User { Name = "דני", Phone = "0500000002" });
                users = await userRepo.GetAllUsersAsync(); // עדכון
            }

            var categories = await categoryRepo.GetAllAsync();
            if (!categories.Any())
            {
                await categoryRepo.AddAsync(new Category { Name = "כללי" });
                await categoryRepo.AddAsync(new Category { Name = "טכנולוגיה" });
                categories = await categoryRepo.GetAllAsync(); // עדכון
            }

            var subCategories = await subCategoryRepo.GetAllAsync();
            if (!subCategories.Any())
            {
                var techCategoryId = categories.First(c => c.Name == "טכנולוגיה").Id;
                var generalCategoryId = categories.First(c => c.Name == "כללי").Id;

                await subCategoryRepo.AddAsync(new SubCategory { Name = "C#", CategoryId = techCategoryId });
                await subCategoryRepo.AddAsync(new SubCategory { Name = "כללי אחר", CategoryId = generalCategoryId });
                subCategories = await subCategoryRepo.GetAllAsync(); // עדכון
            }

            var prompts = await promptRepo.GetAllAsync();
            if (!prompts.Any())
            {
                await promptRepo.AddAsync(new Prompt
                {
                    UserId = users[0].Id,
                    CategoryId = categories[0].Id,
                    SubCategoryId = subCategories[0].Id,
                    PromptText = "כתוב קוד בסיסי ב־C#",
                    GeneratedContent = string.Empty
                });
            }
        }
    }
}
