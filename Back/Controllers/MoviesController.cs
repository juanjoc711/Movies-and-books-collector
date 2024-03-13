using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly MongoConnection _mongoConnection = new MongoConnection();


        // GET api/movies
        public async Task<IHttpActionResult> Get()
        {
            var movies = await _mongoConnection.Movies.AsQueryable().ToListAsync();

            return Ok(movies);
        }
        // GET api/movies/bytitle/{title}
        [HttpGet]
        [Route("api/movies/bytitle/{title}")]
        public async Task<IHttpActionResult> GetByTitle(string title)
        {
            var filter = Builders<Movie>.Filter.Eq(m => m.Title, title);
            var movies = await _mongoConnection.Movies.Find(filter).ToListAsync();

            if (movies == null || movies.Count == 0)
                return NotFound();

            return Ok(movies);
        }


        // GET api/movies/{id}
        public async Task<IHttpActionResult> Get(string id)
        {
            var filter = Builders<Movie>.Filter.Eq(m => m.Id, ObjectId.Parse(id));
            var movie = await _mongoConnection.Movies.Find(filter).FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // POST api/movies
        public async Task<IHttpActionResult> Post([FromBody] Movie movie)
        {
            await _mongoConnection.Movies.InsertOneAsync(movie);
            return Ok();
        }

        // PUT api/movies/{id}
        public async Task<IHttpActionResult> Put(string id, [FromBody] Movie movie)
        {
            var filter = Builders<Movie>.Filter.Eq(m => m.Id, ObjectId.Parse(id));

            movie.Id = ObjectId.Parse(id);
            var result = await _mongoConnection.Movies.ReplaceOneAsync(filter, movie);

            if (result.ModifiedCount == 0)
                return NotFound();

            return Ok(movie);
        }

        // DELETE api/movies/{id}
        public async Task<IHttpActionResult> Delete(string id)
        {
            var filter = Builders<Movie>.Filter.Eq(m => m.Id, ObjectId.Parse(id));
            var result = await _mongoConnection.Movies.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
                return NotFound();

            return Ok();
        }
    }
}