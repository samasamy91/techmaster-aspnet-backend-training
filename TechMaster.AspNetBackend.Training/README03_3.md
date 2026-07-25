# Task 03 - Training Center Database API

## Overview

The **Training Center Database API** is an ASP.NET Core 8 Web API developed for **TechMaster Academy** as part of **Phase 03 - Real Backend Data Systems**.

The system manages the complete training center workflow, including students, instructors, training tracks, enrollments, payments, and reporting. It uses **Entity Framework Core** with **SQL Server**, follows a layered architecture using **DTOs** and **Services**, and exposes RESTful endpoints documented with **Swagger**.

---

# Objectives

- Build a real database-driven Web API.
- Design and implement relational database models using EF Core.
- Apply DTOs instead of exposing entities.
- Implement business rules in the Service layer.
- Provide RESTful endpoints with proper HTTP status codes.
- Document the API using Swagger.
- Test endpoints using Postman.
- Generate reports using LINQ and EF Core.

---

# Technologies Used

- ASP.NET Core 8 Web API
- C#
- Entity Framework Core
- SQL Server
- LINQ
- Dependency Injection
- Swagger / OpenAPI
- Postman
- Visual Studio 2022

---

# Project Structure

```
task-03-training-center-database-api
│
├── TrainingCenter.Api
│   ├── Common
│   ├── Controllers
│   ├── Data
│   ├── DTOs
│   ├── Entities
│   ├── Migrations
│   ├── Services
│   │   ├── Interfaces
│   │   └── Implementations
│   ├── Program.cs
│   └── appsettings.json
│
├── postman
│
├── evidence
│
└── README.md
```

---

# Database Design

The API uses SQL Server with Entity Framework Core.

## Tables

- Students
- Instructors
- TrainingTracks
- Enrollments
- Payments

---

# Entity Relationships

- One Instructor → Many Training Tracks
- One Student → Many Enrollments
- One Training Track → Many Enrollments
- One Enrollment → Many Payments

---

# Features

## Students

- Create student
- Update student
- Soft delete student
- Search students
- Pagination
- Filter by active status
- Get student details

---

## Instructors

- Create instructor
- Update instructor
- View instructors
- View instructor details
- View instructor tracks

---

## Training Tracks

- Create track
- Update track
- Soft delete track
- Search tracks
- Filter by

  - Level
  - Status
  - Instructor
  - Keyword

- Track capacity validation

---

## Enrollments

- Enroll student
- Prevent duplicate enrollments
- Validate track capacity
- Update enrollment status
- Student enrollment history
- View students in a track

---

## Payments

- Create payment
- Update payment status
- View payment history
- Generate unique payment reference number

---

## Reports

- Dashboard Summary
- Revenue Summary
- Revenue By Track
- Track Capacity
- Unpaid Enrollments

---

# Business Rules

- Student email must be unique.
- Instructor email must be unique.
- Track code must be unique.
- Track capacity must be greater than zero.
- Students cannot enroll twice in the same track.
- Students cannot enroll when track capacity is full.
- Students are soft deleted.
- Training tracks are soft deleted.
- Payment reference numbers are generated automatically.
- Business logic is implemented inside Services.
- Controllers return DTOs only.

---

# API Endpoints

## Students

| Method | Endpoint |
|---------|----------|
| GET | /api/students |
| GET | /api/students/{id} |
| POST | /api/students |
| PUT | /api/students/{id} |
| DELETE | /api/students/{id} |

---

## Instructors

| Method | Endpoint |
|---------|----------|
| GET | /api/instructors |
| GET | /api/instructors/{id} |
| POST | /api/instructors |
| PUT | /api/instructors/{id} |
| GET | /api/instructors/{id}/tracks |

---

## Training Tracks

| Method | Endpoint |
|---------|----------|
| GET | /api/tracks |
| GET | /api/tracks/{id} |
| POST | /api/tracks |
| PUT | /api/tracks/{id} |
| DELETE | /api/tracks/{id} |

---

## Enrollments

| Method | Endpoint |
|---------|----------|
| GET | /api/enrollments |
| GET | /api/enrollments/{id} |
| POST | /api/enrollments |
| PUT | /api/enrollments/{id}/status |
| GET | /api/students/{id}/enrollments |
| GET | /api/tracks/{id}/students |

---

## Payments

| Method | Endpoint |
|---------|----------|
| GET | /api/payments |
| POST | /api/payments |
| GET | /api/enrollments/{id}/payments |
| PUT | /api/payments/{id}/status |

---

## Reports

| Method | Endpoint |
|---------|----------|
| GET | /api/reports/dashboard-summary |
| GET | /api/reports/unpaid-enrollments |
| GET | /api/reports/track-capacity |
| GET | /api/reports/revenue-summary |
| GET | /api/reports/revenue-by-track |

---

# Response Format

All endpoints return a consistent API response structure.

```json
{
  "success": true,
  "message": "Student created successfully.",
  "data": {
    "studentId": 1,
    "fullName": "Mohamed Ayman",
    "email": "mohamed@example.com"
  }
}
```

---

# Entity Framework Core

Implemented using:

- DbContext
- Data Annotations
- Fluent API Relationships
- EF Core Migrations
- SQL Server

Migration Commands

```powershell
Add-Migration InitialCreate

Update-Database
```

---

# Swagger

Swagger is enabled for API documentation.

Available after running the project:

```
https://localhost:{port}/swagger
```

---

# Postman

The project includes a Postman collection containing:

- Successful requests
- Validation failures
- CRUD operations
- Report endpoints

Location:

```
postman/
```

---

# Evidence

The evidence folder contains screenshots of:

- Swagger UI
- SQL Server database
- Tables
- Successful API requests
- Validation errors
- Reports
- EF Core migration

Location:

```
evidence/
```

---

# How to Run

## Clone Repository

```bash
git clone <repository-url>
```

## Navigate

```bash
cd TrainingCenter.Api
```

## Restore Packages

```bash
dotnet restore
```

## Update Database

```bash
dotnet ef database update
```

## Run

```bash
dotnet run
```

---

# Learning Outcomes

This project demonstrates:

- RESTful API development
- Entity Framework Core
- SQL Server integration
- Layered architecture
- Dependency Injection
- DTO mapping
- Business rule implementation
- LINQ queries
- Reporting
- Swagger documentation
- Postman API testing

---

# Deliverables

- ✅ Entity Framework Core Models
- ✅ SQL Server Database
- ✅ EF Core Migrations
- ✅ DTO Layer
- ✅ Service Layer
- ✅ Controllers
- ✅ Dependency Injection
- ✅ Swagger Documentation
- ✅ Postman Collection
- ✅ Evidence Screenshots
- ✅ README Documentation

---
