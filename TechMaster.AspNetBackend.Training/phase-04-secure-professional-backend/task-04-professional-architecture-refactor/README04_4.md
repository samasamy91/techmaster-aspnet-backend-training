# Task 04 – Professional Architecture Refactor

## 📌 Overview

This task focuses on refactoring the Training Center API into a **clean, maintainable, and professional ASP.NET Core backend architecture**.

The goal is to organize the project so that when a mentor or developer opens the repository, they can immediately understand:

- Where API requests enter the application.
- Where business rules and workflows are implemented.
- Where database access is handled.
- How API responses are structured.
- How authentication and security are handled.
- How global exceptions are handled.
- How services and authentication are registered.
- How DTOs protect the API from exposing database entities directly.
- How pagination is handled consistently.

The architecture separates responsibilities into clear layers instead of placing business logic, database operations, authentication, and response formatting directly inside controllers.

---

# 🎯 Task Objectives

The main objectives of this task are:

- Refactor the API into a professional folder structure.
- Keep controllers lightweight.
- Move business workflows into services.
- Use service interfaces through Dependency Injection.
- Use DTOs for API input and output.
- Prevent unnecessary direct exposure of entities.
- Introduce a consistent API response structure.
- Support paginated API responses.
- Handle unexpected exceptions through global middleware.
- Separate authentication and security functionality.
- Centralize service registration using extension methods.
- Centralize authentication configuration using extension methods.
- Keep database access organized inside the Data layer.
- Use Entity Framework Core migrations to manage database changes.
- Return appropriate HTTP status codes.
- Avoid exposing stack traces or internal implementation details to clients.

---

# 🏗️ Architecture

The project follows a layered architecture based on **separation of concerns**.

```text
Client
   │
   ▼
Controllers
   │
   ▼
DTOs
   │
   ▼
Services
   │
   ├──────────────► Business Rules
   │
   ├──────────────► Security
   │
   ▼
Data / AppDbContext
   │
   ▼
Entities
   │
   ▼
SQL Server
````

Cross-cutting concerns are handled separately:

```text
Middleware
   └── Global Exception Handling

Extensions
   ├── Service Registration
   └── Authentication Configuration

Common
   ├── ApiResponse
   ├── PagedRequest
   └── PagedResult

Security
   ├── JWT Service
   ├── JWT Settings
   └── Password Hashing
```

---

# 📂 Project Structure

The actual project is organized using the following structure:

```text
ProfessionalArchitectureTrainingCenter/
│
├── Common/
│   ├── ApiResponse.cs
│   ├── PagedRequest.cs
│   └── PagedResult.cs
│
├── Controllers/
│   ├── AuthController.cs
│   ├── EnrollmentController.cs
│   ├── InstructorController.cs
│   ├── PaymentController.cs
│   ├── ReportController.cs
│   ├── StudentController.cs
│   └── TrackController.cs
│
├── Data/
│   ├── AppDbContext.cs
│   └── DatabaseSeeder.cs
│
├── DTOs/
│   ├── Auth/
│   ├── Enrollments/
│   ├── Instructors/
│   ├── Payments/
│   ├── Reports/
│   ├── Students/
│   └── Tracks/
│
├── Entities/
│   ├── Enums/
│   ├── Enrollment.cs
│   └── ...
│
├── Extensions/
│   ├── AuthenticationExtensions.cs
│   └── ServiceCollectionExtensions.cs
│
├── Middleware/
│   └── GlobalExceptionMiddlewares.cs
│
├── Migrations/
│   ├── 20260808232111_ArchTrainingCenter.cs
│   └── AppDbContextModelSnapshot.cs
│
├── Security/
│   ├── IJwtService.cs
│   ├── JwtService.cs
│   ├── JwtSettings.cs
│   └── PasswordHasher.cs
│
├── Services/
│   ├── IServices/
│   ├── AuthService.cs
│   ├── EnrollmentService.cs
│   ├── InstructorService.cs
│   ├── PaymentService.cs
│   ├── ReportService.cs
│   ├── StudentService.cs
│   └── TrackService.cs
│
├── appsettings.json
├── ProfessionalArchitectureTrainingCenter.http
└── Program.cs
```

---

# 🧩 Architecture Responsibilities

## 1. Controllers

Controllers are responsible for handling HTTP requests and returning HTTP responses.

The project contains:

```text
Controllers/
├── AuthController.cs
├── EnrollmentController.cs
├── InstructorController.cs
├── PaymentController.cs
├── ReportController.cs
├── StudentController.cs
└── TrackController.cs
```

Controllers should:

* Receive HTTP requests.
* Receive request DTOs.
* Call the appropriate service.
* Return the appropriate HTTP status code.
* Return standardized API responses.

Controllers should **not contain large business workflows**.

### Example Responsibility

```text
POST /api/Track

Controller
    ↓
TrackService
    ↓
AppDbContext
    ↓
SQL Server
```

Instead of putting all business operations inside the controller:

```text
Controller
    ├── Validate business rules
    ├── Query database
    ├── Modify entities
    ├── Save changes
    ├── Build response
    └── Handle exceptions
```

The controller delegates this work to the service layer.

This keeps controllers small and easier to understand.

---

# 2. Services

Services contain the application's **business logic and workflows**.

The project contains:

```text
Services/
├── IServices/
├── AuthService.cs
├── EnrollmentService.cs
├── InstructorService.cs
├── PaymentService.cs
├── ReportService.cs
├── StudentService.cs
└── TrackService.cs
```

Services are responsible for operations such as:

* Authentication workflows.
* Creating and updating students.
* Managing instructors.
* Creating and updating training tracks.
* Managing enrollments.
* Processing payments.
* Generating reports.
* Applying business rules.
* Communicating with the database through `AppDbContext`.

Example:

```text
EnrollmentController
        ↓
EnrollmentService
        ↓
Business Rules
        ↓
AppDbContext
        ↓
SQL Server
```

This keeps business logic outside controllers and makes the application easier to maintain and test.

---

# 3. Service Interfaces

The project contains an `IServices` folder inside `Services`.

```text
Services/
└── IServices/
```

Service interfaces define the contracts used by controllers and Dependency Injection.

The general flow is:

```text
Controller
    ↓
IService
    ↓
Service
    ↓
AppDbContext
```

For example:

```text
IStudentService
       ↓
StudentService
```

Using interfaces provides:

* Separation of concerns.
* Dependency Injection.
* Reduced coupling.
* Easier unit testing.
* Clear service contracts.
* Easier replacement of service implementations.

---

# 4. DTOs

DTOs (**Data Transfer Objects**) are used to control the data entering and leaving the API.

DTOs are organized by feature:

```text
DTOs/
├── Auth/
├── Enrollments/
├── Instructors/
├── Payments/
├── Reports/
├── Students/
└── Tracks/
```

This organization keeps related request and response models together.

Examples of DTO responsibilities include:

```text
Authentication DTOs
    ↓
Login / Registration data

Student DTOs
    ↓
Create / Update / Response data

Track DTOs
    ↓
Create / Update / Response data

Enrollment DTOs
    ↓
Enrollment request / response data

Payment DTOs
    ↓
Payment request / response data

Report DTOs
    ↓
Report output data
```

### Why DTOs are used

DTOs help to:

* Prevent unnecessary exposure of database entities.
* Hide internal properties.
* Control which fields clients can submit.
* Control which fields clients receive.
* Reduce over-posting risks.
* Define clear API contracts.
* Separate the database model from the API model.

---

# 5. Entities

Entities represent the application's database/domain model.

The project contains an `Entities` folder:

```text
Entities/
├── Enums/
├── Enrollment.cs
└── ...
```

Entities represent the application's database structures and relationships.

Examples include:

```text
Student
Instructor
TrainingTrack
Enrollment
Payment
```

Entities are used internally by Entity Framework Core.

Sensitive API endpoints should not unnecessarily return entities directly.

Instead:

```text
Entity
   ↓
Mapping / Projection
   ↓
DTO
   ↓
ApiResponse
   ↓
Client
```

This provides better control over the information exposed by the API.

---

# 6. Data

The `Data` folder contains database-related components.

```text
Data/
├── AppDbContext.cs
└── DatabaseSeeder.cs
```

## AppDbContext

`AppDbContext` is responsible for:

* Database connection.
* Entity Framework Core configuration.
* DbSets.
* Entity relationships.
* EF Core queries.
* Database persistence.

The service layer communicates with the database through `AppDbContext`.

## DatabaseSeeder

`DatabaseSeeder` is responsible for providing initial development/test data when required.

Keeping database seeding separate from controllers keeps the application startup and business logic cleaner.

---

# 7. Middleware

Global concerns that apply across the API are handled through middleware.

The project contains:

```text
Middleware/
└── GlobalExceptionMiddlewares.cs
```

The global exception middleware is responsible for catching unexpected exceptions and converting them into a controlled API response.

Instead of returning internal implementation details such as:

```text
System.NullReferenceException
at StudentService.cs:line 42
```

the API returns a safe response:

```json
{
  "success": false,
  "message": "An unexpected error occurred.",
  "data": null,
  "errors": []
}
```

This prevents stack traces and internal implementation details from being exposed to API clients.

---

# 🛡️ Global Exception Flow

```text
HTTP Request
     │
     ▼
Controller
     │
     ▼
Service
     │
     ▼
Unexpected Exception
     │
     ▼
GlobalExceptionMiddleware
     │
     ▼
ApiResponse
     │
     ▼
HTTP Response
```

Centralizing exception handling also prevents repeating large `try/catch` blocks throughout controllers.

---

# 8. Common

The `Common` folder contains reusable structures shared throughout the application.

```text
Common/
├── ApiResponse.cs
├── PagedRequest.cs
└── PagedResult.cs
```

These classes provide consistency for:

* API responses.
* Pagination requests.
* Pagination results.

---

# 📦 Standard API Response

A professional API should not return random response shapes from every endpoint.

The project uses a common API response structure.

## Successful Response

Example:

```json
{
  "success": true,
  "message": "Track created successfully.",
  "data": {
    "id": 12,
    "title": "ASP.NET Backend Career Training",
    "status": "Open"
  },
  "errors": []
}
```

### Response Properties

| Property  | Purpose                                   |
| --------- | ----------------------------------------- |
| `success` | Indicates whether the operation succeeded |
| `message` | Human-readable result message             |
| `data`    | Returned data                             |
| `errors`  | Validation or operation errors            |

---

# ❌ Error Response

Validation and business errors use the same response structure.

Example:

```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": [
    "Track capacity must be greater than zero.",
    "Start date must be before end date."
  ]
}
```

This makes errors predictable and easier for API consumers to handle.

---

# 📄 Pagination

The project includes reusable pagination structures:

```text
PagedRequest.cs
PagedResult.cs
```

`PagedRequest` represents pagination input such as:

```text
Page Number
Page Size
```

`PagedResult` represents paginated output such as:

```text
Items
Page Number
Page Size
Total Count
Total Pages
```

Example:

```json
{
  "success": true,
  "message": "Students retrieved successfully.",
  "data": {
    "items": [],
    "pageNumber": 1,
    "pageSize": 10,
    "totalCount": 50,
    "totalPages": 5
  },
  "errors": []
}
```

---

# 🔐 Security

Authentication and password-related functionality is separated into the `Security` folder.

```text
Security/
├── IJwtService.cs
├── JwtService.cs
├── JwtSettings.cs
└── PasswordHasher.cs
```

This separates security responsibilities from controllers and general business services.

---

## JWT Service

`JwtService` is responsible for JWT-related functionality.

The authentication flow is:

```text
Login Request
     │
     ▼
AuthController
     │
     ▼
AuthService
     │
     ├──────────────► PasswordHasher
     │
     ▼
JwtService
     │
     ▼
JWT Token
     │
     ▼
Client
```

---

## IJwtService

`IJwtService` defines the contract for JWT functionality.

This follows the same abstraction approach used by the application services.

```text
IJwtService
    ↓
JwtService
```

This makes the authentication implementation easier to manage and test.

---

## JwtSettings

`JwtSettings` provides a structured configuration model for JWT settings.

JWT configuration is kept separate from business logic.

---

## PasswordHasher

`PasswordHasher` is responsible for password hashing.

The API does not need to store raw passwords.

The general authentication process is:

```text
Password
    ↓
PasswordHasher
    ↓
Password Hash
    ↓
Database
```

During login, the supplied password can be checked against the stored password hash.

---

# 🔑 Authentication Flow

```text
Client
  │
  │ Login
  ▼
AuthController
  │
  ▼
AuthService
  │
  ├── PasswordHasher
  │
  └── JwtService
          │
          ▼
       JWT Token
          │
          ▼
        Client
```

For protected endpoints:

```text
Client
  │
  │ Authorization: Bearer JWT
  ▼
JWT Authentication
  │
  ▼
Authorization
  │
  ▼
Controller
  │
  ▼
Service
```

---

# 🔌 Extensions

The project uses extension methods to keep `Program.cs` clean and organized.

```text
Extensions/
├── AuthenticationExtensions.cs
└── ServiceCollectionExtensions.cs
```

## ServiceCollectionExtensions

`ServiceCollectionExtensions` is used to organize application service registration.

Instead of putting all dependency registrations directly inside `Program.cs`:

```text
Program.cs
    ↓
AddApplicationServices()
    ↓
Register Services
    ↓
Dependency Injection Container
```

This keeps the startup configuration easier to understand.

---

## AuthenticationExtensions

`AuthenticationExtensions` organizes authentication and authorization configuration.

Conceptually:

```text
Program.cs
    ↓
Authentication Extension
    ↓
JWT Authentication
    ↓
Authorization
```

This keeps authentication configuration separate from the main application startup file.

---

# 🔄 Request Flow

A typical request follows this flow:

```text
HTTP Request
     │
     ▼
Controller
     │
     ▼
DTO
     │
     ▼
Service
     │
     ├── Business Rules
     ├── Validation
     └── Authorization Checks
     │
     ▼
AppDbContext
     │
     ▼
SQL Server
     │
     ▼
Entity
     │
     ▼
DTO
     │
     ▼
ApiResponse
     │
     ▼
HTTP Response
```

Unexpected exceptions are handled by:

```text
GlobalExceptionMiddlewares
```

---

# 🗄️ Database Flow

Database operations follow the service/data architecture:

```text
Controller
    ↓
Service
    ↓
AppDbContext
    ↓
Entity Framework Core
    ↓
SQL Server
```

Controllers do not directly manage database operations.

This keeps database-related responsibilities separated from HTTP handling.

---

# 🛡️ Error Handling Flow

```text
Request
   ↓
Controller
   ↓
Service
   ↓
Exception
   ↓
GlobalExceptionMiddleware
   ↓
Standard ApiResponse
   ↓
Client
```

The client receives a controlled error response instead of internal stack traces.

---

# 🗃️ Entity Framework Core Migrations

The project contains an EF Core `Migrations` folder:

```text
Migrations/
├── 20260808232111_ArchTrainingCenter.cs
└── AppDbContextModelSnapshot.cs
```

Migrations are used to track changes to the database schema.

The general workflow is:

```text
Entity Changes
      ↓
EF Core Migration
      ↓
Database Schema Update
      ↓
SQL Server
```

This allows database schema changes to be version-controlled with the project.

---

# 📊 HTTP Status Codes

The API follows appropriate HTTP status codes.

| Status Code                 | Usage                                      |
| --------------------------- | ------------------------------------------ |
| `200 OK`                    | Successful retrieval/update                |
| `201 Created`               | Resource successfully created              |
| `204 No Content`            | Successful operation with no response body |
| `400 Bad Request`           | Invalid request or validation failure      |
| `401 Unauthorized`          | Authentication required or invalid         |
| `403 Forbidden`             | User does not have permission              |
| `404 Not Found`             | Resource does not exist                    |
| `500 Internal Server Error` | Unexpected server error                    |

The exact status code depends on the operation and business scenario.

---

# 🧱 Separation of Responsibilities

The architecture follows the principle that each component should have a clear responsibility.

| Layer       | Responsibility                             |
| ----------- | ------------------------------------------ |
| Controllers | HTTP request/response handling             |
| Services    | Business workflows and rules               |
| IService    | Service contracts                          |
| DTOs        | API data contracts                         |
| Entities    | Database/domain models                     |
| Data        | Database access and EF Core                |
| Middleware  | Global exception handling                  |
| Security    | JWT and password security                  |
| Extensions  | Application configuration and registration |
| Common      | Shared response and pagination structures  |
| Migrations  | Database schema versioning                 |

---

# 🚫 Problems Avoided

This refactor prevents common backend architecture problems.

### ❌ Huge Controllers

Business logic is moved into services.

### ❌ Direct Entity Exposure

DTOs are used to control API input and output.

### ❌ Random Response Shapes

`ApiResponse` provides a consistent response format.

### ❌ Repeated Exception Handling

Global exception handling is centralized in middleware.

### ❌ Authentication Mixed With Business Logic

JWT and password functionality are isolated inside `Security`.

### ❌ Large Program.cs

Extension methods organize service registration and authentication configuration.

### ❌ Tight Coupling

Interfaces and Dependency Injection reduce coupling between controllers and service implementations.

### ❌ Database Logic in Controllers

Database operations are handled through services and `AppDbContext`.

### ❌ Uncontrolled API Responses

The common response structure provides predictable responses across endpoints.

---

# 🧪 Testing

The API can be tested using:

* Swagger UI
* Postman
* SQL Server Management Studio

Important scenarios include:

## Successful Request

```text
Request
   ↓
Controller
   ↓
Service
   ↓
Database
   ↓
ApiResponse
   ↓
200 / 201 Response
```

---

## Validation Failure

```text
Invalid Request
   ↓
Validation / Business Rule
   ↓
ApiResponse
   ↓
400 Bad Request
   ↓
Readable Errors
```

Example:

```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": [
    "Invalid request data."
  ]
}
```

---

## Resource Not Found

```text
Request
   ↓
Controller
   ↓
Service
   ↓
Resource Not Found
   ↓
404 Not Found
```

---

## Unauthorized Request

```text
Request
   ↓
JWT Authentication
   ↓
401 Unauthorized
```

---

## Forbidden Request

```text
Authenticated User
   ↓
Authorization
   ↓
403 Forbidden
```

---

## Unexpected Exception

```text
Request
   ↓
Controller
   ↓
Service
   ↓
Unexpected Exception
   ↓
GlobalExceptionMiddleware
   ↓
500 Internal Server Error
```


---

# 📁 Architecture Summary

The final architecture can be summarized as:

```text
                         ┌────────────────────┐
                         │       Client       │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │    Controllers     │
                         │    HTTP Layer      │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │       DTOs         │
                         │   API Contracts    │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │      Services      │
                         │  Business Logic    │
                         └─────────┬──────────┘
                                   │
                    ┌──────────────┼──────────────┐
                    │              │              │
                    ▼              ▼              ▼
              ┌──────────┐  ┌──────────┐  ┌────────────┐
              │ Security │  │ Business │  │   Common   │
              │   JWT    │  │  Rules   │  │  Responses │
              └──────────┘  └──────────┘  └────────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │    AppDbContext     │
                         │       Data          │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │      Entities      │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │     SQL Server     │
                         └────────────────────┘


          Cross-Cutting Concerns
          ───────────────────────

          GlobalExceptionMiddlewares
                    │
                    ▼
          Global Error Handling

          AuthenticationExtensions
                    │
                    ▼
          JWT Authentication

          ServiceCollectionExtensions
                    │
                    ▼
          Dependency Injection
```

---

# 🏁 Conclusion

Task 04 transforms the Training Center API into a more **professional, maintainable, and reviewable backend codebase**.

The architecture clearly separates responsibilities:

```text
Controllers
    ↓
Handle HTTP Requests

DTOs
    ↓
Define API Contracts

Services
    ↓
Handle Business Logic

Data / AppDbContext
    ↓
Handle Database Access

Entities
    ↓
Represent Database Models

Security
    ↓
Handle JWT and Password Security

Middleware
    ↓
Handle Global Exceptions

Extensions
    ↓
Handle Application Registration and Configuration

Common
    ↓
Provide Shared Responses and Pagination

Migrations
    ↓
Track Database Schema Changes
```

The main architectural principle is:

> **Controllers handle HTTP, Services handle business logic, DTOs handle API contracts, Data handles persistence, Security handles authentication, and Middleware handles global errors.**

This separation makes the project easier to:

* Understand
* Maintain
* Test
* Debug
* Review
* Extend
* Scale

