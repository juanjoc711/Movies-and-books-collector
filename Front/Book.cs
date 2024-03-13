using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front
{
    public class Book
    {
        [BsonId]
        [JsonProperty("Id")] // Indica a Newtonsoft.Json que el campo se llama "Id"
        public string Id { get; set; } // Cambia el tipo a string
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}