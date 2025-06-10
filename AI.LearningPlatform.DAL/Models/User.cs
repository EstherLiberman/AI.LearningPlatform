using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.LearningPlatform.DAL.Models
{
    public class User
    {
       [BsonId]
       [BsonRepresentation(BsonType.ObjectId)]
       public string? Id { get; set; }

       [BsonElement("name")]
       public string Name { get; set; } = string.Empty;

       [BsonElement("phone")]
       public string Phone { get; set; } = string.Empty;

        //public List<Lesson> LearningHistory { get; set; } = new List<Lesson>();

       public static object Find<T>(Func<object, bool> value)
       {
           throw new NotImplementedException();
       }
    }
}
