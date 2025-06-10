//////using Microsoft.Extensions.Configuration;
//////using MongoDB.Driver;
//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;
//////using System.Threading.Tasks;
//////using AI.LearningPlatform.DAL.Models;
//////using Microsoft.Extensions.Configuration;

//////namespace AI.LearningPlatform.DAL.Repositories
//////{
//////    public class LessonRepository
//////    {
//////        private readonly IMongoCollection<Lesson> _lessons;

//////        public LessonRepository(IConfiguration configuration)
//////        {
//////            var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
//////            var client = new MongoClient(settings.ConnectionString);
//////            var database = client.GetDatabase(settings.DatabaseName);
//////            _lessons = database.GetCollection<Lesson>("Lessons"); // שם הקולקציה ב-MongoDB
//////        }

//////        public async Task<List<Lesson>> GetLessonsAsync() =>
//////            await _lessons.Find(lesson => true).ToListAsync();

//////        public async Task<List<Lesson>> GetLessonsByUserIdAsync(string userId) =>
//////            await _lessons.Find(lesson => lesson.UserId == userId).ToListAsync();

//////        public async Task<Lesson> GetLessonByIdAsync(string id) =>
//////            await _lessons.Find<Lesson>(lesson => lesson.Id == id).FirstOrDefaultAsync();

//////        public async Task CreateLessonAsync(Lesson newLesson) =>
//////            await _lessons.InsertOneAsync(newLesson);

//////        public async Task DeleteLessonAsync(string id) =>
//////            await _lessons.DeleteOneAsync(lesson => lesson.Id == id);
//////    }
//////}
////using AI.LearningPlatform.DAL.Models;
////using MongoDB.Driver;
////using System.Collections.Generic;
////using System.Threading.Tasks;

////namespace AI.LearningPlatform.DAL.Repositories
////{
////    public class LessonRepository
////    {
////        private readonly IMongoCollection<Lesson> _collection;

////        public LessonRepository(IMongoDatabase database)
////        {
////            _collection = database.GetCollection<Lesson>("Lessons");
////        }

////        public async Task<List<Lesson>> GetAllAsync()
////        {
////            return await _collection.Find(_ => true).ToListAsync();
////        }

////        public async Task<List<Lesson>> GetByUserIdAsync(string userId)
////        {
////            return await _collection.Find(lesson => lesson.UserId == userId).ToListAsync();
////        }

////        public async Task<Lesson> GetByIdAsync(string id)
////        {
////            return await _collection.Find(lesson => lesson.Id == id).FirstOrDefaultAsync();
////        }

////        public async Task AddAsync(Lesson lesson)
////        {
////            await _collection.InsertOneAsync(lesson);
////        }

////        public async Task DeleteAsync(string id)
////        {
////            await _collection.DeleteOneAsync(lesson => lesson.Id == id);
////        }
////    }
////}



//using AI.LearningPlatform.DAL.Models;
//using MongoDB.Driver;

//public class LessonRepository
//{
//    private readonly IMongoCollection<Lesson> _collection;

//    public LessonRepository(IMongoDatabase database)
//    {
//        _collection = database.GetCollection<Lesson>("lessons");
//    }

//    public async Task CreateLessonAsync(Lesson lesson) =>
//        await _collection.InsertOneAsync(lesson);

//    public async Task<Lesson?> GetLessonByIdAsync(string id) =>
//        await _collection.Find(l => l.Id == id).FirstOrDefaultAsync();

//    public async Task<List<Lesson>> GetLessonsByUserIdAsync(string userId) =>
//        await _collection.Find(l => l.UserId == userId).ToListAsync();
//}
