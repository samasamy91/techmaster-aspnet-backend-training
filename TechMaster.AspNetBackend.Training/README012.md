# Phase 02 - Task 01: REST & Routing Drill Pack

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Project:** StudentManagementAPI
* **Phase:** Phase 02 - ASP.NET Core Web API Basics

---

# Task Purpose

This task focuses on building a solid understanding of ASP.NET Core Web API fundamentals. The drills cover routing, controllers, request bodies, query strings, route parameters, headers, status codes, validation, CRUD operations, pagination, and standardized API responses.

---

# Technologies Used

* ASP.NET Core Web API (.NET 8)
* C#
* Swagger (OpenAPI)
* Postman
* Visual Studio 2022

---

# How to Run

1. Open the solution in Visual Studio.
2. Build the solution.
3. Run the project (F5 or Ctrl+F5).
4. Swagger opens automatically, or navigate to:

```
http://localhost:5282//swagger
```

5. Test endpoints using Swagger or Postman.

---

# Drill Summary

| Drill | Endpoint                                        | HTTP            | Concept                        | Status | Evidence        |
| ----: | ----------------------------------------------- | --------------- | ------------------------------ | :----: | --------------- |
|    01 | `/api/health`                                   | GET             | Basic endpoint                 |  Done | Swagger         |
|    02 | `/api/tools/echo/{name}`                        | GET             | Route parameters               |  Done | Swagger/Postman |
|    03 | `/api/calculator/add?a=10&b=5`                  | GET             | Query parameters               |  Done | Swagger/Postman |
|    04 | `/api/converter/celsius-to-fahrenheit?value=25` | GET             | Service layer & business logic |  Done | Swagger/Postman |
|    05 | `/api/grades/calculate?score=85`                | GET             | Validation & conditions        |  Done | Swagger/Postman |
|    06 | `/api/notes`                                    | POST            | Request body DTO               |  Done | Swagger/Postman |
|    07 | `/api/notes`                                    | GET             | Collection response            |  Done | Swagger/Postman |
|    08 | `/api/notes/{id}`                               | GET             | Route ID & 404                 |  Done | Swagger/Postman |
|    09 | `/api/notes/{id}`                               | PUT             | Update resource                |  Done | Swagger/Postman |
|    10 | `/api/notes/{id}`                               | DELETE          | DELETE & status codes          |  Done | Swagger/Postman |
|    11 | `/api/notes/search?keyword=api`                 | GET             | Search using query string      |  Done | Swagger/Postman |
|    12 | `/api/notes?pageNumber=1&pageSize=5`            | GET             | Pagination (Skip/Take)         |  Done | Swagger/Postman |
|    13 | `/api/request-info`                             | GET             | Reading request headers        |  Done | Swagger/Postman |
|    14 | Multiple status code endpoints                  | GET/POST/DELETE | HTTP status codes              |  Done | Swagger/Postman |
|    15 | `/api/errors/demo`                              | GET             | Standard error response        |  Done | Swagger/Postman |

---

# Drill Details

## Drill 01 – Health Check Endpoint

**Endpoint**

```
GET /api/health
```

**Purpose**

Verify that the API is running and reachable.

**Sample Response**

```json
{
  "status": "Running",
  "service": "TechMaster API",
  "time": "2026-07-12T12:00:00Z"
}
```

---

## Drill 02 – Route Parameter Echo

**Endpoint**

```
GET /api/tools/echo/{name}
```

**Purpose**

Demonstrates receiving route parameters.

**Example**

```
GET /api/tools/echo/Sama
```

**Sample Response**

```json
{
  "originalName": "Sama",
  "message": "Hello, Sama! Welcome to TechMaster API."
}
```

---

## Drill 03 – Query String Calculator

**Endpoint**

```
GET /api/calculator/add?a=10&b=5
```

**Purpose**

Demonstrates query string parameters.

**Sample Response**

```json
{
  "a": 10,
  "b": 5,
  "operation": "Addition",
  "result": 15
}
```

---

## Drill 04 – Temperature Conversion API

**Endpoint**

```
GET /api/converter/celsius-to-fahrenheit?value=25
```

**Purpose**

Converts Celsius to Fahrenheit using a service registered with Dependency Injection.

**Sample Response**

```json
{
  "celsius": 25,
  "fahrenheit": 77,
  "formulaUsed": "F = (C × 9 / 5) + 32"
}
```

---

## Drill 05 – Grade API

**Endpoint**

```
GET /api/grades/calculate?score=85
```

**Purpose**

Validates the score and returns the grade with pass/fail status.

**Sample Response**

```json
{
  "score": 85,
  "grade": "B",
  "passed": true
}
```

---

## Drill 06 – Create Note

**Endpoint**

```
POST /api/notes
```

**Purpose**

Creates a note using a JSON request body.

**Sample Request**

```json
{
  "title": "ASP.NET Core",
  "content": "Learning Web API"
}
```

---

## Drill 07 – Get Notes

**Endpoint**

```
GET /api/notes
```

**Purpose**

Returns all notes or an empty collection if none exist.

---

## Drill 08 – Get Note By Id

**Endpoint**

```
GET /api/notes/{id}
```

**Purpose**

Retrieves a single note by its identifier.

**Possible Responses**

* 200 OK
* 404 Not Found

---

## Drill 09 – Update Note

**Endpoint**

```
PUT /api/notes/{id}
```

**Purpose**

Updates an existing note.

**Sample Request**

```json
{
  "title": "Updated Title",
  "content": "Updated Content"
}
```

---

## Drill 10 – Delete Note

**Endpoint**

```
DELETE /api/notes/{id}
```

**Purpose**

Deletes a note by its identifier.

**Possible Responses**

* 204 No Content
* 404 Not Found

---

## Drill 11 – Search Notes

**Endpoint**

```
GET /api/notes/search?keyword=api
```

**Purpose**

Searches notes by title or content using a case-insensitive keyword.

---

## Drill 12 – Pagination

**Endpoint**

```
GET /api/notes?pageNumber=1&pageSize=5
```

**Purpose**

Returns paginated notes using `Skip()` and `Take()`.

**Sample Response**

```json
{
  "pageNumber": 1,
  "pageSize": 5,
  "totalCount": 20,
  "items": []
}
```

---

## Drill 13 – Header Reader

**Endpoint**

```
GET /api/request-info
```

**Required Header**

```
X-Student-Name
```

**Purpose**

Reads a custom request header and returns it with the current request path.

**Sample Response**

```json
{
  "studentName": "Sama Samy",
  "requestPath": "/api/request-info"
}
```

---

## Drill 14 – HTTP Status Code Practice

This drill demonstrates the five most common HTTP status codes.

|     Status Code | Meaning            | Example            |
| --------------: | ------------------ | ------------------ |
|          200 OK | Successful request | Get data           |
|     201 Created | Resource created   | Create note        |
|  204 No Content | Successful delete  | Delete note        |
| 400 Bad Request | Invalid request    | Validation failure |
|   404 Not Found | Resource not found | Missing note       |

---

## Drill 15 – Standard Error Response

**Endpoint**

```
GET /api/errors/demo
```

**Purpose**

Demonstrates a consistent JSON error response format.

**Standard Error Shape**

```json
{
  "success": false,
  "message": "Description of the error",
  "code": "ERROR_CODE",
  "details": [
    "Additional information"
  ]
}
```

---

# HTTP Status Codes Used

|     Status Code | Description                            |
| --------------: | -------------------------------------- |
|          200 OK | Request completed successfully.        |
|     201 Created | Resource created successfully.         |
|  204 No Content | Resource deleted successfully.         |
| 400 Bad Request | Invalid request or validation failure. |
|   404 Not Found | Requested resource was not found.      |

---

# Project Structure

```
StudentManagementAPI
│
├── Controllers
├── DTOs
├── Models
├── Services
├── Program.cs
└── README.md
```

---

# Evidence

* Swagger screenshots: `https://drive.google.com/drive/folders/1a2ra8Visb_A-WARrO-bxMGeGoNsnm3Jk?usp=drive_link`
* Postman screenshots: `https://drive.google.com/drive/folders/1a2ra8Visb_A-WARrO-bxMGeGoNsnm3Jk?usp=drive_link`
* Postman Collection: `https://samasamy68-337795.postman.co/workspace/Sama-Samy's-Workspace~1d1af13e-cfae-4a45-89e6-adb100211e58/folder/52799279-7fc2f801-93df-4e29-afd9-1ab24bb1ea65?action=share&source=copy-link&creator=52799279&ctx=documentation`

---

# Learning Outcomes

After completing these drills, I practiced:

* Creating RESTful APIs with ASP.NET Core
* Working with Controllers and Routing
* Route parameters and query strings
* Request body DTOs
* CRUD operations
* Dependency Injection
* Validation using Data Annotations
* HTTP status codes
* Pagination with `Skip()` and `Take()`
* Reading request headers
* Building consistent JSON responses
* Testing APIs using Swagger and Postman
