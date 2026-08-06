# Task 01 – Authentication Foundation

## Overview

This task introduces authentication and identity management to the Training Center Registration API using **ASP.NET Core Identity** and **JWT (JSON Web Tokens)**. The goal is to allow real users to securely register, authenticate, and access protected endpoints while following professional backend security practices.

---

# Objectives

* Implement ASP.NET Core Identity.
* Create an `ApplicationUser` entity.
* Securely hash passwords.
* Authenticate users using JWT.
* Support role-based authentication.
* Prevent anonymous access to protected endpoints.
* Provide authenticated user information.
* Allow authenticated users to change their passwords.

---

# Technologies

* ASP.NET Core Web API
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger
* Postman

---

# Authentication Flow

## 1. User Registration

A new user registers by sending:

```http
POST /api/auth/register
```

Request Body

```json
{
  "fullName": "Mohamed Ayman",
  "email": "mohamed@example.com",
  "password": "P@ssw0rd123",
  "confirmPassword": "P@ssw0rd123",
  "role": "Student"
}
```

### Registration Validation

* Full name is required.
* Email must be unique.
* Password and Confirm Password must match.
* Password is never stored as plain text.
* User is created with `IsActive = true`.
* `CreatedAt` is automatically assigned.
* Students cannot register themselves as Admin.

---

## 2. Login

```http
POST /api/auth/login
```

Request

```json
{
  "email": "mohamed@example.com",
  "password": "P@ssw0rd123"
}
```

The login process performs the following:

1. Find the user by email.
2. Verify that the account is active.
3. Verify the password hash using ASP.NET Core Identity.
4. Update `LastLoginAt`.
5. Generate a JWT access token.
6. Return a safe authentication response.

Example Response

```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "userId": "xxxxxxxx",
    "fullName": "Mohamed Ayman",
    "email": "mohamed@example.com",
    "role": "Student",
    "accessToken": "JWT_TOKEN",
    "expiresAt": "2026-08-10T12:00:00Z"
  }
}
```

---

## 3. Current User

```http
GET /api/auth/me
```

Requires a valid Bearer Token.

Returns the authenticated user's information.

Example

```json
{
  "success": true,
  "data": {
    "userId": "xxxxxxxx",
    "fullName": "Mohamed Ayman",
    "email": "mohamed@example.com",
    "role": "Student",
    "linkedStudentId": 3,
    "linkedInstructorId": null
  }
}
```

---

## 4. Change Password

```http
POST /api/auth/change-password
```

Authenticated users can change their password by providing:

* Current Password
* New Password
* Confirm Password

The current password is verified before updating the password hash.

---

## 5. Logout

```http
POST /api/auth/logout
```

Since JWT authentication is stateless, logout currently returns a successful response without storing server-side sessions.

---

# JWT Authentication

After successful login, the API generates a JWT containing only safe identity information.

Included Claims

* User Id (`sub` / `NameIdentifier`)
* Email
* Role
* JWT Id (`jti`)
* Expiration (`exp`)

Sensitive information is **never** included in the token.

The following are never stored inside JWT:

* Password
* Password Hash
* Database Connection String
* Private Notes
* API Keys
* Payment Details

---

# User Roles

The application uses ASP.NET Core Identity Roles.

Available Roles

* Admin
* Instructor
* Student

Roles are stored using Identity tables:

* AspNetUsers
* AspNetRoles
* AspNetUserRoles

---

# Database Tables

Identity automatically creates the following tables:

* AspNetUsers
* AspNetRoles
* AspNetUserRoles
* AspNetUserClaims
* AspNetRoleClaims
* AspNetUserLogins
* AspNetUserTokens

---

# Security Features

* Password hashing using ASP.NET Core Identity.
* Unique email validation.
* JWT authentication.
* Role-based authentication support.
* Active account verification.
* Last login tracking.
* Secure API responses.
* Protected endpoints require authentication.

---

# Implemented Endpoints

| Method | Endpoint                    | Description                      |
| ------ | --------------------------- | -------------------------------- |
| POST   | `/api/auth/register`        | Register a new user              |
| POST   | `/api/auth/login`           | Authenticate user and return JWT |
| GET    | `/api/auth/me`              | Get current authenticated user   |
| POST   | `/api/auth/change-password` | Change current user's password   |
| POST   | `/api/auth/logout`          | Logout (stateless JWT)           |

---

# Project Structure

```
Security/
│
├── JwtSettings.cs
├── JwtService.cs
└── IJwtService.cs

Entities/
└── ApplicationUser.cs

Controllers/
└── AuthController.cs

Services/
├── AuthService.cs
└── IAuthService.cs

DTOs/
└── Auth/
    ├── RegisterRequest.cs
    ├── LoginRequest.cs
    ├── ChangePasswordRequest.cs
    ├── AuthResponse.cs
    └── CurrentUserResponse.cs
```

---

# Evidence

The following evidence was prepared for submission:

* Swagger screenshot – Register endpoint
* Swagger screenshot – Login endpoint
* Postman Register request
* Postman Login request
* Postman Current User request
* JWT token decoded showing claims
* SQL Server Identity tables
* Authentication flow demonstration

---

# Result

Task 01 establishes a secure authentication foundation for the Training Center Registration API. Users authenticate using ASP.NET Core Identity and JWT, passwords are securely hashed, role information is included in JWT claims, and protected endpoints can identify the currently authenticated user while following secure backend development practices.
