using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Back
{
    public class Movie
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Synopsis { get; set; }
    }
}