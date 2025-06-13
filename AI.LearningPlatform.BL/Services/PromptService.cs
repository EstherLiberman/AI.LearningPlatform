using AI.LearningPlatform.BL.NewFolder;
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//namespace AI.LearningPlatform.BL.Services
//{
//    public class PromptService
//    {
//        private readonly PromptRepository _promptRepository;
//        private readonly IAiService _aiService;

//        public PromptService(PromptRepository promptRepository, IAiService aiService)
//        {
//            _promptRepository = promptRepository;
//            _aiService = aiService;
//        }

//        public async Task<List<Prompt>> GetAllPromptsAsync()
//        {
//            return await _promptRepository.GetAllAsync();
//        }

//        public async Task AddPromptAsync(Prompt prompt)
//        {
//            await _promptRepository.AddAsync(prompt);
//        }

//        public async Task<Prompt?> GetPromptByIdAsync(string id)
//        {
//            return await _promptRepository.GetByIdAsync(id);
//        }

//        /// <summary>
//        /// שולח פרומפט ל-AI לפי מזהה מהמסד ומחזיר תשובה
//        /// </summary>
//        public async Task<string> GetAiResponseForPromptAsync(string promptId)
//        {
//            var prompt = await GetPromptByIdAsync(promptId);
//            if (prompt == null || string.IsNullOrWhiteSpace(prompt.PromptText))
//            {
//                return "לא נמצא פרומפט תקף לשליחה.";
//            }

//            // שליחה ל-AI
//            return await _aiService.GetCompletionAsync(prompt.PromptText);

//        }
//        public async Task UpdatePromptAsync(Prompt prompt)
//        {
//            await _promptRepository.UpdateAsync(prompt);
//        }

//    }
//}

using AI.LearningPlatform.BL.NewFolder;
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;

namespace AI.LearningPlatform.BL.Services
{
    public class PromptService
    {
        private readonly PromptRepository _promptRepository;
        private readonly IAiService _aiService;
        private readonly UserService _userService;

        public PromptService(PromptRepository promptRepository, IAiService aiService, UserService userService)
        {
            _promptRepository = promptRepository;
            _aiService = aiService;
            _userService = userService;
        }

        public async Task<List<Prompt>> GetAllPromptsAsync()
        {
            return await _promptRepository.GetAllAsync();
        }

        public async Task AddPromptAsync(Prompt prompt)
        {
            await _promptRepository.AddAsync(prompt);
        }

        public async Task<Prompt?> GetPromptByIdAsync(string id)
        {
            return await _promptRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// שולח פרומפט ל-AI לפי מזהה מהמסד ומחזיר תשובה
        /// </summary>
        public async Task<string> GetAiResponseForPromptAsync(string promptId)
        {
            var prompt = await GetPromptByIdAsync(promptId);
            if (prompt == null || string.IsNullOrWhiteSpace(prompt.PromptText))
            {
                return "לא נמצא פרומפט תקף לשליחה.";
            }

            // שליחה ל-AI
            return await _aiService.GetCompletionAsync(prompt.PromptText);
        }

        public async Task UpdatePromptAsync(Prompt prompt)
        {
            await _promptRepository.UpdateAsync(prompt);
        }

        public async Task<List<Prompt>> GetPromptsByUserNameAndPhoneAsync(string name, string phone)
        {
            var user = await _userService.GetUserByNameAndPhoneAsync(name, phone);
            if (user == null)
                return new List<Prompt>();

            return await _promptRepository.GetPromptsByUserIdAsync(user.Id);
        }
    }
}
