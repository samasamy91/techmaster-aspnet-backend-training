# Task 05 — Validation, Errors & Logging

Task 05 focuses on making the Training Center API more production-like by introducing proper validation, centralized error handling, safe API responses, and application logging.

The main goal is to ensure that:

* Invalid requests are rejected correctly.
* Business rules are enforced in the service layer.
* Unauthorized users receive `401`.
* Forbidden users receive `403`.
* Missing resources return `404`.
* Invalid requests return `400`.
* Unexpected exceptions are handled by global middleware.
* Internal exceptions are logged without exposing sensitive information.
* Login, payment, and enrollment operations can be traced through logs.
* Passwords, password hashes, JWT tokens, and other secrets are never logged.

---

# 2. Objectives

This task implements the following production-oriented features:

### Validation

* Request validation
* Business rule validation
* Role validation
* Ownership validation
* Date validation
* Amount validation
* Entity existence validation
* Duplicate data validation

### Error Handling

* Global exception middleware
* Safe error responses
* `400 Bad Request`
* `401 Unauthorized`
* `403 Forbidden`
* `404 Not Found`
* `500 Internal Server Error`

### Logging

* Login success/failure logging
* Payment update logging
* Enrollment change logging
* Exception logging
* Warning logs for business rule violations
* No passwords or JWT tokens in logs

---

# 3. Project Structure

The Task 05 implementation uses the following structure:

```text
TrainingCenter.Api
│
├── Controllers
│
├── Services
│   ├── AuthService.cs
│   ├── StudentService.cs
│   ├── TrackService.cs
│   ├── PaymentService.cs
│   ├── EnrollmentService.cs
│   └── ...
│
├── DTOs
│
├── Entities
│
├── Data
│   └── AppDbContext.cs
│
├── Middlewares
│   └── GlobalExceptionMiddleware.cs
│
├── Common
│   └── ApiResponse.cs
│
└── ...
```

---

# 4. Validation Strategy

Validation is divided into different levels.

## 4.1 Request Validation

Request DTOs validate incoming API data before it reaches the business logic.

Examples include:

* Required fields
* Valid email format
* Valid password format
* Positive numbers
* Valid enum values
* Required dates

---

## 4.2 Business Rule Validation

Business rules are implemented mainly in the service layer.

For example:

```csharp
if (emailExists)
{
    throw new BusinessRuleException("Email already exists");
}
```

Another example:

```csharp
if (request.Capacity <= 0)
{
    throw new BusinessRuleException(
        "Capacity must be greater than zero");
}
```

This keeps business rules outside the controllers.

---

# 5. Validation Rule Bank

At least 20 validation/business rules are required for this task.

The project implements rules across registration, tracks, enrollments, payments, sessions, and profiles.

## Registration Rules

1. Full name is required.
2. Email is required.
3. Email must be valid.
4. Email must be unique.
5. Password is required.
6. Password confirmation must match.
7. Admin users cannot register through the public registration endpoint.
8. User role must be valid.
9. Instructor specialization is required for instructor registration.

## Track Rules

10. Track title is required.
11. Track code must be unique.
12. Track capacity must be greater than zero.
13. Instructor must exist.
14. Instructor must be active where required.
15. Track start date must be before the end date.
16. A track with active enrollments cannot be deleted.
17. Deleted tracks cannot be accessed as normal tracks.

## Enrollment Rules

18. Student must exist.
19. Track must exist.
20. Duplicate active enrollment is not allowed.
21. Track capacity cannot be exceeded.
22. Closed tracks cannot accept new enrollments.
23. Deleted/inactive students cannot create active enrollments where the business rule disallows it.

## Payment Rules

24. Enrollment must exist.
25. Payment amount must be positive.
26. Payment cannot exceed the remaining amount.
27. Payment status must be valid.
28. Paid payments can activate the enrollment.
29. Failed payments must not activate the enrollment.

## Session / Ownership Rules

30. Session title is required.
31. Session date must be valid.
32. Session must belong to the correct track.
33. Instructor can access only tracks assigned to that instructor.

## Profile Rules

34. Student full name is required.
35. Student can update only permitted profile fields.
36. Student cannot change their role through profile update.

The project therefore contains **more than the minimum 20 required validation/business rules**.

---

# 6. Custom Exceptions

The project uses custom exceptions to distinguish expected application errors.

Examples:

```csharp
throw new NotFoundException("Student not found");
```

```csharp
throw new BusinessRuleException("Email already exists");
```

```csharp
throw new UnauthorizedAccessException("Invalid token");
```

This allows the global middleware to convert exceptions into the correct HTTP response.

---

# 7. Global Exception Middleware

The project uses:

```text
Middlewares/GlobalExceptionMiddleware.cs
```

The middleware is responsible for centralized exception handling.

### Main responsibilities

* Catch unhandled exceptions.
* Log exceptions.
* Convert known exceptions into suitable HTTP status codes.
* Return consistent API responses.
* Prevent controllers from containing repetitive `try/catch` blocks.
* Prevent internal stack traces from being exposed to clients.

---

## 7.1 Middleware Implementation

The middleware handles:

```csharp
catch (NotFoundException ex)
```

as:

```text
404 Not Found
```

Business rule exceptions:

```csharp
catch (BusinessRuleException ex)
```

as:

```text
400 Bad Request
```

Unauthorized access:

```csharp
catch (UnauthorizedAccessException ex)
```

as an authentication/authorization error.

Unexpected exceptions are handled by:

```csharp
catch (Exception ex)
```

and logged using:

```csharp
logger.LogError(ex, "Unexpected error occurred");
```

---

# 8. Safe Error Responses

The API does not expose internal exception details for unexpected errors.

For an unexpected exception, the client receives:

```json
{
  "success": false,
  "message": "An unexpected error occurred.",
  "data": null,
  "errors": [
    "An internal server error occurred. Please try again later."
  ]
}
```

The actual exception is written to the application logs.

This provides a separation between:

```text
Client
   ↓
Safe error message
```

and:

```text
Application logs
   ↓
Detailed exception information
```

---

# 9. HTTP Error Strategy

| Situation                            | Status Code | Response                        |
| ------------------------------------ | ----------: | ------------------------------- |
| Missing resource                     |       `404` | Resource not found              |
| Invalid request                      |       `400` | Validation/business rule errors |
| Missing/invalid authentication       |       `401` | Authentication required         |
| Wrong role / insufficient permission |       `403` | Access denied                   |
| Unexpected exception                 |       `500` | Safe generic error              |

---

# 10. 404 — Resource Not Found

Services throw `NotFoundException` when the requested resource does not exist.

Example:

```csharp
var student = await context.Students
    .FirstOrDefaultAsync(s => s.StudentId == id && !s.IsDeleted);

if (student == null)
{
    throw new NotFoundException("Student not found");
}
```

The middleware converts this to:

```text
HTTP 404 Not Found
```

Example response:

```json
{
  "success": false,
  "message": "Student not found",
  "data": null,
  "errors": [
    "Student not found"
  ]
}
```

---

# 11. 400 — Business Validation

Business rule violations use:

```csharp
throw new BusinessRuleException("...");
```

Examples:

```csharp
throw new BusinessRuleException("Email already exists");
```

```csharp
throw new BusinessRuleException(
    "Capacity must be greater than zero");
```

```csharp
throw new BusinessRuleException(
    "Track code already exists");
```

These are returned as:

```text
HTTP 400 Bad Request
```

This prevents business validation errors from incorrectly appearing as `500 Internal Server Error`.

---

# 12. 401 — Unauthorized

Authentication failures are handled separately.

Examples include:

* No JWT token.
* Invalid JWT token.
* Invalid user identity.
* Missing user identity claims.

Example:

```csharp
throw new UnauthorizedAccessException("Invalid token");
```

The API should return:

```text
401 Unauthorized
```

with a safe response such as:

```json
{
  "success": false,
  "message": "Authentication required"
}
```

---

# 13. 403 — Forbidden

A user may be authenticated but still not have permission to access a resource.

For example, an instructor attempting to access another instructor's track.

The service verifies ownership:

```csharp
if (track.InstructorId != instructor.InstructorId)
{
    throw new BusinessRuleException(
        "You are not authorized to access this track.");
}
```

Role-based authorization is also used for protected endpoints.

Expected result:

```text
403 Forbidden
```

Example:

```json
{
  "success": false,
  "message": "Access denied"
}
```

---

# 14. Ownership Validation

The project includes ownership checks.

For example, when an instructor requests students for a track:

```csharp
if (user.IsInRole("Admin"))
{
    return await GetStudentsForTrack(trackId);
}
```

For instructors, the system identifies the instructor from the authenticated user's email and checks:

```csharp
if (track.InstructorId != instructor.InstructorId)
{
    throw new BusinessRuleException(
        "You are not authorized to access this track.");
}
```

This prevents one instructor from accessing another instructor's data.

---

# 15. Authentication Validation

The authentication service validates:

### Registration

```csharp
if (request.Password != request.ConfirmPassword)
    throw new BusinessRuleException("Passwords dont match");
```

```csharp
if (emailExists)
    throw new BusinessRuleException("Email already exists");
```

```csharp
if (!Enum.TryParse<UserRole>(
    request.Role,
    true,
    out var role))
{
    throw new BusinessRuleException("Invalid role");
}
```

```csharp
if (role == UserRole.Admin)
    throw new BusinessRuleException(
        "Cannot register as Admin");
```

### Login

The service validates:

* User existence.
* Account status.
* Password correctness.

Example:

```csharp
if (!passwordHasher.Verify(
    request.Password,
    user.HashPassword))
{
    throw new BusinessRuleException(
        "Invalid email or password");
}
```

---

# 16. Track Validation

`TrackService` validates important track rules.

### Unique track code

```csharp
bool codeExists =
    await context.TrainingTracks
        .AnyAsync(t => t.Code == request.Code);

if (codeExists)
{
    throw new BusinessRuleException(
        "Track code already exists");
}
```

### Instructor existence

```csharp
bool instructorExists =
    await context.Instructors
        .AnyAsync(i =>
            i.InstructorId == request.InstructorId);

if (!instructorExists)
{
    throw new NotFoundException(
        "Instructor not found");
}
```

### Capacity

```csharp
if (request.Capacity <= 0)
{
    throw new BusinessRuleException(
        "Capacity must be greater than zero");
}
```

### Active enrollment deletion protection

```csharp
if (hasActiveEnrollments)
{
    throw new BusinessRuleException(
        "Cannot delete a track with active enrollments");
}
```

---

# 17. Payment Validation

`PaymentService` validates payment-related resources.

A payment cannot be created if its enrollment does not exist:

```csharp
if (enrollment == null)
{
    throw new NotFoundException(
        "Enrollment not found");
}
```

When a payment becomes `Paid`, the associated enrollment becomes active:

```csharp
if (payment.PaymentStatus == PaymentStatus.Paid)
{
    payment.Enrollment.Status =
        EnrollmentStatus.Active;
}
```

Payment status changes are also logged.

---

# 18. Student Validation

`StudentService` validates student data.

### Unique email

```csharp
var emailExists =
    await context.Students.AnyAsync(
        s => s.Email == request.Email &&
             !s.IsDeleted);

if (emailExists)
{
    throw new BusinessRuleException(
        "Email already exists");
}
```

### Student existence

```csharp
if (student == null)
{
    throw new NotFoundException(
        "Student not found");
}
```

### Profile ownership

The current student is identified from the authenticated user's claims.

```csharp
var email =
    user.FindFirst(ClaimTypes.Email)?.Value;
```

This prevents users from modifying arbitrary student records through the profile endpoint.

---

# 19. Logging Strategy

The application uses ASP.NET Core's built-in:

```csharp
ILogger<T>
```

Logging is used for:

* Warnings
* Informational events
* Exceptions

---

# 20. Exception Logging

The global middleware logs unexpected exceptions:

```csharp
logger.LogError(
    ex,
    "Unexpected error occurred");
```

Known problems are logged as warnings:

```csharp
logger.LogWarning(
    "Resource not found: {Message}",
    ex.Message);
```

```csharp
logger.LogWarning(
    "Business rule violation: {Message}",
    ex.Message);
```

This allows developers to investigate problems without exposing internal information to API clients.

---

# 21. Login Logging

The authentication service logs successful and failed login attempts.

Example:

```csharp
logger.LogWarning(
    "Login failed for user {UserId}: invalid password",
    user.Id);
```

Successful login:

```csharp
logger.LogInformation(
    "Login successful for user {UserId} with role {Role}",
    user.Id,
    user.Role);
```

### Sensitive information is not logged.

The application does **not** log:

```text
Password
Password hash
JWT token
Refresh token
```

---

# 22. Payment Logging

Payment status changes should be logged.

Example:

```csharp
logger.LogInformation(
    "Payment {PaymentId} status changed from {OldStatus} to {NewStatus}",
    paymentId,
    oldStatus,
    payment.PaymentStatus);
```

This makes payment changes traceable during debugging and auditing.

---

# 23. Enrollment Logging

Enrollment creation/status changes should also be logged.

Recommended format:

```csharp
logger.LogInformation(
    "Enrollment {EnrollmentId} status changed from {OldStatus} to {NewStatus}",
    enrollment.EnrollmentId,
    oldStatus,
    enrollment.Status);
```

This provides an audit trail for important enrollment changes.

---

# 24. Sensitive Data Protection

The logging strategy follows the rule:

> Log what happened, not secrets used to make it happen.

The following information must never be logged:

* Plain-text passwords
* Password hashes
* JWT access tokens
* Refresh tokens
* Authorization headers
* Database passwords
* Connection strings containing credentials

Safe examples:

```text
Login successful for user 10
```

```text
Payment 15 changed from Pending to Paid
```

```text
Student 8 was not found
```

Unsafe example:

```text
User password is MyPassword123
```

---

# 25. Error Handling Flow

The API follows this flow:

```text
HTTP Request
     │
     ▼
Controller
     │
     ▼
Service
     │
     ├── Validation Error
     │       │
     │       ▼
     │   BusinessRuleException
     │
     ├── Missing Resource
     │       │
     │       ▼
     │   NotFoundException
     │
     └── Unexpected Exception
             │
             ▼
       GlobalExceptionMiddleware
             │
             ├── Log exception
             │
             └── Safe API response
```

---

# 26. Why Global Middleware?

Without global middleware, controllers may contain repeated code such as:

```csharp
try
{
    ...
}
catch (Exception ex)
{
    ...
}
```

This produces duplicated and inconsistent error handling.

With the middleware:

```text
Controller
    ↓
Service
    ↓
Exception
    ↓
GlobalExceptionMiddleware
    ↓
Consistent response
```

This makes the application easier to maintain.

---

# 27. Error Handling Test Cases

The following test cases are required by Task 05.

| Test Case            | Expected Status | Expected Result             |
| -------------------- | --------------: | --------------------------- |
| Missing resource ID  |           `404` | Resource not found          |
| Invalid request body |           `400` | Validation errors           |
| Duplicate email      |           `400` | Email already exists        |
| Duplicate track code |           `400` | Track code already exists   |
| Invalid capacity     |           `400` | Capacity validation message |
| No token             |           `401` | Authentication required     |
| Wrong role           |           `403` | Access denied               |
| Wrong resource owner |           `403` | Access denied               |
| Unexpected exception |           `500` | Safe generic error          |

---

# 28. Required Evidence

## Evidence 1 — Validation Error

Swagger/Postman request with invalid data.

Example:

```json
{
  "fullName": "",
  "email": "invalid-email"
}
```

Expected:

```text
400 Bad Request
```

Take a screenshot showing the validation error.

---

## Evidence 2 — Unauthorized Request

Call a protected endpoint without a JWT token.

Expected:

```text
401 Unauthorized
```

Screenshot should show:

```text
401
Authentication required
```

---

## Evidence 3 — Forbidden Request

Login with a valid user who does not have the required role or ownership.

Expected:

```text
403 Forbidden
```

Example:

```text
Instructor A
     ↓
Requests Track owned by Instructor B
     ↓
403 Forbidden
```

Take a screenshot.

---

## Evidence 4 — Global Exception Middleware

Take a screenshot of:

```text
Middlewares
└── GlobalExceptionMiddleware.cs
```

The screenshot should clearly show:

```csharp
try
{
    await next(context);
}
catch (...)
{
    ...
}
```

and:

```csharp
logger.LogError(...)
```

---

## Evidence 5 — Logging

Run the application and trigger:

* Failed login
* Successful login
* Missing resource
* Payment status update
* Enrollment change
* Unexpected exception

Then show the logs in the Visual Studio Output window or terminal.

Example:

```text
warn: GlobalExceptionMiddleware
      Resource not found: Student not found

info: AuthService
      Login successful for user 5 with role Student

warn: AuthService
      Login failed for user 5: invalid password

info: PaymentService
      Payment 4 status changed from Pending to Paid
```

---

# 29. How to View Logs

When running the application through Visual Studio:

```text
Visual Studio
    ↓
Run API
    ↓
Swagger
    ↓
Send Request
    ↓
View → Output
    ↓
Show output from Debug
```

Alternatively:

```bash
dotnet run
```

and observe the terminal output.

---

# 30. Production Error Strategy

The API follows an important production rule:

### Internal

Detailed exception:

```text
System.NullReferenceException
at TrainingCenter.Api.Services...
```

is logged internally.

### Client

Only a safe response is returned:

```json
{
  "success": false,
  "message": "An unexpected error occurred.",
  "data": null,
  "errors": [
    "An internal server error occurred. Please try again later."
  ]
}
```

This prevents exposing:

* Stack traces
* File paths
* Database details
* Internal class names
* Implementation details

to API consumers.

---

# 31. Improvements Made in Task 05

Before Task 05, errors could be handled directly inside individual controllers or could expose internal exception information.

After Task 05:

```text
Before

Controller
 ├── try
 ├── catch
 ├── response
 └── logging

Controller
 ├── try
 ├── catch
 ├── response
 └── logging
```

becomes:

```text
After

Controller
      │
      ▼
    Service
      │
      ▼
   Exception
      │
      ▼
Global Exception Middleware
      │
      ├── Logging
      │
      └── Standard API Response
```

This provides a cleaner and more maintainable architecture.


## Evidence

* [ ] Validation error screenshot
* [ ] Unauthorized `401` screenshot
* [ ] Forbidden `403` screenshot
* [ ] Global exception middleware screenshot
* [ ] Logging/output screenshot
* [ ] README completed
