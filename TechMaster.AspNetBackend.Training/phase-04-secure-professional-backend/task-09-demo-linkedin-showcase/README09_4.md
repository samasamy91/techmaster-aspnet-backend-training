# Phase 04 – Secure ASP.NET Core API

## TechMaster Academy – ASP.NET Backend Career Training

### Project Overview

This project is the final upgraded version of the **TechMaster Training Center API** developed during **Phase 04**.

The project focuses on transforming a basic ASP.NET Core Web API into a more production-oriented backend with:

* JWT authentication and authorization
* Secure password hashing
* Role-based access control
* Business-rule validation
* Audit/activity logging
* Professional service and controller architecture
* Remote SQL Server database deployment
* Production API verification
* Swagger documentation
* Pagination, filtering, and reporting
* Secure authentication flows

The API manages students, instructors, training tracks, enrollments, payments, authentication, and administrative activity logs.

---

# Phase 04 Features

## 1. Secure Authentication

The authentication system was refactored from insecure authentication code into a service-based implementation.

Implemented:

* User registration
* User login
* Password hashing using BCrypt
* Password verification
* JWT token generation
* JWT claims
* Token expiration
* Current-user endpoint
* Change-password endpoint
* Logout endpoint
* Account active/inactive checking
* Duplicate email validation
* Password validation
* Role validation
* Admin registration restriction

Passwords are never stored as plain text.

---

## 2. JWT Authentication & Authorization

The API uses JWT Bearer authentication.

JWT tokens contain claims such as:

* User ID
* Email
* Role
* Token ID (`jti`)

Protected endpoints require a valid Bearer token.

Example:

```http
Authorization: Bearer <JWT_TOKEN>
```

Role-based authorization is implemented using ASP.NET Core authorization attributes.

Example:

```csharp
[Authorize]
```

and:

```csharp
[Authorize(Roles = "Admin")]
```

---

# 3. Roles & Access Matrix

The application contains different user roles with different permissions.

| Feature / Endpoint    | Admin |     Instructor     |       Student      |
| --------------------- | :---: | :----------------: | :----------------: |
| Register              |   ✅   |          ✅         |          ✅         |
| Login                 |   ✅   |          ✅         |          ✅         |
| Get Current User      |   ✅   |          ✅         |          ✅         |
| Change Password       |   ✅   |          ✅         |          ✅         |
| Admin Activity Logs   |   ✅   |          ❌         |          ❌         |
| Student Operations    |   ✅   |      ❌/Limited     |          ✅         |
| Instructor Operations |   ✅   |          ✅         |          ❌         |
| Track Management      |   ✅   |          ✅         |          ❌         |
| Enrollment Operations |   ✅   | According to rules | According to rules |
| Payment Operations    |   ✅   | According to rules | According to rules |
| Reports               |   ✅   |     Restricted     |          ❌         |

The exact permissions are enforced by the authorization rules configured for each endpoint.

### Wrong Role Behavior

When an authenticated user attempts to access an endpoint that requires a different role, the API returns:

```http
403 Forbidden
```

This was tested as part of the production verification.

---

# 4. Authentication Flow

### Registration

```text
Client
   ↓
POST /api/auth/register
   ↓
AuthController
   ↓
AuthService
   ↓
Validate request
   ↓
Check duplicate email
   ↓
Validate role
   ↓
Hash password
   ↓
Create user
   ↓
Save to database
   ↓
Create activity log
   ↓
Return AuthResponse
```

### Login

```text
Client
   ↓
POST /api/auth/login
   ↓
AuthController
   ↓
AuthService
   ↓
Find user
   ↓
Check IsActive
   ↓
Verify hashed password
   ↓
Generate JWT
   ↓
Create login activity log
   ↓
Return AuthResponse + JWT
```

Failed login attempts are logged safely without storing passwords.

---

# 5. Password Security

The original authentication implementation compared passwords directly.

The insecure approach was replaced with password hashing and verification.

A dedicated `PasswordHasher` is used:

```csharp
public string Hash(string password)
{
    return BCrypt.Net.BCrypt.HashPassword(password);
}
```

Password verification:

```csharp
public bool Verify(string password, string hash)
{
    return BCrypt.Net.BCrypt.Verify(password, hash);
}
```

The API never returns password hashes in its authentication responses.

---

# 6. Authentication Refactor

The original bad authentication implementation had several security and architecture problems.

The following issues were fixed:

1. Plain-text password comparison was removed.
2. Fake JWT tokens were replaced with real JWT tokens.
3. Passwords are hashed using BCrypt.
4. Password verification is performed securely.
5. User entities are not returned directly.
6. `AuthResponse` DTOs are used.
7. Business logic was moved from the controller into `AuthService`.
8. EF Core operations use asynchronous methods.
9. Duplicate email registration is prevented.
10. Email and password validation was added.
11. User roles are validated.
12. Admin registration is restricted.
13. Inactive users cannot log in.
14. JWT claims contain user identity and role information.
15. Failed login attempts are logged.
16. Successful login attempts are logged.
17. Activity logging was integrated into authentication.
18. Protected endpoints require authentication.
19. Role-protected endpoints reject unauthorized roles with `403`.
20. Production secrets are kept outside source-controlled configuration.

---

# 7. Training Center Management

The API manages the main Training Center entities:

* Students
* Instructors
* Training Tracks
* Enrollments
* Payments
* Track Sessions
* Users
* Activity Logs

Relationships are implemented using Entity Framework Core.

---

# 8. Business Rules

Phase 04 also introduced business-rule validation.

Examples include:

### Student Rules

* Student email must be unique.
* Student name is required.
* Deleted students are excluded from normal operations.
* Inactive students cannot perform restricted operations.

### Track Rules

* Track title is required.
* Track code is unique.
* Capacity must be greater than zero.
* Start date must be before end date.
* Track must have an instructor.
* Track capacity cannot be exceeded.
* Closed tracks cannot accept new enrollments.

### Enrollment Rules

* Duplicate active enrollment is prevented.
* Enrollment starts with the appropriate status.
* Enrollment status changes follow business rules.
* Completed enrollments cannot be cancelled directly.
* Cancelled enrollments do not count toward active capacity.

### Payment Rules

* Payment amount must be positive.
* Payment cannot exceed the remaining amount.
* Payment statuses are validated.
* Only successful/paid payments are counted in revenue calculations.
* Failed payments do not activate enrollments.

---

# 9. Querying, Filtering & Pagination

The API contains query and reporting functionality developed during the previous Phase 04 tasks.

Implemented functionality includes:

* Student search
* Student filtering
* Paged student lists
* Track search
* Tracks with available seats
* Unpaid enrollments
* Payments by date range
* Revenue summary
* Revenue per track
* Dashboard summary

Pagination uses a reusable paging structure with a maximum page size of `100`.

Example:

```http
?pageNumber=1&pageSize=10
```

---

# 10. Audit Trail & Activity Logs

An activity logging system was implemented to provide traceability for important actions.

The `ActivityLog` entity records information such as:

* User ID
* User role
* Action
* Entity name
* Entity ID
* Description
* Creation date
* IP address when available
* Metadata when available

Important actions are logged, including:

* User registered
* User logged in
* Failed login attempts
* Track created
* Enrollment requested
* Enrollment status updated
* Payment created
* Payment status updated

The logging system uses an `IActivityLogService` so business services can create audit records without putting logging logic directly inside controllers.

---

# 11. Activity Log Endpoints

Activity logs are protected for administrators.

### Get activity logs

```http
GET /api/admin/activity-logs
```

### Filter by user

```http
GET /api/admin/activity-logs?userId=5
```

### Filter by entity

```http
GET /api/admin/activity-logs?entityName=Payment
```

### Filter by date

```http
GET /api/admin/activity-logs?from=2026-08-01&to=2026-08-31
```

The endpoint supports pagination and filtering.

Only users with the `Admin` role can access the activity log endpoint.

---

# 12. API Architecture

The project was refactored into a professional layered structure.

Main areas include:

```text
Controllers/
Services/
Services/IServices/
DTOs/
Data/
Entities/
Middleware/
Extensions/
Validators/
Common/
Security/
```

### Controllers

Responsible for:

* HTTP requests
* Authorization
* Calling services
* Returning appropriate API responses

### Services

Responsible for:

* Business logic
* Validation
* Database operations
* Authentication logic
* Activity logging

### DTOs

Used to control API input/output models and avoid exposing database entities directly.

### Data

Contains:

* `AppDbContext`
* EF Core configuration
* Database access

### Security

Contains:

* JWT configuration
* JWT service
* Password hashing
* Authentication-related functionality

---

# 13. API Response Structure

The API uses a common response structure such as:

```csharp
ApiResponse<T>
```

This provides consistent responses for successful and failed API operations.

Validation errors are also returned through the common API response format.

---

# 14. Database & EF Core

The application uses:

* ASP.NET Core
* Entity Framework Core
* SQL Server

EF Core migrations are used to create and update the database schema.

The production database is hosted remotely rather than relying only on the local SQL Server database.

---

# 15. Production Deployment

The API was redeployed using **MonsterASP.NET**.

The production environment uses:

```text
ASP.NET Core API
        ↓
MonsterASP.NET
        ↓
Remote SQL Server Database
```

The application was tested against the remote database.

Production deployment verification included:

* Live Swagger
* Remote database connectivity
* Registration
* Login
* JWT authentication
* Protected endpoints
* Role authorization
* Wrong-role rejection
* Activity logs
* Reports

---

# 16. Environment & Secrets

Production secrets should not be committed to GitHub.

The following must remain private:

* Production database password
* Production connection string
* JWT secret key
* Other authentication secrets

Production configuration should be provided through the hosting provider's environment/application settings whenever possible.

Example structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "PRODUCTION_CONNECTION_STRING"
  }
}
```

The actual production password and JWT secret are intentionally not included in this README.

---

# 17. Live API

### Swagger

```text
http://trainingcenterapihosting.runasp.net/swagger/index.html
```

Replace the value above with the actual MonsterASP.NET Swagger URL before publishing this README.

Example:

```text
http://trainingcenterapihosting.runasp.net/swagger/index.html
```

---

# 18. Postman Collection

The API was tested using Swagger and Postman.

### Postman Collection

```text
https://drive.google.com/drive/folders/1DsjXi38Ia1F2A18ZssqDZEyfBxzJPQbf?usp=drive_link
```

Replace this with the GitHub/Postman public collection link.

The collection should demonstrate:

* Register
* Login
* Authenticated request
* Admin endpoint
* Student/Instructor endpoint
* Wrong-role request
* Activity logs
* Reports

---

# 19. Demo Video

The demo video demonstrates the main Phase 04 functionality.

The video should include:

1. Repository/project structure
2. Live Swagger URL
3. User registration
4. Login
5. JWT token
6. Protected endpoint
7. Admin endpoint
8. Student/Instructor role endpoint
9. Wrong-role rejection
10. Activity logs
11. Remote database/production evidence
12. Explanation of what was learned

### Demo Video

```text
https://drive.google.com/drive/folders/1Bhu3yBENX3xXudu8Ld8vUW0KXQxu8psD?usp=drive_link
```

---

# 20. Production Verification

The following scenarios were tested:

| Test                                | Expected Result             |
| ----------------------------------- | --------------------------- |
| Live Swagger opens                  | ✅                           |
| Remote database connection          | ✅                           |
| Register user                       | ✅                           |
| Login                               | ✅                           |
| JWT returned                        | ✅                           |
| Protected endpoint without token    | Rejected                    |
| Protected endpoint with valid token | Allowed                     |
| Admin endpoint with Admin           | Allowed                     |
| Admin endpoint with Student         | `403 Forbidden`             |
| Activity logs                       | Available to Admin          |
| Reports                             | Protected according to role |
| Passwords exposed                   | ❌                           |
| Production secrets committed        | ❌                           |

---

# 21. Known Limitations

* Logout is implemented as an API operation but JWT tokens remain valid until their expiration because JWT authentication is stateless.
* IP address and metadata logging are optional depending on the endpoint implementation.
* Production deployment depends on the availability and configuration of the MonsterASP.NET hosting environment.
* Some production configuration values must be provided through hosting environment settings rather than source control.
* The system can be further improved with refresh tokens, token revocation, centralized correlation IDs, and more advanced monitoring.

---

# 22. Security Improvements Summary

The project started with intentionally insecure authentication code and was refactored into a safer production-oriented implementation.

### Before

```text
Plain-text passwords
Fake token
DbContext in controller
Synchronous database operations
No validation
Full entity returned
No role restrictions
No inactive-user check
Incorrect status handling
No audit trail
```

### After

```text
BCrypt password hashing
Real JWT authentication
Service-based architecture
Async EF Core
DTO validation
Safe AuthResponse
Role-based authorization
Inactive-user protection
Proper API responses
Logging and audit trail
Production deployment
Remote database
```

---

# 23. Phase 04 Learning Outcomes

Through Phase 04, I learned how to move from a basic ASP.NET Core API toward a more realistic backend application.

Key areas learned:

* Secure authentication
* JWT implementation
* Password hashing
* Claims and roles
* Authorization
* Service-layer architecture
* DTO-based API design
* Business-rule validation
* EF Core migrations
* SQL Server
* Pagination and filtering
* Reporting
* Audit trails
* Logging
* Production deployment
* Remote database configuration
* API security
* Swagger and Postman testing
* Production verification

---

# 24. Phase 04 Deliverables

The final Phase 04 project includes:

* ✅ Secure authentication
* ✅ JWT authorization
* ✅ Role-based access control
* ✅ Password hashing
* ✅ Business rules
* ✅ Querying and reporting
* ✅ Pagination
* ✅ Activity/audit logging
* ✅ Admin activity-log endpoint
* ✅ Professional project architecture
* ✅ Remote SQL Server database
* ✅ MonsterASP.NET deployment
* ✅ Live Swagger
* ✅ Postman testing
* ✅ Production verification
* ✅ Security-focused configuration
* ✅ Demo documentation

---

## TechMaster Academy

**ASP.NET Backend Career Training – Phase 04**

This project represents the complete Phase 04 backend journey from refactoring insecure authentication code to building, securing, auditing, and deploying a production-oriented ASP.NET Core API.
