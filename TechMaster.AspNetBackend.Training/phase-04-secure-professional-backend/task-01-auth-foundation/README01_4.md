# Task 01 – Authentication Foundation

## Overview

This task introduces authentication and authorization to the Training Center Registration API. A custom authentication system was implemented using ASP.NET Core, Entity Framework Core, JWT (JSON Web Tokens), and secure password hashing.

The goal is to allow authenticated users to securely register, log in, receive JWT access tokens, access protected endpoints, and manage their passwords.

---

# Objectives

* Build a secure authentication foundation.
* Create a custom User entity.
* Implement secure password hashing.
* Generate JWT access tokens.
* Protect API endpoints.
* Return safe authentication responses.
* Track user login activity.

---

# Business Scenario

TechMaster Academy requires real user authentication before accessing the API.

Each user has:

* Login credentials
* Secure password hash
* Assigned role
* Active status
* Audit information

Anonymous access is no longer allowed for protected endpoints.

---

# Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Password Hashing
* Swagger
* Postman

---

# User Entity

The authentication system is built around the custom **User** entity.

| Property     | Description                  |
| ------------ | ---------------------------- |
| Id           | User identifier              |
| FullName     | User full name               |
| Email        | Unique email address         |
| HashPassword | Encrypted password           |
| Role         | User role                    |
| IsActive     | Account status               |
| CreatedAt    | Creation date                |
| UpdatedAt    | Last update                  |
| LastLoginAt  | Last login time              |
| StudentId    | Linked student (optional)    |
| InstructorId | Linked instructor (optional) |

---

# User Roles

The project uses a Role enum.

* Admin
* Instructor
* Student

Students are not allowed to register as Admin.

---

# DTOs

## RegisterRequest

* FullName
* Email
* Password
* ConfirmPassword
* Role

## LoginRequest

* Email
* Password

## ChangePasswordRequest

* CurrentPassword
* NewPassword
* ConfirmPassword

## AuthResponse

* UserId
* FullName
* Email
* Role
* AccessToken
* ExpiresAt

## CurrentUserResponse

* UserId
* FullName
* Email
* Role
* LinkedStudentId
* LinkedInstructorId

---

# Authentication Flow

## Register

1. Receive registration request.
2. Validate request.
3. Check duplicate email.
4. Validate passwords.
5. Prevent Admin self-registration.
6. Hash password.
7. Save user.
8. Return safe response.

---

## Login

1. Receive credentials.
2. Find user by email.
3. Verify account is active.
4. Verify password hash.
5. Update LastLoginAt.
6. Generate JWT.
7. Return access token.

---

## Current User

The authenticated user is identified using JWT claims.

The endpoint returns:

* User ID
* Full Name
* Email
* Role
* Linked Student
* Linked Instructor

---

## Change Password

The authenticated user can:

* Verify current password
* Enter new password
* Confirm password
* Update hashed password
* Save update timestamp

---

# JWT Claims

The generated JWT contains:

* NameIdentifier (User Id)
* Email
* Role
* Expiration

Sensitive information is never included inside the token.

---

# Security Features

* Password hashing
* JWT Authentication
* Protected endpoints
* Unique email validation
* Active account validation
* Safe authentication responses
* Password confirmation validation
* Audit fields

---

# API Endpoints

| Method | Endpoint                  | Description                |
| ------ | ------------------------- | -------------------------- |
| POST   | /api/auth/register        | Register new user          |
| POST   | /api/auth/login           | Login user                 |
| GET    | /api/auth/me              | Current authenticated user |
| POST   | /api/auth/change-password | Change password            |
| POST   | /api/auth/logout          | Logout (JWT client-side)   |
| POST   | /api/auth/refresh-token   | Planned for next task      |

---

# Validation Rules

* Email must be unique.
* Passwords must match.
* Password is never stored in plain text.
* Inactive users cannot login.
* Students cannot register as Admin.
* Invalid credentials return safe error messages.

---

# Testing

The authentication module was tested using:

* Swagger UI
* Postman

Verified scenarios:

* Successful registration
* Duplicate email
* Invalid login
* Successful login
* JWT generation
* Protected endpoint access
* Current user endpoint
* Change password
* Unauthorized access
* Invalid token

---

# Project Structure

```
Authentication
│
├── Controllers
│     └── AuthController
│
├── DTOs
│     ├── RegisterRequest
│     ├── LoginRequest
│     ├── ChangePasswordRequest
│     ├── AuthResponse
│     └── CurrentUserResponse
│
├── Entities
│     ├── User
│     └── UserRole
│
├── Security
│     ├── PasswordHasher
│     ├── JwtSettings
│     ├── JwtService
│     └── IJwtService
│
└── Services
      ├── IAuthService
      └── AuthService
```

---

# Future Improvements

* Refresh Token support
* Token revocation
* Email verification
* Forgot password
* Password reset
* Account lockout
* Refresh token persistence

---

# Status

**Completed**

Task 01 successfully establishes the authentication foundation for Phase 04 by implementing secure registration, login, JWT authentication, password hashing, protected endpoints, and current user management.
