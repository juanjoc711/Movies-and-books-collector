using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Front
{
    public class Movie
    {
        [BsonId]
        [JsonProperty("Id")] // Indica a Newtonsoft.Json que el campo se llama "Id"
        public string Id { get; set; } // Cambia el tipo a string

        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Synopsis { get; set; }
    }
}
