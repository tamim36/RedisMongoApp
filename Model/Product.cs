using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PrimaryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int counter { get; set; }
        public string Source { get; set; }
    }
}
