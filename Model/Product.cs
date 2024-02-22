﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebConnectMongoDBAndSQL.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
