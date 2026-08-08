# Task 03 - Secure Platform Upgrade

## TechMaster Academy - ASP.NET Backend Career Training

Phase 04 - Secure Platform Upgrade

---

## Overview

Task 03 upgrades the Phase 03 Training Center API into a secure, role-based platform.

The API now protects sensitive operations using:

- JWT Authentication
- Role-Based Authorization
- Ownership Validation
- Current-user claims
- Secure DTO responses
- Admin-only operations
- Instructor-scoped operations
- Student-scoped operations
- Protected payment and report endpoints
- Business-rule validation

The main goal is to ensure that authentication alone is not enough to access data.

A user must also have the correct role and, where required, ownership of the requested resource.


Project Structure

TrainingCenter.Api
│
├── Controllers
│   ├── AuthController.cs
│   ├── StudentController.cs
│   ├── InstructorController.cs
│   ├── TrackController.cs
│   ├── EnrollmentController.cs
│   ├── PaymentController.cs
│   └── ReportController.cs
│
├── Data
│   ├── AppDbContext.cs
│   └── DatabaseSeeder.cs
│
├── Entities
│   ├── User.cs
│   ├── Student.cs
│   ├── Instructor.cs
│   ├── TrainingTrack.cs
│   ├── Enrollment.cs
│   ├── Payment.cs
│   └── TrackSession.cs
│
├── DTOs
│
├── Services
│   ├── AuthService.cs
│   ├── StudentService.cs
│   ├── InstructorService.cs
│   ├── TrackService.cs
│   ├── EnrollmentService.cs
│   ├── PaymentService.cs
│   └── ReportService.cs
│
├── Security
│   ├── JwtService.cs
│   └── PasswordHasher.cs
│
└── Common


---

# Business Scenario

TechMaster Academy now has three main user roles:

- **Admin**
- **Instructor**
- **Student**

Each role has different responsibilities and access boundaries.

The API must prevent users from accessing information that does not belong to them.

For example:

- A Student cannot view another student's profile.
- A Student cannot access revenue reports.
- An Instructor cannot access another instructor's tracks.
- An Instructor cannot update another instructor's sessions.
- An Instructor cannot access payment revenue reports.
- Only Admin can manage tracks.
- Only Admin can update payment status.
- Anonymous users cannot access protected endpoints.

---

# Technologies

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-Based Authorization
- LINQ
- Swagger
- Postman
- Git / GitHub

---

# Authentication

Authentication was implemented without ASP.NET Core Identity.

The project uses a custom `User` entity and custom password hashing.

## User

The User entity contains:

```text
Id
FullName
Email
HashPassword
Role
IsActive
CreatedAt
UpdatedAt
LastLoginAt
StudentId
InstructorId

Admin Responsibilities

Admin has full access to administrative operations.

Admin can:

Manage students
Manage instructors
Manage training tracks
View all enrollments
Update enrollment status
View payments
Update payment status
View reports
View revenue
Perform administrative operations

Instructor Responsibilities

Instructors can access only their assigned tracks and related data.

Instructor operations include:

View assigned tracks
View students in assigned tracks
View track enrollments
Create sessions
Update sessions
View track progress

An Instructor cannot access another Instructor's track.

Student Responsibilities

Students can access only their own information.

Students can:

View their own profile
View their own enrollments
View their own payments
Browse available tracks
Request enrollment

Students cannot access:

Other students' profiles
Other students' enrollments
Other students' payments
Admin reports
Revenue reports
Administrative operations

| Endpoint Group         | Admin       | Instructor                 | Student             |
| ---------------------- | ----------- | -------------------------- | ------------------- |
| Students CRUD          | Full access | Own/assigned students only | Own profile only    |
| Instructors CRUD       | Full access | Own profile/operations     | No access           |
| Tracks                 | Full access | Assigned tracks            | Available tracks    |
| Enrollments            | Full access | Own track enrollments      | Own enrollments     |
| Payments               | Full access | No payment administration  | Own payment history |
| Payment Status         | Full access | No access                  | No access           |
| Reports                | Full access | Own track reports          | No access           |
| Revenue Reports        | Full access | No access                  | No access           |
| Track Sessions         | Full access | Own tracks                 | No access           |
| Audit/Admin Operations | Full access | No access                  | No access           |
