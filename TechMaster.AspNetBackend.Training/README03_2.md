# Task 03 - Products & Categories API

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Phase:** Phase 02 - Web API Basics
* **Task:** Task 03 - Products & Categories API

---

# Project Overview

The Products & Categories API is an ASP.NET Core Web API that simulates a simple store management system using in-memory data storage. The API allows users to manage product categories, perform CRUD operations on products, search and filter products, update stock, and generate stock reports.

The project demonstrates RESTful API design, service-layer architecture, dependency injection, DTO usage, LINQ queries, validation, and proper HTTP status codes.

---

# Technologies Used

* ASP.NET Core Web API (.NET 8)
* C#
* Swagger (OpenAPI)
* Dependency Injection
* LINQ
* In-Memory Storage (`List<T>`)

---

# Project Structure

```text
ProductsCategoriesApi/
│
├── Controllers/
│   ├── CategoriesController.cs
│   └── ProductsController.cs
│
├── Models/
│   ├── Category.cs
│   └── Product.cs
│
├── DTOs/
│   ├── CreateCategoryRequest.cs
│   ├── CreateProductRequest.cs
│   ├── UpdateProductRequest.cs
│   ├── UpdateStockRequest.cs
│   └── StockReportResponse.cs
│
├── Services/
│   ├── ICategoryService.cs
│   ├── CategoryService.cs
│   ├── IProductService.cs
│   └── ProductService.cs
│
├── Program.cs
└── README.md
```

---

# Business Rules

### Categories

* Category name is required.
* Category name must be unique.
* Only active categories are returned.
* A product can only belong to an existing category.

### Products

* Product name is required.
* Price must be greater than zero.
* Stock quantity cannot be negative.
* Category must exist before creating or updating a product.
* Deleting a product marks it as unavailable instead of removing it permanently.

---

# Seed Data

The project includes the following sample data:

| Category    | Products |
| ----------- | -------- |
| Electronics | 5        |
| Furniture   | 3        |
| Stationery  | 4        |
| Accessories | 3        |

**Total Categories:** 4

**Total Products:** 15

---

# API Endpoints

## Categories

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

# Products

### Get All Products

**GET**

```http
/api/products
```

Supports:

* Search
* Category filtering
* Price filtering
* Availability filtering

Example

```http
GET /api/products?search=laptop
```

```http
GET /api/products?categoryId=1
```

```http
GET /api/products?minPrice=500
```

```http
GET /api/products?maxPrice=5000
```

```http
GET /api/products?isAvailable=true
```

```http
GET /api/products?search=mouse&categoryId=4&minPrice=100&maxPrice=500
```

Response

* 200 OK

---

### Get Product By Id

**GET**

```http
/api/products/{id}
```

Response

* 200 OK
* 404 Not Found

---

### Create Product

**POST**

```http
/api/products
```

Creates a new product.

Response

* 201 Created
* 400 Bad Request

---

### Update Product

**PUT**

```http
/api/products/{id}
```

Updates an existing product.

Response

* 200 OK
* 400 Bad Request
* 404 Not Found

---

### Update Stock

**PATCH**

```http
/api/products/{id}/stock
```

Updates only the stock quantity.

Request

```json
{
  "stockQuantity": 25
}
```

Response

* 200 OK
* 404 Not Found

---

### Delete Product

**DELETE**

```http
/api/products/{id}
```

Marks the product as unavailable.

Response

* 204 No Content
* 404 Not Found

---

### Low Stock Products

**GET**

```http
/api/products/low-stock
```

Returns products with stock below the configured threshold.

Response

* 200 OK

---

### Stock Reports

**GET**

```http
/api/products/reports/stock-value
```

Returns:

* Total stock value
* Stock value per category
* Product count per category
* Low stock products
* Out-of-stock products

Response

* 200 OK

---

# Search & Filter Examples

Search by name

```http
GET /api/products?search=laptop
```

Filter by category

```http
GET /api/products?categoryId=1
```

Filter by availability

```http
GET /api/products?isAvailable=true
```

Filter by minimum price

```http
GET /api/products?minPrice=1000
```

Filter by maximum price

```http
GET /api/products?maxPrice=5000
```

Filter by price range

```http
GET /api/products?minPrice=100&maxPrice=1000
```

Combined filters

```http
GET /api/products?search=mouse&categoryId=4&minPrice=100&maxPrice=500&isAvailable=true
```

---

# Stock Report

The stock report includes:

* Total stock value
* Stock value grouped by category
* Number of products in each category
* Low stock products
* Out-of-stock products

---

# Validation

The API validates:

* Required product name
* Required category name
* Positive product price
* Non-negative stock quantity
* Existing category before product creation
* Unique category names

---

# HTTP Status Codes

| Status Code     | Description                         |
| --------------- | ----------------------------------- |
| 200 OK          | Request completed successfully      |
| 201 Created     | Resource created successfully       |
| 204 No Content  | Product deleted/marked unavailable  |
| 400 Bad Request | Invalid request or validation error |
| 404 Not Found   | Resource not found                  |

---

# How to Run

1. Open the solution in Visual Studio.
2. Build the project.
3. Run the API.
4. Open Swagger.

Swagger URL

```text
https://localhost:{port}/swagger
```

---

# Testing

The API was tested using:

* Swagger UI
* Postman

Tested scenarios include:

* Create category
* Create product
* Get all products
* Get product by ID
* Update product
* Update stock
* Delete product
* Search by name
* Filter by category
* Filter by price
* Filter by availability
* Low stock products
* Stock reports

---

# Evidence

* Swagger screenshots
* Postman collection
* Postman request/response screenshots

---

# Learning Outcomes

Through this task, I practiced:

* Building RESTful APIs with ASP.NET Core
* Managing related resources (Products and Categories)
* Applying dependency injection
* Implementing a service layer
* Creating request and response DTOs
* Using LINQ for searching, filtering, grouping, and reporting
* Implementing business validation
* Returning appropriate HTTP status codes
* Designing clean and maintainable API architecture
