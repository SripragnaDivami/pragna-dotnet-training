# Employee Management API

A RESTful API built with ASP.NET Core for managing employee data. This project demonstrates a clean, layered architecture with Entity Framework Core and PostgreSQL integration.

## Features

- Employee data management
- RESTful API endpoints
- PostgreSQL database integration
- Repository pattern implementation
- Service layer architecture
- Swagger/OpenAPI documentation
- Entity Framework Core for data access

## Architecture

The project follows a layered architecture pattern:

```
BackendApicalls/
├── Controllers/         # API endpoints
├── Services/           # Business logic layer
├── Repositories/       # Data access layer
├── Models/
│   ├── Entities/      # Database models
│   └── DTOs/          # Data Transfer Objects
└── Data/              # Database context
```

### Layers

- **Controllers**: Handle HTTP requests and responses
- **Services**: Contain business logic and orchestrate data flow
- **Repositories**: Abstract data access operations
- **Models**: Define data structures and database entities

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/SripragnaDivami/pragna-dotnet-training.git
cd task2
```

### 2. Configure Database

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=EmployeeDB;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
  }
}
```

### 3. Create Database

Ensure PostgreSQL is running and create the database:

```sql
CREATE DATABASE EmployeeDB;
```

Create the `employees_list` table:

```sql
CREATE TABLE employees_list (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    experience INTEGER NOT NULL,
    city VARCHAR(100),
    department VARCHAR(100) NOT NULL
);
```

### 4. Install Dependencies

```bash
cd BackendApicalls
dotnet restore
```

### 5. Run the Application

```bash
dotnet run
```

## API Endpoints

### Employees

| Method | Endpoint         | Description            |
| ------ | ---------------- | ---------------------- |
| GET    | `/api/employees` | Retrieve all employees |


##  Swagger Documentation

Once the application is running, access the Swagger UI at:

```
https://localhost:5001/swagger
```

This provides interactive API documentation where you can test endpoints directly.

## Database Schema

### Employee Entity

| Column     | Type         | Constraints                 |
| ---------- | ------------ | --------------------------- |
| id         | INTEGER      | Primary Key, Auto-increment |
| first_name | VARCHAR(100) | NOT NULL                    |
| last_name  | VARCHAR(100) | NOT NULL                    |
| experience | INTEGER      | NOT NULL                    |
| city       | VARCHAR(100) | Nullable                    |
| department | VARCHAR(100) | NOT NULL                    |

##  Technologies Used

- **Framework**: ASP.NET Core 10.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core with Npgsql provider
- **API Documentation**: Swagger/Swashbuckle
- **Language**: C# 12

## Packages

- `Microsoft.EntityFrameworkCore`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `Swashbuckle.AspNetCore`





