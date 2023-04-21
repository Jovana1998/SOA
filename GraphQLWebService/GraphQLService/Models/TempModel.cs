using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQLService.Models
{
    public class TempModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement("value")]
        public string value { get; set; }
        [BsonElement("recordTime")]
        public string recordTime { get; set; }
        [BsonElement("type")]
        public string type { get; set; }

    }
}
