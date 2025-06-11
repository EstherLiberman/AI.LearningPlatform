//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;
//////using System.Threading.Tasks;

//////namespace AI.LearningPlatform.BL.Services
//////{
//////    internal class LessonService
//////    {
//////    }
//////}


////using AI.LearningPlatform.DAL.Repositories;
////using AI.LearningPlatform.DAL.Models;
////using System.Threading.Tasks;
////using System.Collections.Generic;

////namespace AI.LearningPlatform.BL.Services
////{
////    public class LessonService
////    {
////        private readonly OpenAiService _openAiService;
////        private readonly LessonRepository _lessonRepository;
////        private readonly UserRepository _userRepository;
////        // אם תרצי לשלוף שמות של קטגוריה ותת-קטגוריה מה-DB, תצטרכי להוסיף כאן גם:
////        // private readonly CategoryRepository _categoryRepository;
////        // private readonly SubCategoryRepository _subCategoryRepository;


////        public LessonService(OpenAiService openAiService, LessonRepository lessonRepository, UserRepository userRepository
////            /* אם הוספת CategoryRepository ו-SubCategoryRepository, הוסף אותם כאן גם: */
////            //, CategoryRepository categoryRepository, SubCategoryRepository subCategoryRepository
////            )
////        {
////            _openAiService = openAiService;
////            _lessonRepository = lessonRepository;
////            _userRepository = userRepository;
////            // _categoryRepository = categoryRepository; // ודא שאת מפעילה את השורה הזו רק אם הוספת אותה לקונסטרקטור
////            // _subCategoryRepository = subCategoryRepository; // ודא שאת מפעילה את השורה הזו רק אם הוספת אותה לקונסטרקטור
////        }

////        public async Task<Lesson> GenerateAndSaveLessonAsync(string userId, string categoryId, string subCategoryId, string promptText)
////        {
////            // 1. קבלת פרטי קטגוריה/תת-קטגוריה מה-DAL (אופציונלי, לצורך בניית הנחיה טובה יותר)
////            // לצורך בדיקה ראשונית, נשתמש בשמות ברירת מחדל.
////            // בעתיד, תוכל לשלוף את השמות האמיתיים מה-DB.
////            string categoryName = "כללי";
////            string subCategoryName = "אין";

////            // כאן היית שולף את הקטגוריה והתת-קטגוריה מהרפוזיטורי כדי לקבל את השמות שלהן:
////            // var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
////            // var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subCategoryId);
////            // categoryName = category?.Name ?? "General";
////            // subCategoryName = subCategory?.Name ?? "N/A";


////            // 2. יצירת תוכן השיעור באמצעות OpenAiService
////            var generatedContent = await _openAiService.GenerateLessonContentAsync(categoryName, subCategoryName, promptText);

////            // 3. שמירת השיעור במסד הנתונים
////            var newLesson = new Lesson
////            {
////                UserId = userId,
////                CategoryId = categoryId,
////                SubCategoryId = subCategoryId,
////                PromptText = promptText,
////                GeneratedContent = generatedContent,
////                DateCreated = DateTime.UtcNow
////            };

////            await _lessonRepository.CreateLessonAsync(newLesson);

////            // 4. עדכון היסטוריית הלמידה של המשתמש
////            var user = await _userRepository.GetUserByIdAsync(userId);
////            if (user != null)
////            {
////                // וודא שהרשימה LearningHistory מאותחלת במודל User
////                // (כפי שצוין בשלב 1 של ההנחיות הקודמות)
////                if (user.LearningHistory == null)
////                {
////                    user.LearningHistory = new List<Lesson>();
////                }
////                user.LearningHistory.Add(newLesson);
////                await _userRepository.UpdateUserAsync(userId, user);
////            }

////            return newLesson;
////        }

////        public async Task<List<Lesson>> GetUserLearningHistoryAsync(string userId)
////        {
////            return await _lessonRepository.GetLessonsByUserIdAsync(userId);
////        }

////        // אם תרצי לקבל שיעור ספציפי:
////        public async Task<Lesson> GetLessonByIdAsync(string lessonId)
////        {
////            return await _lessonRepository.GetLessonByIdAsync(lessonId);
////        }
////    }
////}


//using AI.LearningPlatform.DAL.Models;
//using AI.LearningPlatform.DAL.Repositories;
////using AI.LearningPlatform.DTOs;
////using OpenAI.GPT3;
////using OpenAI.GPT3.Interfaces;
////using OpenAI.GPT3.ObjectModels;
////using OpenAI.GPT3.ObjectModels.RequestModels;
//using OpenAI.Interfaces;
//using OpenAI.ObjectModels;
//using OpenAI.ObjectModels.RequestModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AI.LearningPlatform.BL.Services
//{
//    public class LessonService
//    {
//        //private readonly IOpenAIService _openAiService;
//        //private readonly LessonRequestDto _lessonRepository;

//        //public LessonService(IOpenAIService openAiService, LessonRequestDto lessonRepository)
//        //{
//        //    _openAiService = openAiService;
//        //    _lessonRepository = lessonRepository;
//        //}
//        private readonly IOpenAIService _openAiService;
//        private readonly LessonRepository _lessonRepository;

//        public LessonService(IOpenAIService openAiService, LessonRepository lessonRepository)
//        {
//            _openAiService = openAiService;
//            _lessonRepository = lessonRepository;
//        }

//        public async Task<Lesson> GenerateLessonAsync(LessonRequestDto request)
//        {
//            var prompt = $"כתוב שיעור על הנושא: {request.Prompt}";

//            var chatRequest = new ChatCompletionCreateRequest
//            {
//                Model = Models.Gpt_3_5_Turbo,
//                Messages = new List<ChatMessage>
//                {
//                    ChatMessage.FromSystem("אתה מורה המלמד בצורה ברורה ומובנת."),
//                    ChatMessage.FromUser(prompt)
//                },
//                MaxTokens = 500
//            };

//            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(chatRequest);

//            if (!completionResult.Successful)
//                throw new Exception("אירעה שגיאה בעת יצירת השיעור מה-AI.");

//            var lessonContent = completionResult.Choices.First().Message.Content;

//            var lesson = new Lesson
//            {
//                UserId = request.UserId,
//                CategoryId = request.CategoryId,
//                SubCategoryId = request.SubCategoryId,
//                PromptText = request.Prompt,
//                GeneratedContent = lessonContent,
//                DateCreated = DateTime.UtcNow
//            };

//            await _lessonRepository.AddAsync(lesson);

//            return lesson;


//        }
//    }
//}


using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.Repositories;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.LearningPlatform.BL.Services
{
    public class LessonService
    {
        private readonly IOpenAIService _openAiService;
        private readonly LessonRepository _lessonRepository;

        public LessonService(IOpenAIService openAiService, LessonRepository lessonRepository)
        {
            _openAiService = openAiService;
            _lessonRepository = lessonRepository;
        }

        public async Task<Lesson> GenerateLessonAsync(LessonRequestDto request)
        {
            // יצירת ההנחיה שתישלח ל-AI
            var prompt = $"כתוב שיעור על הנושא: {request.Prompt}";

            // הכנת בקשת ChatCompletion ל-OpenAI
            var chatRequest = new ChatCompletionCreateRequest
            {
                Model = Models.Gpt_3_5_Turbo,
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("אתה מורה המלמד בצורה ברורה ומובנת."),
                    ChatMessage.FromUser(prompt)
                },
                MaxTokens = 500
            };

            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(chatRequest);

            if (!completionResult.Successful || completionResult.Choices.Count == 0)
            {
                var errorMessage = completionResult.Error?.Message ?? "שגיאה כללית לא ידועה";
                Console.WriteLine($"OpenAI error: {errorMessage}");
                throw new Exception("אירעה שגיאה בעת יצירת השיעור מה-AI.");
            }

            var generatedContent = completionResult.Choices.First().Message.Content;

            // בניית אובייקט Lesson
            var lesson = new Lesson
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                SubCategoryId = request.SubCategoryId,
                PromptText = request.Prompt,
                GeneratedContent = generatedContent,
                DateCreated = DateTime.UtcNow
            };

            // שמירה למסד הנתונים
            await _lessonRepository.AddAsync(lesson);

            return lesson;
        }

        public async Task<List<Lesson>> GetLessonsByUserAsync(string userId)
        {
            return await _lessonRepository.GetLessonsByUserIdAsync(userId);
        }

        public async Task<Lesson?> GetLessonByIdAsync(string lessonId)
        {
            return await _lessonRepository.GetLessonByIdAsync(lessonId);
        }
    }
}
