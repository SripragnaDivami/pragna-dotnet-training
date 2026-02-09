# Policy Management System API

A comprehensive RESTful API for managing insurance policies, user enrollments, and administrative operations. Built with ASP.NET Core Web API and PostgreSQL.

## Features

- **User Authentication & Authorization**
  - JWT-based authentication
  - Role-based access control (User and Admin roles)
  - Secure password hashing with BCrypt
  - Password validation

- **Policy Management**
  - View active insurance policies
  - Search policies by coverage amount
  - Filter policies by status
  - Admin-only policy creation, updates, and status management

- **Policy Enrollment**
  - Users can enroll in policies
  - Duplicate enrollment prevention
  - Admin approval workflow for enrollments
  - Track enrollment status (Pending, Approved, Rejected)

- **User Management**
  - User registration and login
  - Profile management (Name, Email, Password)
  - View user enrollments
  - Admin operations for user management

- **Global Features**
  - Centralized exception handling
  - Performance monitoring
  - Standardized API responses

## Technologies Used

- **Framework**: ASP.NET Core Web API (.NET 8.0)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 8.0.11
- **Authentication**: JWT Bearer tokens
- **Password Hashing**: BCrypt.Net-Next 4.0.3
- **API Documentation**: Swashbuckle (Swagger) 6.9.0
- **Database Provider**: Npgsql.EntityFrameworkCore.PostgreSQL 8.0.11

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/SripragnaDivami/pragna-dotnet-training.git
cd capstone_policy_management
```

### 2. Database Setup

1. **Install PostgreSQL** if you haven't already
2. **Create a database**:

   ```sql
   CREATE DATABASE policy_management_db;
   ```

3. **Run the SQL scripts** to create tables:
   - Navigate to the `Scripts` folder
   - Execute the following scripts in order:

     ```bash
     # Connect to your PostgreSQL database
     psql -U postgres -d policy_management_db

     # Run the scripts
     \i Scripts/User.sql
     \i Scripts/Policy.sql
     \i Scripts/PolicyEnrollment.sql
     ```

### 3. Configuration

Update the `appsettings.json` file with your database credentials:

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Build the Project

```bash
dotnet build
```

### 6. Run the Application

```bash
dotnet run
```

### 7. Access Swagger Documentation

Open your browser and navigate to swagger UI to explore and test the API endpoints

```


## 📚 API Endpoints

### Authentication (No Authorization Required)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Policies (User Role Required)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/policies` | Get all active policies |
| GET | `/api/policies/{id}` | Get policy by ID |
| GET | `/api/policies/search?minAmount=X&maxAmount=Y` | Search policies by amount |
| GET | `/api/policies/status?isActive=true/false` | Get policies by status |
| POST | `/api/policies/{policyId}/enroll` | Enroll in a policy |

### Admin - Policies (Admin Role Required)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/admin/policies` | Create a new policy |
| PUT | `/api/admin/policies/{id}` | Update policy details |
| PATCH | `/api/admin/policies/{id}/status` | Update policy status |

### Admin - Enrollments (Admin Role Required)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/admin/enrollments` | Get all enrollments |
| GET | `/api/admin/enrollments?status=Pending/Approved/Rejected` | Filter enrollments by status |
| PATCH | `/api/admin/enrollments/{id}/approve` | Approve an enrollment |
| PATCH | `/api/admin/enrollments/{id}/reject` | Reject an enrollment |

### Users (User/Admin Role Required)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/users` | Get all users (Admin only) |
| GET | `/api/users/{id}` | Get user by ID (Admin only) |
| POST | `/api/users` | Create a new user |
| PUT | `/api/users/{id}` | Update user profile |
| DELETE | `/api/users/{id}` | Delete a user |
| GET | `/api/users/my/enrollments?userId={id}` | Get user's enrollments |

##  Authentication

### Register a User

**POST** `/api/auth/register`

### Login

**POST** `/api/auth/login`


### Using the JWT Token

1. Copy the token from the login/register response
2. In Swagger, click the **Authorize** button
3. Enter: `Bearer YOUR_TOKEN_HERE`
4. Click **Authorize**

All subsequent requests will include the authorization header.

##  Password Requirements


## 🎭 User Roles

- **User**: Can view policies, enroll in policies, and manage their own profile
- **Admin**: Full access to all endpoints including policy creation, enrollment approval, and user management

**Note**: New registrations automatically receive the "User" role. Admin users must be created directly in the database.

## Project Structure

```

capstone_policy_management/
├── Controllers/ # API Controllers
│ ├── AuthContoller.cs
│ ├── PolicyController.cs
│ ├── AdminPolicyController.cs
│ ├── AdminEnrollmentController.cs
│ └── UserController.cs
├── Data/ # Database Context
│ └── DbContext.cs
├── DTOs/ # Data Transfer Objects
│ ├── AuthDTOs/
│ ├── PolicyDTOs/
│ ├── PolicyEnrollmentDTOs/
│ └── UserDTOs/
├── Entities/ # Database Models
│ ├── User.cs
│ ├── Policy.cs
│ └── PolicyEnrollment.cs
├── Filters/ # Global Filters
│ ├── GlobalExceptionFilter.cs
│ ├── GlobalResponseFilter.cs
│ └── PerformanceActionFilter.cs
├── Helper/ # Utility Classes
│ └── GenerateJWTToken.cs
│ 
├── Repository/ # Data Access Layer
│ ├── Interfaces/
│ └── Implementations/
├── Services/ # Business Logic Layer
│ ├── Interfaces/
│ └── Implementations/
├── Scripts/ # SQL Scripts
│ ├── User.sql
│ ├── Policy.sql
│ └── PolicyEnrollment.sql
├── appsettings.json # Configuration
└── Program.cs # Application Entry Point

````

##  Error Handling

The API uses a global exception filter that returns standardized error responses:

```json
{
  "errorCode": "BadRequest",
  "message": "Detailed error message",
  "traceId": "unique-trace-id"
}
````

HTTP Status Codes:

- `200 OK` - Success
- `201 Created` - Resource created
- `400 Bad Request` - Validation errors
- `401 Unauthorized` - Invalid credentials or missing token
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server errors
