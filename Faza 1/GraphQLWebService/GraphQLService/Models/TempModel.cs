﻿using System;
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
        public string Id { get; set; }
        [BsonElement("time_id")]
        public string Time_id { get; set; }

        [BsonElement("ping_ms")]
        public string Ping_ms { get; set; }

        [BsonElement("temperature_c")]
        public string Temperature_c { get; set; }

        [BsonElement("humidity_p")]
        public string Humidity_p { get; set; }

    }
}
