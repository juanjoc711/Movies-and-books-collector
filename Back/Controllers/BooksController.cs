using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Back.Controllers
{
    public class BooksController : ApiController
    {
        private readonly MongoConnection _mongoConnection = new MongoConnection();

        // GET api/books
        public async Task<IHttpActionResult> Get()
        {
            var books = await _mongoConnection.Books.AsQueryable().ToListAsync();

            return Ok(books);
        }

        // GET api/books/bytitle/{title}
        [HttpGet]
        [Route("api/books/bytitle/{title}")]
        public async Task<IHttpActionResult> GetByTitle(string title)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Title, title);
            var books = await _mongoConnection.Books.Find(filter).ToListAsync();

            if (books == null || books.Count == 0)
                return NotFound();

            return Ok(books);
        }

        // POST api/books
        public async Task<IHttpActionResult> Post([FromBody] Book book)
        {
            await _mongoConnection.Books.InsertOneAsync(book);
            return Ok();
        }

        // PUT api/books/{id}
        public async Task<IHttpActionResult> Put(string id, [FromBody] Book book)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, ObjectId.Parse(id));

            book.Id = ObjectId.Parse(id);
            var result = await _mongoConnection.Books.ReplaceOneAsync(filter, book);

            if (result.ModifiedCount == 0)
                return NotFound();

            return Ok(book);
        }

        // DELETE api/books/{id}
        public async Task<IHttpActionResult> Delete(string id)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, ObjectId.Parse(id));
            var result = await _mongoConnection.Books.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
                return NotFound();

            return Ok();
        }

    }

}