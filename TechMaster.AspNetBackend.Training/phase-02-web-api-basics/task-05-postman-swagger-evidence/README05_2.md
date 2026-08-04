# Task 05 - Swagger & Postman Evidence Pack

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Phase:** Phase 02 - ASP.NET Core Web API Basics
* **Task:** Task 05 - Swagger & Postman Evidence Pack

---

# Overview

This task demonstrates that all APIs developed during Phase 02 have been tested and verified using both **Swagger UI** and **Postman**.

The evidence included in this task proves that all endpoints function correctly, return the expected HTTP status codes, and satisfy the required business rules.

---

# APIs Included

The Postman collection contains requests for the following projects:

* REST & Routing Drills
* Student Management API
* Products & Categories API
* Book Store API

---

# Swagger Evidence

Swagger was used to verify:

* All controllers are visible.
* All endpoints are listed correctly.
* Request DTOs are displayed.
* Response schemas are generated.
* Parameters are documented.
* Successful and error responses can be tested interactively.

---

## Swagger Screenshots

The repository contains screenshots for the following endpoints:

1. Swagger Home
2. Student Management API
3. Products API
4. Categories API
5. Books API
6. Authors API
7. Reports Endpoint
8. Search & Pagination Endpoint

Folder:

```text
Evidence/Swagger/
```

---

# Postman Collection

Collection Name

```text
TechMaster ASP.NET Phase 02.postman_collection.json
```

---

## Collection Structure

```text
TechMaster ASP.NET Phase 02

├── REST & Routing Drills
│
├── Student Management API
│   ├── Create Student
│   ├── Get All Students
│   ├── Get Student By Id
│   ├── Update Student
│   ├── Update Student Status
│   ├── Delete Student
│   └── Student Statistics
│
├── Products & Categories API
│   ├── Create Category
│   ├── Get Categories
│   ├── Create Product
│   ├── Get Products
│   ├── Search Products
│   ├── Update Product
│   ├── Update Stock
│   ├── Delete Product
│   ├── Low Stock Products
│   └── Stock Value Report
│
├── Book Store API
│   ├── Create Author
│   ├── Get Authors
│   ├── Create Category
│   ├── Get Categories
│   ├── Create Book
│   ├── Get Books
│   ├── Search Books
│   ├── Update Book
│   ├── Delete Book
│   └── Summary Report
│
└── Error Cases
    ├── 400 Bad Request
    ├── 404 Not Found
    └── Validation Error
```

---

# Environment Variable

The collection uses a single environment variable.

| Variable | Example                |
| -------- | ---------------------- |
| baseUrl  | https://localhost:5001 |

Example request:

```text
{{baseUrl}}/api/students
```

This allows the collection to work even if the API port changes.

---

# Tested HTTP Status Codes

| Status Code     | Meaning                                |
| --------------- | -------------------------------------- |
| 200 OK          | Successful request                     |
| 201 Created     | Resource created successfully          |
| 204 No Content  | Resource deleted or marked unavailable |
| 400 Bad Request | Validation or business rule failed     |
| 404 Not Found   | Requested resource does not exist      |

---

# Test Scenarios

## Successful Requests

* Create Student
* Get All Students
* Update Student
* Change Student Status
* Create Category
* Create Product
* Search Products
* Generate Stock Report
* Create Author
* Create Book
* Search Books
* Generate Book Summary Report

---

## Error Scenarios

The collection includes requests demonstrating:

### Bad Request (400)

Examples:

* Invalid email
* Negative product price
* Negative stock quantity
* Empty author name
* Duplicate ISBN
* Duplicate category name

---

### Not Found (404)

Examples:

* Student not found
* Product not found
* Book not found
* Category not found

---

### Validation Errors

Examples:

* Missing required fields
* Invalid category ID
* Invalid author ID
* Invalid pagination values

---

# Repository Structure

```text
task-05-swagger-postman-evidence-pack/

README.md

Evidence/
│
├── Swagger/
│   ├── swagger-home.png
│   ├── students-api.png
│   ├── products-api.png
│   ├── books-api.png
│   ├── authors-api.png
│   ├── reports.png
│   └── pagination.png
│
├── Postman/
│   ├── create-student.png
│   ├── get-students.png
│   ├── create-product.png
│   ├── create-book.png
│   ├── reports.png
│   ├── bad-request.png
│   ├── not-found.png
│   └── validation-error.png
│
└── Collections/
    └── TechMaster ASP.NET Phase 02.postman_collection.json
```

---

# How to Import the Postman Collection

1. Open Postman.
2. Click **Import**.
3. Select **TechMaster ASP.NET Phase 02.postman_collection.json**.
4. Import the environment file if provided.
5. Set the **baseUrl** variable to your running API.
6. Execute the requests.

---

# Demo Video Checklist

The demo video includes:

* Running the ASP.NET Core API.
* Opening Swagger.
* Creating resources.
* Updating resources.
* Searching and filtering.
* Pagination.
* Report endpoints.
* Validation errors.
* 404 responses.
* Postman collection demonstration.

---

# Learning Outcomes

Through this task, I demonstrated the ability to:

* Test RESTful APIs using Swagger.
* Build and organize a professional Postman collection.
* Verify successful and error responses.
* Document API testing evidence.
* Follow professional API review practices.
