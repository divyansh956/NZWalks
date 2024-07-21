# .NET Core Web API

This is a .NET Core Web API project for managing walks in New Zealand.

## Table of Contents

- [About](#about)
- [Getting Started](#getting-started)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)

## About

This project is a .NET Core Web API that allows users to manage walks in New Zealand. It supports CRUD operations for regions and walks, providing endpoints to create, read, update, and delete data.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/divyansh956/NZWalks.git
   cd NZWalks
   ```

2. Restore the dependencies and apply database migrations:
   ```bash
   dotnet restore
   dotnet ef database update
   ```

3. Update the database connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

## Running the Application

To run the application locally, execute the following command:
```bash
dotnet run
```

## API Endpoints

Here are some of the main API endpoints:

### Regions

- **GET** /api/regions - Get all regions
- **GET** /api/regions/{id} - Get a specific region by ID
- **POST** /api/regions - Create a new region
- **PUT** /api/regions/{id} - Update an existing region
- **DELETE** /api/regions/{id} - Delete a region

### Walks

- **GET** /api/walks - Get all walks
- **GET** /api/walks/{id} - Get a specific walk by ID
- **POST** /api/walks - Create a new walk
- **PUT** /api/walks/{id} - Update an existing walk
- **DELETE** /api/walks/{id} - Delete a walk

### Example Request

```bash
curl -X GET "https://localhost:5001/api/regions" -H "accept: text/plain"
```

## Technologies Used

- .NET Core
- Entity Framework Core
- SQL Server
- Swagger for API documentation
