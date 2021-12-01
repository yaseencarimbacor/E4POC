﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CosmosBooksApi.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("number")]
        public string TelephoneNumber { get; set; }

        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("dateofbirth")]
        public string DateOfBirth { get; set; }
        [BsonElement("notes")]
        public string Notes { get; set; }
    }
}
