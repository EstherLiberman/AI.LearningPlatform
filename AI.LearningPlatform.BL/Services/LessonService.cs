
////using AI.LearningPlatform.DAL.Models;
////using AI.LearningPlatform.DAL.Repositories;
////using OpenAI.Interfaces;
////using OpenAI.ObjectModels;
////using OpenAI.ObjectModels.RequestModels;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;

////namespace AI.LearningPlatform.BL.Services
////{
////    public class LessonService
////    {
////        private readonly IOpenAIService _openAiService;
////        private readonly LessonRepository _lessonRepository;

////        public LessonService(IOpenAIService openAiService, LessonRepository lessonRepository)
////        {
////            _openAiService = openAiService;
////            _lessonRepository = lessonRepository;
////        }

////        public async Task<Lesson> GenerateLessonAsync(LessonRequestDto request)
////        {
////            // יצירת ההנחיה שתישלח ל-AI
////            var prompt = $"כתוב שיעור על הנושא: {request.Prompt}";

////            // הכנת בקשת ChatCompletion ל-OpenAI
////            var chatRequest = new ChatCompletionCreateRequest
////            {
////                Model = Models.Gpt_3_5_Turbo,
////                Messages = new List<ChatMessage>
////                {
////                    ChatMessage.FromSystem("אתה מורה המלמד בצורה ברורה ומובנת."),
////                    ChatMessage.FromUser(prompt)
////                },
////                MaxTokens = 500
////            };

////            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(chatRequest);

////            if (!completionResult.Successful || completionResult.Choices.Count == 0)
////            {
////                var errorMessage = completionResult.Error?.Message ?? "שגיאה כללית לא ידועה";
////                Console.WriteLine($"OpenAI error: {errorMessage}");
////                throw new Exception("אירעה שגיאה בעת יצירת השיעור מה-AI.");
////            }

////            var generatedContent = completionResult.Choices.First().Message.Content;

////            // בניית אובייקט Lesson
////            var lesson = new Lesson
////            {
////                UserId = request.UserId,
////                CategoryId = request.CategoryId,
////                SubCategoryId = request.SubCategoryId,
////                PromptText = request.Prompt,
////                GeneratedContent = generatedContent,
////                DateCreated = DateTime.UtcNow
////            };

////            // שמירה למסד הנתונים
////            await _lessonRepository.AddAsync(lesson);

////            return lesson;
////        }

////        public async Task<List<Lesson>> GetLessonsByUserAsync(string userId)
////        {
////            return await _lessonRepository.GetLessonsByUserIdAsync(userId);
////        }

////        public async Task<Lesson?> GetLessonByIdAsync(string lessonId)
////        {
////            return await _lessonRepository.GetLessonByIdAsync(lessonId);
////        }
////    }
////}
//using AI.LearningPlatform.DAL.Models;
//using AI.LearningPlatform.DAL.Repositories;
//using OpenAI.Interfaces;
//using OpenAI.ObjectModels;
//using OpenAI.ObjectModels.RequestModels;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AI.LearningPlatform.BL.Services
//{
//    public class LessonService
//    {
//        private readonly IOpenAIService _openAiService;
//        private readonly LessonRepository _lessonRepository;
//        private readonly ILogger<LessonService> _logger;

//        public LessonService(IOpenAIService openAiService, LessonRepository lessonRepository, ILogger<LessonService> logger)
//        {
//            _openAiService = openAiService;
//            _lessonRepository = lessonRepository;
//            _logger = logger;
//        }

//        public async Task<Lesson> GenerateLessonAsync(LessonRequestDto request)
//        {
//            try
//            {
//                var prompt = $"כתוב שיעור על הנושא: {request.Prompt}";

//                var chatRequest = new ChatCompletionCreateRequest
//                {
//                    Model = Models.Gpt_3_5_Turbo,
//                    Messages = new List<ChatMessage>
//                    {
//                        ChatMessage.FromSystem("אתה מורה המלמד בצורה ברורה ומובנת."),
//                        ChatMessage.FromUser(prompt)
//                    },
//                    MaxTokens = 500
//                };

//                var completionResult = await _openAiService.ChatCompletion.CreateCompletion(chatRequest);

//                if (!completionResult.Successful || completionResult.Choices.Count == 0)
//                {
//                    var errorMessage = completionResult.Error?.Message ?? "שגיאה כללית לא ידועה";
//                    _logger.LogError("שגיאה מה-AI: {Error}", errorMessage);

//                    if (completionResult.Error?.Code == "rate_limit_exceeded")
//                    {
//                        throw new Exception("חרגת מהמכסה המותרת. אנא נסי שוב מאוחר יותר.");
//                    }

//                    throw new Exception("אירעה שגיאה בעת יצירת השיעור מה-AI.");
//                }

//                var generatedContent = completionResult.Choices.First().Message.Content;

//                var lesson = new Lesson
//                {
//                    UserId = request.UserId,
//                    CategoryId = request.CategoryId,
//                    SubCategoryId = request.SubCategoryId,
//                    PromptText = request.Prompt,
//                    GeneratedContent = generatedContent,
//                    DateCreated = DateTime.UtcNow
//                };

//                await _lessonRepository.AddAsync(lesson);

//                return lesson;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "שגיאה כללית בעת יצירת שיעור מ-AI.");
//                throw new Exception("אירעה שגיאה בעת יצירת השיעור מה-AI. אנא נסי שוב או פני לתמיכה.");
//            }
//        }

//        public async Task<List<Lesson>> GetLessonsByUserAsync(string userId)
//        {
//            return await _lessonRepository.GetLessonsByUserIdAsync(userId);
//        }

//        public async Task<Lesson?> GetLessonByIdAsync(string lessonId)
//        {
//            return await _lessonRepository.GetLessonByIdAsync(lessonId);
//        }
//    }
//}
