using AI.LearningPlatform.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.DAL.NewFolder
{
    public static class DALDependencies
    {
        public static void AddDALRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<SubCategoryRepository>();
            services.AddScoped<PromptRepository>();
        }
    }
}
