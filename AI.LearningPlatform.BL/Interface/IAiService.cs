using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.BL.NewFolder
{
    public interface IAiService
    {
        Task<string> GetCompletionAsync(string prompt);
    }

}
