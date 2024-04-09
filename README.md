# Trello Clone API

## Description:

This RESTful API allows you to manage a Trello clone using C# in .NET 8 and a database. The API is divided into controllers, each of which handles a specific entity (e.g., accounts, boards, cards, lists). Business logic is separated into services to avoid direct manipulation of the database context. DTOs are also used to transfer data between different layers of the application.

## Technologies:

- ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
- ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
- ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
- Entity Framework Core
  
## Database Structure:

### The database consists of the following tables:

- Account: Stores information about application users.
- Board: Stores information about Trello boards.
- List: Stores information about task lists within a board.
- Card: Stores information about task cards within a list.

### Controllers:

- AccountController: Controls operations related to user accounts.
- BoardController: Controls operations related to boards.
- CardController: Controls operations related to cards.
- ListController: Controls operations related to lists.
- LoginController: Controls the login process.

### DTOs:

- AccountDtoIn: Used to receive data from the API to create or update accounts.
- AccountDtoOut: Used to send data from the API about accounts.
- BoardDtoIn: Used to receive data from the API to create or update boards.
- BoardDtoOut: Used to send data from the API about boards.
- LoginDto: Used to receive data from the API for the login process.

### Services:

- AccountService: Contains business logic for operations related to accounts.
- BoardService: Contains business logic for operations related to boards.
- CardService: Contains business logic for operations related to cards.
- ListService: Contains business logic for operations related to lists.

## Using the API:

To use the API, you can use any tool that allows you to make HTTP requests. You can find more information on how to use the API in the Swagger documentation.

## Endpoints:

### Account Controller:

- **GET** `/api/account/all`
- **GET** `/api/account/get/{id}`
- **GET** `/api/account/getbyemail/{email}`
- **POST** `/api/account/create`
- **PUT** `/api/account/update/{id}`
- **DELETE** `/api/account/delete/{id}`

### Board Controller:

- **GET** `/api/board/all`
- **GET** `/api/board/get/{id}`
- **GET** `/api/board/getbyAccount/{id}`
- **POST** `/api/board/create`
- **PUT** `/api/board/update/{id}`
- **DELETE** `/api/board/delete/{id}`

### Card Controller:

- **GET** `/api/card/all`
- **GET** `/api/card/get/{id}`
- **GET** `/api/card/getbyList/{id}`
- **POST** `/api/card/create`
- **PUT** `/api/card/update/{id}`
- **DELETE** `/api/card/delete/{id}`

### List Controller:

- **GET** `/api/list/all`
- **GET** `/api/list/{id}`
- **GET** `/api/list/getbyboardid/{boardId}`
- **POST** `/api/list/create`
- **PUT** `/api/list/update/{id}`
- **DELETE** `/api/list/delete/{id}`

### Login Controller:

- **POST** `/api/login/authenticate`

## Swagger Documentation:

Swagger documentation is available at the following URL:

https://localhost:5001/swagger/index.html

### Quick Start:

1. Clone this repository to your local machine.
2. Restore dependencies with dotnet restore.
3. Run the application with dotnet run.
4. Open the Swagger documentation in your web browser.
5. Explore the different operations available in the API.
6. Contributions:

## Contributions:

Contributions to this project are appreciated. If you wish to contribute, you can do so by following these guidelines:

- Fork this repository.
- Implement your change in a new branch.
- Submit a pull request to the main branch.

### Contact:

If you have any questions or comments, you can contact me via the following email address:

juanesbs2003@hotmail.com

## Additional Information:

- This project can be used as a starting point to create your own Trello application.
- You can extend the API to add new functionalities.
- I hope this project is helpful to you.
