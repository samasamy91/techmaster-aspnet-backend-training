# Phase 03 - Database & Entity Framework Core

## TechMaster ASP.NET Backend Career Training

Phase 03 focuses on designing and implementing a production-style database-driven ASP.NET Core Web API using Entity Framework Core and SQL Server. The phase progresses from database modeling to building a complete RESTful API, implementing business rules, advanced querying, and deploying the application to a live hosting environment.


# Learning Objectives

By completing this phase, I learned how to:

- Design relational databases from business requirements.
- Create Entity Framework Core entities and relationships.
- Build a layered ASP.NET Core Web API.
- Apply DTOs and Service Layer architecture.
- Implement filtering, searching, sorting, and pagination.
- Write business rules inside services.
- Protect data integrity.
- Generate EF Core migrations.
- Connect to SQL Server databases.
- Deploy an ASP.NET Core API to production.
- Connect a live API to a remote SQL Server database.

---

# Technology Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- LINQ
- Swagger / OpenAPI
- Postman
- Visual Studio 2022
- RunASP.NET Hosting
- Git & GitHub

---

# Phase Structure

```
phase-03-database-and-ef-core/

│
├── task-00-workspace-setup/
├── task-01-ef-core-modeling-drills/
├── task-02-requirements-to-erd/
├── task-03-training-center-database-api/
├── task-04-querying-filtering-pagination/
├── task-05-business-rules-data-integrity/
├── task-06-production-hosting/
│
└── README.md
```

---

# Tasks Overview

## Task 00 - Workspace Setup

### Objective

Prepare the development environment for Entity Framework Core.

### Completed

- Created ASP.NET Core Web API project.
- Installed EF Core packages.
- Configured SQL Server connection.
- Configured DbContext.
- Created project structure.
- Tested database connectivity.

---

## Task 01 - EF Core Modeling Drill Pack

### Objective

Practice Entity Framework Core relationships and modeling.

### Completed Drills

- DbContext & First Migration
- One-to-One Relationship
- One-to-Many Relationship
- Many-to-Many Relationship
- Payment Summary
- Seed Data
- Soft Delete
- Audit Fields
- Projection DTOs
- Pagination

### Concepts

- Fluent API
- Data Annotations
- Relationships
- Migrations
- Navigation Properties
- Global Query Filters

---

## Task 02 - Requirements to ERD

### Objective

Convert business requirements into a relational database design.

### Business Domain

Training Center Management System

### Designed Entities

- Student
- Instructor
- TrainingTrack
- Enrollment
- Payment

### Deliverables

- ERD Diagram
- Entity Specifications
- PK/FK Design
- Relationships
- Business Rules
- Database Reports
- Design Documentation

---

## Task 03 - Training Center Database API

### Objective

Build a production-style REST API using Entity Framework Core.

### Modules

### Students

- CRUD Operations
- Soft Delete
- Search
- Pagination

### Instructors

- CRUD Operations
- Track Assignment

### Training Tracks

- CRUD Operations
- Capacity Management
- Instructor Assignment

### Enrollments

- Student Registration
- Status Management
- Enrollment History

### Payments

- Payment Creation
- Payment Status
- Payment History

### Reports

- Dashboard Summary
- Revenue Summary
- Revenue by Track
- Track Capacity
- Unpaid Enrollments

### Architecture

- Controllers
- Services
- DTOs
- EF Core
- SQL Server

---

## Task 04 - Querying, Filtering, Pagination & Reports

### Objective

Implement production-style query endpoints.

### Implemented Queries

- Search Students
- Filter Students
- Student Pagination
- Search Tracks
- Filter Tracks by Level
- Filter Tracks by Instructor
- Tracks with Available Seats
- Enrollment List
- Enrollment Status Filter
- Student Enrollment History
- Track Students
- Unpaid Enrollments
- Payments by Date Range
- Revenue Summary
- Revenue by Track
- Top Tracks
- Instructor Workload
- Students Without Payments
- Advanced Enrollment Filter
- Dashboard Summary

### EF Core Features

- Where
- Select
- Include
- GroupBy
- Sum
- Count
- Any
- Skip
- Take
- OrderBy
- Projection DTOs

---

## Task 05 - Business Rules & Data Integrity

### Objective

Protect the system from invalid operations.

### Student Rules

- Unique Email
- Required Name
- Soft Delete
- Prevent Enrollment for Deleted Students
- Hide Deleted Records

### Track Rules

- Required Title
- Unique Code
- Capacity Validation
- Date Validation
- Instructor Required
- Capacity Protection
- Closed Track Validation

### Enrollment Rules

- Prevent Duplicate Active Enrollment
- Pending Status by Default
- Status Transition Rules
- Completed Cannot Be Cancelled
- Cancelled Enrollments Ignored in Capacity

### Payment Rules

- Positive Amount
- Prevent Overpayment
- Payment Status Validation
- Revenue Uses Paid Payments Only
- Failed Payments Do Not Activate Enrollment

---

## Task 06 - Production Hosting & Remote Database

### Objective

Deploy the Training Center API to a live production environment.

### Completed

- Created RunASP.NET website.
- Created remote SQL Server database.
- Applied EF Core migrations.
- Published API using Web Deploy.
- Connected API to remote SQL Server.
- Verified GET endpoints.
- Verified POST endpoints.
- Published live Swagger.

### Deployment Features

- Live API
- Remote SQL Server
- HTTPS
- Swagger
- Production Configuration
- Secure Connection Strings

---

# Project Architecture

```
TrainingCenter.Api/

│
├── Controllers
├── Data
├── DTOs
├── Entities
├── Services
├── Common
├── Migrations
├── Program.cs
└── appsettings.json
```

---

# Main Features

- Entity Framework Core
- SQL Server Integration
- Repository-Free Service Layer
- DTO Mapping
- Soft Delete
- Audit Fields
- Filtering
- Searching
- Sorting
- Pagination
- Reporting
- Business Rules
- Data Validation
- Swagger Documentation
- Postman Testing
- Production Deployment

---

# Database Entities

- Student
- StudentProfile
- Instructor
- TrainingTrack
- Enrollment
- Payment
- PaymentSummary

---

# Entity Relationships

- Student → StudentProfile (One-to-One)
- Student → Enrollment (One-to-Many)
- Instructor → TrainingTrack (One-to-Many)
- TrainingTrack → Enrollment (One-to-Many)
- Enrollment → Payment (One-to-Many)
- Student ↔ TrainingTrack (Many-to-Many through Enrollment)

---

# API Features

- CRUD Operations
- Validation
- Soft Delete
- Search
- Filtering
- Sorting
- Pagination
- Reports
- Dashboard
- DTO Responses
- Service Layer
- Swagger Documentation

---

# Testing

The project was tested using:

- Swagger UI
- Postman
- SQL Server
- Remote Hosting

Testing includes:

- Success scenarios
- Failure scenarios
- Business rule validation
- Query validation
- Production deployment validation

---

# Deployment

The API has been deployed successfully.

Deployment includes:

- Live ASP.NET Core Web API
- Remote SQL Server Database
- HTTPS
- Swagger Documentation
- Production Testing

---

# Skills Demonstrated

- Entity Framework Core
- LINQ
- SQL Server
- REST API Design
- DTO Pattern
- Service Layer
- Dependency Injection
- Fluent API
- EF Core Migrations
- Query Optimization
- Pagination
- Reporting
- Business Rule Implementation
- Data Integrity
- Production Deployment
- Remote Database Configuration
- Swagger Documentation
- Postman Testing



---

# Final Outcome

Phase 03 delivers a complete, production-ready **Training Center Registration API** built with ASP.NET Core Web API and Entity Framework Core. The project includes a well-designed SQL Server database, clean layered architecture, comprehensive business rules, advanced querying capabilities, automated documentation through Swagger, thorough Postman testing, and successful deployment to a live hosting environment connected to a remote SQL Server database.