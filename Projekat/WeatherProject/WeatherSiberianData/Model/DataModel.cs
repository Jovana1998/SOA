using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherSiberianData.Model
{
    public class DataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
         public string ID { get; set; }
        [BsonElement ("value")]
        public string Value { get; set; }

        [BsonElement("recordTime")]
        public string RecordTime { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }

    }
}
