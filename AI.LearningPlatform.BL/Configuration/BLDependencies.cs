using AI.LearningPlatform.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.BL.Configuration
{
    public static class BLDependencies
    {
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<SubCategoryService>();
            services.AddScoped<PromptService>();

            // ניתן להוסיף כאן שירותים נוספים מה-BL
        }
    }
}
