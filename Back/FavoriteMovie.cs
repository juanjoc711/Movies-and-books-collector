using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class FavoriteMovie
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("UserId")]
    public string UserId { get; set; }
    [BsonElement("UserName")]
    public string UserName { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; }
}