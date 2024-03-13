using System.Web.Http;

namespace Back
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Habilitar el enrutamiento de atributos
            config.MapHttpAttributeRoutes();

            // Rutas para el controlador de películas
            config.Routes.MapHttpRoute(
                name: "MoviesApi",
                routeTemplate: "api/movies/{id}",
                defaults: new { controller = "Movies", id = RouteParameter.Optional }
            );

            // Rutas para el controlador de libros
            config.Routes.MapHttpRoute(
                name: "BooksApi",
                routeTemplate: "api/books/{id}",
                defaults: new { controller = "Books", id = RouteParameter.Optional }
            );

            // Rutas para el controlador de usuarios
            config.Routes.MapHttpRoute(
                name: "UsersApi",
                routeTemplate: "api/users/{id}",
                defaults: new { controller = "Users", id = RouteParameter.Optional }
            );
        }
    }
}