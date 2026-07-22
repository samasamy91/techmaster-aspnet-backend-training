# techmaster-aspnet-backend-training

# TechMaster ASP.NET Backend Career Training

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Language & Framework:** C# / ASP.NET Core (.NET 8)

---

# Repository Overview

This repository contains my solutions for the **TechMaster ASP.NET Backend Career Training**.

The training is organized into phases that gradually build backend development skills, starting from C# programming fundamentals and progressing to professional RESTful Web API development.

---

# Repository Structure

```text
TechMaster.AspNetBackend.Training/
│
├── phase-01-backend-foundations/
│   ├── task-00-setup/
│   ├── task-01-csharp-drills/
│   ├── task-02-object-oriented-programming/
│   ├── task-03-collections-linq/
│   └── ...
│
├── phase-02-web-api-basics/
│   ├── task-00-api-workspace-setup/
│   ├── task-01-rest-routing-drills/
│   ├── task-02-student-management-api/
│   ├── task-03-products-categories-api/
│   ├── task-04-book-store-api/
│   ├── task-05-swagger-postman-evidence/
│   ├── task-06-api-standards-refactor-pack/
│   └── task-07-interview-answers/
│
└── README.md
```

---

# Phase 01 – Backend Foundations

## Overview

Phase 01 focused on building strong C# programming fundamentals before starting ASP.NET Core development.

---

## Topics Covered

* C# syntax
* Variables and data types
* Conditional statements
* Loops
* Methods
* Arrays
* Collections
* Object-Oriented Programming
* Classes and Objects
* Encapsulation
* Inheritance
* Polymorphism
* Abstraction
* Exception Handling
* LINQ
* File Organization
* Git & GitHub Workflow

---

## Skills Learned

* Writing clean C# code
* Breaking problems into methods
* Creating reusable classes
* Working with collections
* Using LINQ
* Applying OOP principles
* Managing projects with Git

---

# Phase 02 – ASP.NET Core Web API Basics

## Overview

Phase 02 focused on designing and implementing professional RESTful APIs using ASP.NET Core.

The phase introduced API architecture, DTOs, dependency injection, service layers, Swagger documentation, and Postman testing.

---

# Completed Tasks

## Task 00 – API Workspace Setup

* Created ASP.NET Core Web API project
* Configured Controllers
* Configured Swagger
* Enabled HTTPS Redirection
* Verified application startup
* Added project documentation

---

## Task 01 – REST & Routing Drill Pack

Implemented 15 API drills covering:

* Health Check
* Route Parameters
* Query Parameters
* Calculator API
* Temperature Converter
* Grade Calculator
* Create Note
* Get Notes
* Get Note by Id
* Update Note
* Delete Note
* Search
* Pagination
* Request Headers
* Standard Error Responses

Concepts practiced:

* Controllers
* Routing
* HTTP Methods
* Request Bodies
* Status Codes
* Validation
* JSON Responses

---

## Task 02 – Student Management API

Built a complete in-memory CRUD API.

### Features

* Create Student
* Update Student
* Delete Student
* Get All Students
* Get Student by Id
* Search
* Filter
* Pagination
* Update Status
* Student Statistics

### Technologies

* DTOs
* Service Layer
* Dependency Injection
* LINQ
* Validation
* RESTful APIs

---

## Task 03 – Products & Categories API

Built a complete Products & Categories management system.

### Features

Categories

* Create Category
* Get Categories

Products

* Create Product
* Update Product
* Delete Product
* Get Product
* Search
* Filters
* Low Stock
* Stock Reports
* Update Stock

Business Rules

* Category Validation
* Positive Price
* Stock Validation
* Availability Handling

---

## Task 04 – Book Store API

Built the largest project in Phase 02.

### Resources

* Authors
* Categories
* Books

### Features

Authors

* CRUD Operations

Categories

* CRUD Operations

Books

* CRUD Operations
* Search
* Pagination
* Availability Filter
* Category Filter
* Author Filter

Reports

* Books per Category
* Books per Author
* Inventory Value
* Available Books
* Out of Stock Books

---

## Task 05 – Swagger & Postman Evidence

Prepared project documentation and testing evidence.

Included:

* Swagger screenshots
* Postman Collection
* Success Requests
* Error Requests
* README Documentation

---

## Task 06 – API Standards & Refactor Pack

Refactored a poorly designed API into a professional architecture.

Improvements:

* DTOs
* Service Layer
* Dependency Injection
* RESTful Routes
* Proper Status Codes
* Validation
* Response Models

---

## Task 07 – Interview Answers Pack

Prepared answers for common backend interview questions covering:

* REST
* HTTP Methods
* Status Codes
* DTOs
* Dependency Injection
* Controllers
* Services
* Validation
* Swagger
* Postman
* Pagination
* Search
* Architecture
* Debugging
* Security
* GitHub Workflow

---

# Technologies Used

## Programming Languages

* C#
* SQL

## Frameworks

* ASP.NET Core Web API (.NET 8)

## Concepts

* REST API
* MVC Pattern
* Dependency Injection
* DTO Pattern
* Service Layer
* SOLID Principles
* LINQ
* CRUD Operations
* Validation
* Pagination
* Search & Filtering

## Tools

* Visual Studio 2022
* Swagger (OpenAPI)
* Postman
* Git
* GitHub

---

# Project Architecture

```text
Controllers
        │
        ▼
Services
        │
        ▼
Models
        ▲
        │
DTOs
```

Controllers handle HTTP requests.

Services contain business logic.

DTOs define request and response models.

Models represent application entities.

---

# How to Run

1. Clone the repository.

```bash
git clone https://github.com/samasamy91/techmaster-aspnet-backend-training.git
```

2. Open the solution in Visual Studio 2022.

3. Set the desired API project as the startup project.

4. Run the application.

5. Open Swagger.

Example:

```text
https://localhost:xxxx/swagger
```

---

# Evidence

The repository includes:

* Swagger screenshots
* Postman collection
* Request/Response screenshots
* API documentation
* Demo videos (where required)

---

# Learning Outcomes

By completing the first two phases of the TechMaster ASP.NET Backend Career Training, I gained practical experience in:

* Writing clean C# applications
* Applying Object-Oriented Programming principles
* Using LINQ for querying collections
* Building RESTful APIs with ASP.NET Core
* Designing DTOs for requests and responses
* Implementing Dependency Injection
* Separating business logic into service layers
* Returning proper HTTP status codes
* Implementing validation and error handling
* Building CRUD operations
* Creating search, filtering, and pagination features
* Documenting APIs with Swagger
* Testing APIs using Postman
* Refactoring APIs following professional standards
* Explaining backend concepts in preparation for technical interviews

---

# Git Commit Style

Examples of commit messages used throughout the repository:

* `Initial training repository setup`
* `Complete Phase 01 C# drills`
* `Add REST routing drills`
* `Implement Student Management API`
* `Implement Products & Categories API`
* `Build Book Store API`
* `Add Swagger and Postman evidence`
* `Refactor API using DTOs and services`
* `Complete interview preparation pack`


