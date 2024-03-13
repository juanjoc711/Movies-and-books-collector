using MongoDB.Driver;

using MongoDB.Driver;

namespace Back
{
    public class MongoConnection
    {
        private static MongoClient _client;
        private static IMongoDatabase _database;

        public MongoConnection()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("MoviesCollector");
        }

        public IMongoCollection<Movie> Movies
        {
            get
            {
                return _database.GetCollection<Movie>("Movies");
            }
        }

        public IMongoCollection<Book> Books
        {
            get
            {
                return _database.GetCollection<Book>("Books");
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }

    }
}