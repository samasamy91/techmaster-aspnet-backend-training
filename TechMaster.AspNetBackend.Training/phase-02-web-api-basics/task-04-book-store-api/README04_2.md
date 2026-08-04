# Task 04 - Book Store API Mini Project

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Phase:** Phase 02 - ASP.NET Core Web API Basics
* **Task:** Task 04 - Book Store API Mini Project

---

# Project Overview

The Book Store API is an ASP.NET Core Web API that simulates a bookstore management system using in-memory data storage. The project is designed to mimic a real-world application structure and prepares the codebase for Entity Framework Core integration in Phase 03.

The API manages three related resources:

* Authors
* Categories
* Books

Books are linked to both authors and categories through their respective IDs, demonstrating relationships similar to those used in relational databases.

---

# Project Objectives

This project demonstrates:

* RESTful API design
* Layered architecture
* Dependency Injection
* DTO pattern
* In-memory data storage
* Resource relationships
* Search and filtering
* Pagination
* LINQ reporting
* Proper HTTP status codes
* Validation and business rules

---

# Technologies Used

* ASP.NET Core Web API (.NET 8)
* C#
* Swagger / OpenAPI
* Dependency Injection
* LINQ
* In-Memory Collections (`List<T>`)

---

# Project Structure

```text
BookStoreApi/
│
├── Controllers/
│   ├── AuthorsController.cs
│   ├── CategoriesController.cs
│   └── BooksController.cs
│
├── Models/
│   ├── Author.cs
│   ├── Category.cs
│   └── Book.cs
│
├── DTOs/
│   ├── CreateAuthorRequest.cs
│   ├── AuthorResponse.cs
│   ├── CreateCategoryRequest.cs
│   ├── CategoryResponse.cs
│   ├── CreateBookRequest.cs
│   ├── UpdateBookRequest.cs
│   ├── BookResponse.cs
│   └── BookSummaryResponse.cs
│
├── Services/
│   ├── IAuthorService.cs
│   ├── AuthorService.cs
│   ├── ICategoryService.cs
│   ├── CategoryService.cs
│   ├── IBookService.cs
│   └── BookService.cs
│
├── Program.cs
└── README.md
```

---

# Models

## Author

* AuthorId
* FullName
* Country
* BirthDate
* CreatedAt

---

## Category

* CategoryId
* Name
* Description
* IsActive

---

## Book

* BookId
* Title
* ISBN
* PublishedYear
* Price
* StockQuantity
* AuthorId
* CategoryId
* IsAvailable
* CreatedAt

---

# Business Rules

## Authors

* Full name is required.
* Author IDs are unique.
* Authors with assigned books cannot be deleted.

---

## Categories

* Category name is required.
* Category name must be unique.
* Only active categories can be assigned to new books.

---

## Books

* Title is required.
* ISBN is required.
* ISBN must be unique.
* Price must be greater than zero.
* Stock quantity cannot be negative.
* Author must exist.
* Category must exist.
* Deleting a book marks it as unavailable instead of removing it permanently.

---

# Seed Data

The project includes sample data for testing.

## Authors

* Robert C. Martin
* Martin Fowler
* Jon Skeet
* Andrew Hunt
* Eric Evans

---

## Categories

* Programming
* Databases
* Software Engineering
* Design Patterns

---

## Books

The project contains **15 seeded books**, including:

* Clean Code
* Clean Architecture
* Refactoring
* Domain-Driven Design
* C# in Depth
* The Pragmatic Programmer
* Code Complete
* Effective Java
* ASP.NET Core in Action
* Design Patterns
* Head First Design Patterns
* SQL Cookbook
* CLR via C#
* Working Effectively with Legacy Code
* Patterns of Enterprise Application Architecture

---

# API Endpoints

## Authors

### Get All Authors

**GET**

```http
/api/authors
```

Returns all authors.

Response

* 200 OK

---

### Create Author

**POST**

```http
/api/authors
```

Creates a new author.

Response

* 201 Created
* 400 Bad Request

---

# Categories

### Get All Categories

**GET**

```http
/api/categories
```

Returns all active categories.

Response

* 200 OK

---

### Create Category

**POST**

```http
/api/categories
```

Creates a new category.

Response

* 201 Created
* 400 Bad Request

---

# Books

### Get All Books

**GET**

```http
/api/books
```

Supports:

* Search
* Category filtering
* Author filtering
* Availability filtering
* Pagination

---

### Get Book By ID

**GET**

```http
/api/books/{id}
```

Response

* 200 OK
* 404 Not Found

---

### Create Book

**POST**

```http
/api/books
```

Creates a new book.

Validation:

* Existing author
* Existing category
* Unique ISBN

Response

* 201 Created
* 400 Bad Request

---

### Update Book

**PUT**

```http
/api/books/{id}
```

Response

* 200 OK
* 400 Bad Request
* 404 Not Found

---

### Delete Book

**DELETE**

```http
/api/books/{id}
```

Marks the book as unavailable.

Response

* 204 No Content
* 404 Not Found

---

# Search & Filtering

The Books endpoint supports multiple filters.

## Search by title

```http
GET /api/books?search=clean
```

---

## Search by ISBN

```http
GET /api/books?search=978
```

---

## Filter by author

```http
GET /api/books?authorId=2
```

---

## Filter by category

```http
GET /api/books?categoryId=3
```

---

## Filter by availability

```http
GET /api/books?isAvailable=true
```

---

## Pagination

```http
GET /api/books?pageNumber=1&pageSize=5
```

---

## Combined Filters

```http
GET /api/books?search=clean&authorId=1&categoryId=2&isAvailable=true&pageNumber=1&pageSize=5
```

---

# Reports

## Summary Report

**GET**

```http
/api/books/reports/summary
```

Returns:

* Total books
* Available books
* Out-of-stock books
* Total inventory value
* Books grouped by author
* Books grouped by category

Response

* 200 OK

---

# Validation

The API validates:

* Required fields
* Unique ISBN
* Unique category names
* Positive prices
* Non-negative stock quantities
* Existing authors
* Existing categories

Invalid requests return **400 Bad Request** with descriptive error messages.

---

# HTTP Status Codes

| Status Code     | Description                       |
| --------------- | --------------------------------- |
| 200 OK          | Successful request                |
| 201 Created     | Resource created successfully     |
| 204 No Content  | Book marked unavailable           |
| 400 Bad Request | Validation or business rule error |
| 404 Not Found   | Resource not found                |

---

# Dependency Injection

Services are registered in `Program.cs` using Dependency Injection.

* IAuthorService
* ICategoryService
* IBookService

Business logic is implemented inside services to keep controllers clean and maintainable.

---

# How to Run

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Build the solution.
4. Run the project.
5. Open Swagger.

Swagger URL

```text
https://localhost:5035/swagger
```

---

# Testing

The API was tested using:

* Swagger UI
* Postman

Test scenarios include:

* Create author
* Create category
* Create book
* Invalid author
* Invalid category
* Duplicate ISBN
* Get all books
* Get book by ID
* Update book
* Delete book
* Search books
* Filter by author
* Filter by category
* Pagination
* Summary report

---

# Evidence

The following evidence is included with the submission:

* Swagger screenshots
* Postman request and response screenshots
* Postman collection
* Demo video (3–6 minutes)

---

# Learning Outcomes

Through this project, I learned to:

* Build a multi-resource RESTful API.
* Design relationships between entities.
* Use DTOs to separate API contracts from models.
* Implement dependency injection and service layers.
* Apply business validation rules.
* Build search, filtering, and pagination with LINQ.
* Generate summary reports using grouping and aggregation.
* Structure an ASP.NET Core project for future Entity Framework Core integration.




# Review Questions

## Feature 01 – Authors API

### Why did you use this route?

I used `/api/authors` because it follows RESTful API conventions where the resource name is plural and represents a collection of authors. Different HTTP methods (`GET` and `POST`) perform different operations on the same resource.

### What DTOs did you create?

* `CreateAuthorRequest`
* `AuthorResponse`

These DTOs separate the API contract from the internal model and ensure only the required data is exposed to clients.

### What validation rules did you apply?

* Author full name is required.
* Country is required.
* Author IDs are generated automatically to ensure uniqueness.

### How will this change when EF Core is added?

The in-memory `List<Author>` will be replaced by a database table. The service will use `DbContext` to retrieve and store authors, and IDs will be generated by the database.

---

## Feature 02 – Categories API

### Why did you use this route?

I used `/api/categories` because it represents the Categories resource and follows REST naming conventions. The route is simple, readable, and consistent with the rest of the API.

### What DTOs did you create?

* `CreateCategoryRequest`
* `CategoryResponse`

These DTOs simplify validation and prevent exposing the entity directly.

### What validation rules did you apply?

* Category name is required.
* Category name must be unique.
* Only active categories can be assigned to new books.

### How will this change when EF Core is added?

Categories will be stored in a database table, uniqueness can be enforced with a database constraint, and validation will check the database instead of an in-memory collection.

---

## Feature 03 – Books API

### Why did you use this route?

I used `/api/books` because books are the primary resource in the application. The route follows REST conventions and supports CRUD operations through HTTP methods.

### What DTOs did you create?

* `CreateBookRequest`
* `UpdateBookRequest`
* `BookResponse`

These DTOs separate request and response data from the domain model.

### What validation rules did you apply?

* Title is required.
* ISBN is required.
* ISBN must be unique.
* Price must be greater than zero.
* Stock quantity cannot be negative.
* Author must exist.
* Category must exist.

### How will this change when EF Core is added?

Validation for author and category existence will query the database using foreign key relationships. ISBN uniqueness can be enforced using a unique index in the database.

---

## Feature 04 – Search and Pagination

### Why did you use this route?

I used query parameters because searching, filtering, and pagination modify the returned collection without changing the resource itself. This follows REST best practices.

Example:

```text
GET /api/books?search=clean&authorId=1&pageNumber=1&pageSize=5
```

### What DTOs did you create?

The endpoint returns `BookResponse` objects after applying filters and pagination.

### What validation rules did you apply?

* `pageNumber` must be greater than zero.
* `pageSize` must be within the allowed range.
* Optional filters are applied only when values are provided.

### How will this change when EF Core is added?

Filtering and pagination will be translated into SQL queries using LINQ to Entities, improving performance by retrieving only the required records from the database.

---

## Feature 05 – Reports Summary

### Why did you use this route?

I used `/api/books/reports/summary` because it clearly indicates that the endpoint returns aggregated reporting data rather than individual book resources.

### What DTOs did you create?

* `BookSummaryResponse`

This DTO organizes all report values into a single response object.

### What validation rules did you apply?

No additional validation is required because the endpoint only reads existing data and calculates statistics.

### How will this change when EF Core is added?

The summary calculations will use LINQ queries executed against the database. Aggregate functions such as `Count`, `Sum`, and `GroupBy` will be translated into SQL for improved efficiency.

---

# Design Decisions

* Used a **Service Layer** to keep business logic out of controllers.
* Used **DTOs** to separate API contracts from domain models.
* Used **Dependency Injection** to make services reusable and easier to test.
* Used **in-memory collections** to simulate a database before introducing Entity Framework Core in the next phase.
* Used **LINQ** for searching, filtering, grouping, pagination, and report generation.
* Followed **RESTful API principles** with meaningful routes and appropriate HTTP status codes.

