# Trello Clone API

## Descripción:

Esta API RESTful te permite gestionar un clon de Trello utilizando C# en .NET 8 y una base de datos. La API está dividida en controladores, cada uno de los cuales se encarga de una entidad específica (por ejemplo, cuentas, tableros, tarjetas, listas). La lógica de negocio está separada en servicios para evitar la manipulación directa del contexto de la base de datos. También se utilizan DTOs para transferir datos entre las diferentes capas de la aplicación.

## Tecnologías:

- ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
- ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
- Entity Framework Core
- ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

## Estructura de la base de datos:

### La base de datos se compone de las siguientes tablas:

- Cuenta: Almacena información sobre los usuarios de la aplicación.
- Tablero: Almacena información sobre los tableros de Trello.
- Lista: Almacena información sobre las listas de tareas dentro de un tablero.
- Tarjeta: Almacena información sobre las tarjetas de tareas dentro de una lista.

### Controladores:

- AccountController: Controla las operaciones relacionadas con las cuentas de usuario.
- BoardController: Controla las operaciones relacionadas con los tableros.
- CardController: Controla las operaciones relacionadas con las tarjetas.
- ListController: Controla las operaciones relacionadas con las listas.
- LoginController: Controla el proceso de inicio de sesión.

### DTOs:

- AccountDtoIn: Se utiliza para recibir datos de la API para crear o actualizar cuentas.
- AccountDtoOut: Se utiliza para enviar datos de la API sobre las cuentas.
- BoardDtoIn: Se utiliza para recibir datos de la API para crear o actualizar tableros.
- BoardDtoOut: Se utiliza para enviar datos de la API sobre los tableros.
- LoginDto: Se utiliza para recibir datos de la API para el proceso de inicio de sesión.

### Servicios:

- AccountService: Contiene la lógica de negocio para las operaciones relacionadas con las cuentas.
- BoardService: Contiene la lógica de negocio para las operaciones relacionadas con los tableros.
- CardService: Contiene la lógica de negocio para las operaciones relacionadas con las tarjetas.
- ListService: Contiene la lógica de negocio para las operaciones relacionadas con las listas.

## Uso de la API:

Para usar la API, puedes utilizar cualquier herramienta que te permita realizar solicitudes HTTP. Puedes encontrar más información sobre cómo usar la API en la documentación Swagger.

## Documentación Swagger:

La documentación Swagger está disponible en la siguiente URL:

https://localhost:5001/swagger/index.html

### Inicio rápido:

1. Clona este repositorio en tu máquina local.
2. Restaura las dependencias con dotnet restore.
3. Ejecuta la aplicación con dotnet run.
4. Abre la documentación Swagger en tu navegador web.
5. Explora las diferentes operaciones disponibles en la API.
6. Contribuciones:

## Se agradecen las contribuciones a este proyecto. Si deseas contribuir, puedes hacerlo siguiendo las siguientes pautas:

- Crea una bifurcación de este repositorio.
- Implementa tu cambio en una nueva rama.
- Envía una solicitud de extracción a la rama principal.


### Contacto:

Si tienes alguna pregunta o comentario, puedes ponerte en contacto conmigo a través de la siguiente dirección de correo electrónico:

juanesbs2003@homail.com


## Información adicional:

Este proyecto se puede utilizar como punto de partida para crear tu propia aplicación Trello.
Puedes ampliar la API para agregar nuevas funcionalidades.
Espero que este proyecto te sea útil.