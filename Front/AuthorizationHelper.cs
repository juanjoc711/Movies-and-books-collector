using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Back;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Front
{
    public  class AuthorizationHelper
    {
        private const string ApiBaseUrl = "https://localhost:44366/api/users";
        private readonly HttpClient httpClient;
        private readonly MongoConnection _mongoConnection = new MongoConnection();
        public static async Task<bool> CheckIfUserIsAdmin(string username)
        {
            using (HttpClient client = new HttpClient())
            {
                string searchUrl = $"{ApiBaseUrl}/checkifadmin?username={username}";

                var response = await client.GetAsync(searchUrl);

                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                    return user != null && user.Role == "Admin";
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
