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
        [BsonRepresentation(BsonType.ObjectId)]
        public string TodoId { get; set; }
        [BsonRequired]
        public string TodoDescription { get; set; }
    }
}
