
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.BL.Services
{
    public class PromptService
    {
        private readonly PromptRepository _promptRepository;

        public PromptService(PromptRepository promptRepository)
        {
            _promptRepository = promptRepository;
        }

        public async Task<List<Prompt>> GetAllPromptsAsync()
        {
            return await _promptRepository.GetAllAsync();
        }

        public async Task AddPromptAsync(Prompt prompt)
        {
            await _promptRepository.AddAsync(prompt);
        }
    }
}


