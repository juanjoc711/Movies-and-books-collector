# Explicación de las clases principales del front.

## AuthoritationHelper.cs
Este archivo contiene la clase AuthorizationHelper, que proporciona funciones relacionadas con la autorización y el rol de usuario. En particular, tiene un método CheckIfUserIsAdmin que verifica si un usuario es administrador consultando la API de usuarios.

## Books.cs
El formulario Books maneja las operaciones CRUD relacionadas con libros. Permite al usuario realizar acciones como crear, buscar, eliminar y actualizar libros. La interfaz se comunica con la API del servidor utilizando solicitudes HTTP y muestra los resultados en un DataGridView.

## CRUD.cs
Este formulario, llamado CRUD, realiza operaciones similares a Books.cs, pero para películas. Permite al usuario realizar acciones como crear, buscar, eliminar y actualizar películas. Al igual que en Books.cs, las operaciones se comunican con la API del servidor mediante solicitudes HTTP.

## Login.cs
Este formulario gestiona el inicio de sesión de los usuarios. Utiliza la API de usuarios para verificar las credenciales y permite a los usuarios registrarse. Además, realiza algunas validaciones y muestra mensajes de éxito o error según sea necesario.

## MenuPrincipal.cs
Este formulario representa el menú principal de la aplicación. Tiene botones para acceder a las secciones de libros (Books) y películas (CRUD). Al hacer clic en estos botones, el usuario es redirigido a los formularios correspondientes.

## Program.cs
Este archivo contiene el punto de entrada principal para la aplicación. Inicia la aplicación y muestra el formulario de inicio de sesión (Login) al usuario.
