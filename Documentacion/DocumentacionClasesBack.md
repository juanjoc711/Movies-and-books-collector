# Explicación de las clases principales del back.
## MongoConnection.cs
Este archivo define la clase MongoConnection, responsable de gestionar la conexión a la base de datos MongoDB. Utiliza la biblioteca MongoDB.Driver y proporciona acceso a las colecciones de películas, usuarios y libros.

## MoviesController.cs
El controlador MoviesController implementa operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para la entidad Movie. Proporciona endpoints para obtener todas las películas, buscar por título, obtener por ID, agregar, actualizar y eliminar películas. Utiliza la tecnología .NET Framework y sigue los principios de arquitectura REST.

## BooksController.cs
El controlador BooksController realiza funciones similares al MoviesController, pero para la entidad Book. También implementa operaciones CRUD para libros, incluyendo obtener todas las películas, obtener por ID, agregar, actualizar y eliminar libros. Al igual que MoviesController, sigue los principios de arquitectura REST y utiliza .NET Framework.

## User.cs
Este archivo define la clase User, que representa la entidad de usuario en la base de datos. Contiene propiedades como Id, Username y Password. Esta clase es utilizada para gestionar la autenticación de usuarios en la aplicación.

## FavoriteMovie.cs
La clase FavoriteMovie representa la relación entre usuarios y películas marcadas como favoritas. Contiene propiedades como Id, UserId y MovieId, permitiendo a los usuarios marcar películas como favoritas.

## WebApiConfig.cs
El archivo WebApiConfig configura las rutas para los controladores de películas y libros en la aplicación. Define rutas RESTful para acceder a las operaciones CRUD de las entidades.

## UserController.cs
El controlador UserController implementa operaciones CRUD para la entidad User. Proporciona endpoints para obtener todos los usuarios, obtener por ID, agregar, actualizar y eliminar usuarios. Este controlador se utiliza para gestionar la información de los usuarios, como la autenticación y el registro.

## Movie.cs
El archivo Movie.cs define la clase Movie, que representa la entidad de películas en la aplicación. Contiene propiedades como Id, Title, Year, Director, Genre, Language, y Synopsis. Esta clase se utiliza para estructurar y manipular los datos de las películas.

## Book.cs
El archivo Book.cs define la clase Book, que representa la entidad de libros en la aplicación. Similar a Movie.cs, contiene propiedades como Id, Title, Author. Esta clase estructura la información relacionada con los libros.



