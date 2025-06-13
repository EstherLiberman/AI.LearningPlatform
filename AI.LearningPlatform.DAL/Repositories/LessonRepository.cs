
//using AI.LearningPlatform.DAL.Models;
//using MongoDB.Driver;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace AI.LearningPlatform.DAL.Repositories
//{
//    public class LessonRepository
//    {
//        private readonly IMongoCollection<Lesson> _lessons;

//        public LessonRepository(IMongoDatabase database)
//        {
//            _lessons = database.GetCollection<Lesson>("Lessons");
//        }

//        // הוספת שיעור חדש למסד הנתונים
//        public async Task AddAsync(Lesson lesson)
//        {
//            await _lessons.InsertOneAsync(lesson);
//        }

//        // שליפת כל השיעורים לפי מזהה משתמש
//        public async Task<List<Lesson>> GetLessonsByUserIdAsync(string userId)
//        {
//            var filter = Builders<Lesson>.Filter.Eq(l => l.UserId, userId);
//            return await _lessons.Find(filter).ToListAsync();
//        }

//        // שליפת שיעור לפי מזהה שיעור
//        public async Task<Lesson?> GetLessonByIdAsync(string lessonId)
//        {
//            var filter = Builders<Lesson>.Filter.Eq(l => l.Id, lessonId);
//            return await _lessons.Find(filter).FirstOrDefaultAsync();
//        }
//    }
//}

