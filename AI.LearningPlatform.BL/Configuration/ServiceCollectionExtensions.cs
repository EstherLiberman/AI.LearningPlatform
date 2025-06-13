//using AI.LearningPlatform.BL.Services;
//using AI.LearningPlatform.DAL.Repositories;
//using Microsoft.Extensions.DependencyInjection;

//namespace AI.LearningPlatform.BL.Configuration
//{
//    public static class ServiceCollectionExtensions
//    {
//        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
//        {
//            // Repositories
//            services.AddScoped<UserRepository>();
//            services.AddScoped<CategoryRepository>();
//            services.AddScoped<SubCategoryRepository>();
//            services.AddScoped<PromptRepository>();
//            services.AddScoped<LessonRepository>();

//            // Services
//            services.AddScoped<UserService>();
//            services.AddScoped<CategoryService>();
//            services.AddScoped<SubCategoryService>();
//            services.AddScoped<PromptService>();
//            services.AddScoped<LessonService>();

//            return services;
//        }
//    }
//}
