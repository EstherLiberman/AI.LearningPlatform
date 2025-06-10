using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.DAL.Models
{
    public class Prompt
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        //[BsonElement("userId")]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string UserId { get; set; }

        //[BsonElement("categoryId")]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string CategoryId { get; set; }

        //[BsonElement("subCategoryId")]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string SubCategoryId { get; set; }

        //[BsonElement("prompt")]
        //public string PromptText { get; set; }
        //public string Text { get; internal set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ?Id { get; set; }
        public string ?UserId { get; set; } // המשתמש שלמד את השיעור
        public string ?CategoryId { get; set; }
        public string ?SubCategoryId { get; set; }
        public string ?PromptText { get; set; } // ההנחיה המקורית שנשלחה ל-AI
        public string ?GeneratedContent { get; set; } // התוכן של השיעור שנוצר על ידי ה-AI
        public DateTime ?DateCreated { get; set; } = DateTime.UtcNow; // תאריך יצירת השיעור
    }
}
