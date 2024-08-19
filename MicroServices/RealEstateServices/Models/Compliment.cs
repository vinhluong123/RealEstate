using MongoDB.Bson;

namespace RealEstateServices.Models
{
    public class Compliment
    {
        public ObjectId Id { get; set; }
        public string Content { get; set; }
    }
}
