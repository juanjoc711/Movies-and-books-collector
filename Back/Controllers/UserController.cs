using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Claims;

namespace Back.Controllers
{


    namespace Back.Controllers
    {
        public class UserController : ApiController
        {
            private readonly MongoConnection _mongoConnection = new MongoConnection();

            // POST api/users/register
            [HttpPost]
            [Route("api/users/register")]
            public async Task<IHttpActionResult> Register([FromBody] User user)
            {
                // Verificar si el usuario ya existe
                var existingUser = await _mongoConnection.Users.Find(u => u.Username == user.Username).FirstOrDefaultAsync();
                if (existingUser != null)
                    return BadRequest("El nombre de usuario ya está en uso.");
                if (user.Username == "Admin")  // Reemplaza con tu criterio
                {
                    user.Role = "Admin";
                }
                else
                {
                    user.Role = "User";
                }
                // Guardar el nuevo usuario
                await _mongoConnection.Users.InsertOneAsync(user);
                return Ok("Usuario registrado exitosamente.");
            }

            // POST api/users/login
            [HttpPost]
            [Route("api/users/login")]
            public async Task<IHttpActionResult> Login([FromBody] User user)
            {
                // Verificar si las credenciales son válidas
                var existingUser = await _mongoConnection.Users.Find(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefaultAsync();
                if (existingUser == null)
                    return BadRequest("Credenciales inválidas.");

                // Aquí podrías generar un token JWT y devolverlo como parte de la respuesta
                return Ok(new { Message = "Inicio de sesión exitoso.", Role = existingUser.Role });
            }

            [HttpGet]
            [Authorize]
            [Route("api/users/id/{username}")]
            public async Task<IHttpActionResult> GetUserIdByUsername(string username)
            {
                // Obtén el _id (ObjectId) del usuario por nombre de usuario
                var user = await _mongoConnection.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
                if (user == null)
                    return NotFound();  // Usuario no encontrado

                return Ok(user.Id.ToString()); // Convertir ObjectId a string para devolverlo
            }

            [HttpGet]
            [Authorize]
            [Route("api/users/role")]
            public IHttpActionResult GetUserRole(string username)
            {
                // Obtén el rol del usuario actual
                var user = User as ClaimsPrincipal;
                var roleClaim = user?.FindFirst(ClaimTypes.Role);

                if (roleClaim != null)
                {
                    var userRole = roleClaim.Value;
                    return Ok(userRole);
                }

                return NotFound();
            }

            [HttpGet]
            [Authorize]  // Agrega la autorización según tus necesidades
            [Route("api/users/role/{username}")]
            public async Task<IHttpActionResult> GetRoleByUsername(string username)
            {
                // Busca el usuario por nombre de usuario
                var user = await _mongoConnection.Users.Find(u => u.Username == username).FirstOrDefaultAsync();

                if (user != null)
                {
                    // Devuelve el rol del usuario encontrado
                    return Ok(user.Role);
                }

                return NotFound();  // Usuario no encontrado
            }

        }
    }
}