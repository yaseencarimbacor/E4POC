using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace e4POCApi.Models
{
    public class ContactDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

     
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("surname")]
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
