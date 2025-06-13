//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AI.LearningPlatform.DAL.Models
//{
//    public class Lesson
//    {
//        [BsonId]
//        [BsonRepresentation(BsonType.ObjectId)]
//        public string? Id { get; set; }
//        public string? UserId { get; set; } // המשתמש שלמד את השיעור
//        public string? CategoryId { get; set; } // ID של הקטגוריה
//        public string? SubCategoryId { get; set; } // ID של תת-הקטגוריה
//        public string? PromptText { get; set; } // ההנחיה המקורית שנשלחה ל-AI
//        public string? GeneratedContent { get; set; } // התוכן של השיעור שנוצר על ידי ה-AI
//        public DateTime? DateCreated { get; set; } = DateTime.UtcNow; // תאריך יצירת השיעור
//    }
//}
