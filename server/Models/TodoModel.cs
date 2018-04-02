using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class TodoModel
    {
        [BsonId]
        public ObjectId TodoId { get; set; }
        [BsonRequired]
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime? Deadline { get; set; } = null;
    }
}
