////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

////namespace AI.LearningPlatform.BL.Services
////{
////    internal class LessonService
////    {
////    }
////}


//using AI.LearningPlatform.DAL.Repositories;
//using AI.LearningPlatform.DAL.Models;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace AI.LearningPlatform.BL.Services
//{
//    public class LessonService
//    {
//        private readonly OpenAiService _openAiService;
//        private readonly LessonRepository _lessonRepository;
//        private readonly UserRepository _userRepository;
//        // אם תרצי לשלוף שמות של קטגוריה ותת-קטגוריה מה-DB, תצטרכי להוסיף כאן גם:
//        // private readonly CategoryRepository _categoryRepository;
//        // private readonly SubCategoryRepository _subCategoryRepository;


//        public LessonService(OpenAiService openAiService, LessonRepository lessonRepository, UserRepository userRepository
//            /* אם הוספת CategoryRepository ו-SubCategoryRepository, הוסף אותם כאן גם: */
//            //, CategoryRepository categoryRepository, SubCategoryRepository subCategoryRepository
//            )
//        {
//            _openAiService = openAiService;
//            _lessonRepository = lessonRepository;
//            _userRepository = userRepository;
//            // _categoryRepository = categoryRepository; // ודא שאת מפעילה את השורה הזו רק אם הוספת אותה לקונסטרקטור
//            // _subCategoryRepository = subCategoryRepository; // ודא שאת מפעילה את השורה הזו רק אם הוספת אותה לקונסטרקטור
//        }

//        public async Task<Lesson> GenerateAndSaveLessonAsync(string userId, string categoryId, string subCategoryId, string promptText)
//        {
//            // 1. קבלת פרטי קטגוריה/תת-קטגוריה מה-DAL (אופציונלי, לצורך בניית הנחיה טובה יותר)
//            // לצורך בדיקה ראשונית, נשתמש בשמות ברירת מחדל.
//            // בעתיד, תוכל לשלוף את השמות האמיתיים מה-DB.
//            string categoryName = "כללי";
//            string subCategoryName = "אין";

//            // כאן היית שולף את הקטגוריה והתת-קטגוריה מהרפוזיטורי כדי לקבל את השמות שלהן:
//            // var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
//            // var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subCategoryId);
//            // categoryName = category?.Name ?? "General";
//            // subCategoryName = subCategory?.Name ?? "N/A";


//            // 2. יצירת תוכן השיעור באמצעות OpenAiService
//            var generatedContent = await _openAiService.GenerateLessonContentAsync(categoryName, subCategoryName, promptText);

//            // 3. שמירת השיעור במסד הנתונים
//            var newLesson = new Lesson
//            {
//                UserId = userId,
//                CategoryId = categoryId,
//                SubCategoryId = subCategoryId,
//                PromptText = promptText,
//                GeneratedContent = generatedContent,
//                DateCreated = DateTime.UtcNow
//            };

//            await _lessonRepository.CreateLessonAsync(newLesson);

//            // 4. עדכון היסטוריית הלמידה של המשתמש
//            var user = await _userRepository.GetUserByIdAsync(userId);
//            if (user != null)
//            {
//                // וודא שהרשימה LearningHistory מאותחלת במודל User
//                // (כפי שצוין בשלב 1 של ההנחיות הקודמות)
//                if (user.LearningHistory == null)
//                {
//                    user.LearningHistory = new List<Lesson>();
//                }
//                user.LearningHistory.Add(newLesson);
//                await _userRepository.UpdateUserAsync(userId, user);
//            }

//            return newLesson;
//        }

//        public async Task<List<Lesson>> GetUserLearningHistoryAsync(string userId)
//        {
//            return await _lessonRepository.GetLessonsByUserIdAsync(userId);
//        }

//        // אם תרצי לקבל שיעור ספציפי:
//        public async Task<Lesson> GetLessonByIdAsync(string lessonId)
//        {
//            return await _lessonRepository.GetLessonByIdAsync(lessonId);
//        }
//    }
//}