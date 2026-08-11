# Task 08 - Bad Auth Refactor Pack

## Overview

This task refactors an insecure authentication implementation into a safer, maintainable ASP.NET Core Web API authentication system.

The original implementation had security and architecture problems such as plain-text password comparison, fake JWT tokens, direct database access from the controller, returning the full user entity, and missing validation.

The refactored implementation separates authentication logic into **Controller, Service, DTO, Password Hashing, JWT, and Audit Logging** components.

---

## Objectives

* Secure user passwords using BCrypt hashing.
* Generate real JWT access tokens.
* Validate authentication requests.
* Prevent duplicate email registration.
* Restrict user role creation.
* Prevent inactive users from logging in.
* Return safe DTOs instead of database entities.
* Move business logic from the controller to `AuthService`.
* Use asynchronous EF Core operations.
* Log successful and failed authentication attempts.
* Use appropriate HTTP status codes.

---

## Before Refactoring

The original authentication code had several problems:

* Passwords were compared as plain text.
* Passwords were stored without secure hashing.
* Fake tokens were generated.
* Full user entities were returned in API responses.
* `AppDbContext` was used directly inside the controller.
* Business logic was inside the controller.
* No request validation.
* Roles could be submitted freely.
* No duplicate email validation.
* No `IsActive` check.
* Synchronous database operations were used.
* Wrong HTTP status codes were returned.
* No proper authentication/audit logging.

---

## After Refactoring

### Architecture

```text
AuthController
      │
      ▼
IAuthService
      │
      ▼
AuthService
   ┌──┼───────────────┐
   ▼  ▼               ▼
EF Core  PasswordHasher  JwtService
   │
   ▼
SQL Server

AuthService
      │
      ▼
IActivityLogService
      │
      ▼
ActivityLogs
```

---

## Main Components

### AuthController

The controller is responsible only for handling HTTP requests and returning API responses.

Endpoints:

```text
POST /api/auth/register
POST /api/auth/login
GET  /api/auth/me
POST /api/auth/change-password
POST /api/auth/logout
```

Business logic is delegated to `IAuthService`.

---

### AuthService

`AuthService` contains the authentication business logic.

Responsibilities:

* Register users.
* Validate registration data.
* Check duplicate emails.
* Validate roles.
* Hash passwords.
* Authenticate users.
* Verify passwords.
* Check account status.
* Generate JWT tokens.
* Update last login time.
* Log authentication activities.
* Change passwords.

---

### Password Hashing

Passwords are never stored as plain text.

The project uses BCrypt through:

```csharp
PasswordHasher
```

Hashing:

```csharp
passwordHasher.Hash(password)
```

Verification:

```csharp
passwordHasher.Verify(password, hash)
```

---

### JWT Authentication

A real JWT is generated through:

```csharp
JwtService
```

The token contains claims such as:

* User ID
* Email
* Role
* JWT ID (`JTI`)

Example:

```text
sub
email
role
jti
```

The token is returned through the safe `AuthResponse` DTO.

---

## Registration Validation

Registration validates:

* Full name is required.
* Email is required and must be valid.
* Password is required.
* Password must contain at least 8 characters.
* Password must contain uppercase characters.
* Password must contain lowercase characters.
* Password must contain a digit.
* Password and confirmation must match.
* Email must be unique.
* Role must be valid.
* Admin registration is not allowed.
* Instructor registration requires specialization.

Allowed public roles:

```text
Student
Instructor
```

Admin users cannot be created through the public registration endpoint.

---

## Login Security

The login process:

1. Finds the user by email.
2. Checks whether the account exists.
3. Checks whether the account is active.
4. Verifies the password using BCrypt.
5. Updates `LastLoginAt`.
6. Generates a JWT.
7. Records a successful login activity.
8. Returns a safe authentication response.

Invalid credentials are not exposed through detailed error messages.

---

## Authentication Response

The API does not return the complete `User` entity.

Instead, it returns:

```text
AuthResponse
```

Containing safe authentication information such as:

```text
UserId
FullName
Email
Role
AccessToken
ExpiresAt
```

Sensitive information such as the password hash is never returned.

---

## Activity Logging

Authentication actions are integrated with the Activity Log system from Task 07.

Logged authentication actions include:

```text
User Registered
User Logged In
Login Failed
```

The activity log can contain:

```text
UserId
UserRole
Action
EntityName
EntityId
Description
CreatedAt
IpAddress
Metadata
```

Passwords and other sensitive authentication data are not stored in the logs.

---

## API Authorization

Protected endpoints use:

```csharp
[Authorize]
```

JWT role claims are used for role-based authorization.

Example:

```csharp
[Authorize]
```

allows authenticated users to access protected endpoints.

Admin-only functionality can use:

```csharp
[Authorize(Roles = "Admin")]
```

---

## HTTP Status Codes

The refactored API is designed to use appropriate HTTP responses:

| Situation               |                   Status |
| ----------------------- | -----------------------: |
| Successful login        |                 `200 OK` |
| Successful registration | `200 OK` / `201 Created` |
| Invalid request         |        `400 Bad Request` |
| Invalid credentials     |       `401 Unauthorized` |
| Inactive account        |       `401 Unauthorized` |
| Forbidden operation     |          `403 Forbidden` |
| User not found          |          `404 Not Found` |

Exception handling is handled through the project's common exception/middleware system.

---

## Security Improvements

| Before                         | After                         |
| ------------------------------ | ----------------------------- |
| Plain-text password comparison | BCrypt verification           |
| Plain-text password storage    | Hashed password               |
| Fake token                     | Real JWT                      |
| Full entity returned           | `AuthResponse` DTO            |
| DbContext in controller        | `AuthService`                 |
| No validation                  | DTO + service validation      |
| Free role selection            | Role restrictions             |
| No duplicate check             | Duplicate email validation    |
| No active check                | `IsActive` validation         |
| Sync database calls            | Async EF Core                 |
| No audit logging               | ActivityLog integration       |
| Generic `200 OK` errors        | Appropriate HTTP status codes |

---

## Testing

The following scenarios should be tested using Swagger/Postman:

### 1. Successful Registration

```text
POST /api/auth/register
```

Expected:

```text
200 OK
```

---

### 2. Duplicate Email

Register using an existing email.

Expected:

```text
400 Bad Request
```

---

### 3. Invalid Password

Login using an incorrect password.

Expected:

```text
401 Unauthorized
```

---

### 4. Successful Login

Login using valid credentials.

Expected:

```text
200 OK
```

and a valid JWT access token.

---

### 5. Inactive User

Attempt to login with an inactive account.

Expected:

```text
401 Unauthorized
```

---

### 6. Admin Registration

Attempt to register with:

```json
{
  "role": "Admin"
}
```

Expected:

```text
403 Forbidden
```

---

### 7. Current User

Send the JWT to:

```text
GET /api/auth/me
```

Expected:

```text
200 OK
```

with the authenticated user's information.

---

## Deliverables

* Original bad authentication code.
* Refactored authentication implementation.
* Authentication DTOs.
* `AuthService`.
* `PasswordHasher`.
* `JwtService`.
* JWT configuration.
* Activity logging integration.
* Before/after screenshots or notes.
* Swagger/Postman testing evidence.
* At least 5 meaningful Git commits.
* This README.

---

## Conclusion

Task 08 transforms the original insecure authentication implementation into a more production-like ASP.NET Core authentication system using:

```text
ASP.NET Core Web API
EF Core
SQL Server
BCrypt
JWT
DTOs
Service Layer
Authorization
Activity Logging
Async Database Operations
Validation
```

The result provides a cleaner architecture and significantly improves authentication security, maintainability, and traceability.
