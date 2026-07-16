# Task 06 - API Standards & Refactor Pack

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Phase:** Phase 02 - ASP.NET Core Web API Basics
* **Task:** Task 06 - API Standards & Refactor Pack

---

# Project Overview

This task focuses on improving an intentionally poorly designed ASP.NET Core Web API.

The original API worked functionally but violated many software engineering and REST API best practices. The project was refactored into a clean, maintainable architecture using controllers, services, DTOs, proper validation, and correct HTTP status codes.

The goal was not to change the functionality, but to improve the code quality, readability, maintainability, and scalability.

---

# Original Project Structure

```text
OriginalBadCode/
└── ProductsController.cs
```

Everything existed inside one controller:

* Product model
* Validation
* Business logic
* Data storage
* API endpoints

---

# Refactored Project Structure

```text
RefactoredApi/

Controllers/
    ProductsController.cs

Models/
    Product.cs

DTOs/
    CreateProductRequest.cs
    ProductResponse.cs

Services/
    IServices/
        IProductService.cs
    ProductService.cs

Program.cs
README.md
```

---

# Original Problems

The original API contained several design issues.

## 1. Controller contained business logic

The controller was responsible for:

* Creating products
* Validating input
* Searching products
* Managing storage

Controllers should only receive HTTP requests and return HTTP responses.

---

## 2. No Service Layer

There was no separation between the API layer and business logic.

All application logic existed inside the controller, making the code difficult to maintain and test.

---

## 3. No DTOs

The POST endpoint accepted multiple primitive parameters:

```csharp
Add(string name, decimal price, int stock)
```

Instead of using a request model.

This makes APIs harder to document and extend.

---

## 4. Public Fields Instead of Properties

The Product model used public fields:

```csharp
public int Id;
public string Name;
```

Modern C# uses properties because they support:

* encapsulation
* validation
* serialization
* Entity Framework compatibility

---

## 5. Incorrect HTTP Status Codes

Validation failures returned:

* 200 OK

Examples:

```text
bad name
bad price
```

These should return:

* 400 Bad Request

---

## 6. Poor Route Names

Original routes:

```text
GET /api/products/all
GET /api/products/get?id=1
```

These are not RESTful.

---

## 7. No Error Response Shape

Errors were returned as plain strings.

Example:

```text
not found
```

Instead of JSON.

---

## 8. No Resource Lookup Validation

The API did not properly return 404 when a product did not exist.

---

# Improvements Made

The API was refactored using modern ASP.NET Core practices.

---

## Product Model

Created a dedicated Product model using properties.

```csharp
public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
```

Benefits:

* Cleaner code
* Better serialization
* Compatible with EF Core

---

## Request DTO

Created

```text
CreateProductRequest
```

This DTO represents the request body received by the POST endpoint.

Benefits:

* Cleaner API
* Easier validation
* Better Swagger documentation

---

## Response DTO

Created

```text
ProductResponse
```

Only the required information is returned to clients.

Benefits:

* Better API security
* Separation between model and response
* Easier future changes

---

## Service Layer

Created

```text
IProductService
ProductService
```

Responsibilities moved into the service:

* Validation
* Product creation
* Searching
* Storage

The controller now only calls service methods.

---

## Dependency Injection

The ProductService is registered in Program.cs.

```csharp
builder.Services.AddSingleton<IProductService, ProductService>();
```

Benefits:

* Loose coupling
* Easier testing
* Better architecture

---

## RESTful Routes

Old

```text
GET /api/products/all
```

New

```text
GET /api/products
```

---

Old

```text
GET /api/products/get?id=1
```

New

```text
GET /api/products/{id}
```

These routes follow REST conventions.

---

## Proper Status Codes

The API now returns the correct HTTP status codes.

| Status Code     | Purpose            |
| --------------- | ------------------ |
| 200 OK          | Successful request |
| 201 Created     | Product created    |
| 400 Bad Request | Invalid request    |
| 404 Not Found   | Product not found  |

---

## Validation

Validation now checks:

* Name is required.
* Price must be greater than zero.
* Stock cannot be negative.

Invalid requests return:

```json
{
    "error": "Price must be greater than zero."
}
```

instead of

```text
bad price
```

---

## JSON Responses

The API now always returns structured JSON responses.

Example:

```json
{
    "productId": 1,
    "name": "Laptop",
    "price": 25000,
    "stock": 15
}
```

---

# Before vs After

| Before                        | After                |
| ----------------------------- | -------------------- |
| Public fields                 | Properties           |
| Controller handled everything | Service Layer        |
| Primitive POST parameters     | Request DTO          |
| No Response DTO               | Response DTO         |
| 200 OK for validation errors  | 400 Bad Request      |
| Returned text errors          | JSON error responses |
| Poor route names              | RESTful routes       |
| No Dependency Injection       | Dependency Injection |
| Hard to maintain              | Layered architecture |
| Difficult to extend           | Easy to extend       |

---

# API Endpoints

## Create Product

**POST**

```text
/api/products
```

Creates a new product.

Response

* 201 Created
* 400 Bad Request

---

## Get All Products

**GET**

```text
/api/products
```

Returns all products.

---

## Get Product By ID

**GET**

```text
/api/products/{id}
```

Returns:

* 200 OK
* 404 Not Found

---

# Screenshots

The repository contains:

* Original API behavior
* Refactored API behavior
* Swagger screenshots
* Successful requests
* Validation errors
* Not Found responses

---

# What I Learned

This task showed the importance of writing clean and maintainable APIs instead of simply making code work. I learned how to separate responsibilities by moving business logic from controllers into services, making controllers smaller and easier to understand. Using DTOs improved validation and prevented exposing internal models directly to clients. I also learned the importance of returning proper HTTP status codes such as 400 Bad Request and 404 Not Found instead of always returning 200 OK. Finally, I gained a better understanding of RESTful routing, dependency injection, and how to structure an ASP.NET Core project so it is ready for future enhancements such as Entity Framework Core and database integration.
