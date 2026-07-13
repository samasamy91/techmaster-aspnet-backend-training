# Task 02 - Student Management API

## Student Information

* **Name:** Sama Samy
* **Track:** ASP.NET Backend Career Training
* **Phase:** Phase 02 - Web API Basics
* **Task:** Task 02 - Student Management API

---

# Project Overview

The Student Management API is an ASP.NET Core Web API that simulates a training center's student management system using in-memory data storage.

The project demonstrates RESTful API design, CRUD operations, DTO usage, dependency injection, service layer architecture, filtering, searching, pagination, and proper HTTP status codes without using a database.

---

# Technologies Used

* ASP.NET Core Web API (.NET 8)
* C#
* Swagger (OpenAPI)
* Dependency Injection
* In-Memory Data Storage (`List<Student>`)
* LINQ

---

# Project Structure

```text
StudentManagement/
│
├── Controllers/
│   └── StudentsController.cs
│
├── DTOs/
│   ├── CreateStudentRequest.cs
│   ├── UpdateStudentRequest.cs
│   ├── UpdateStudentStatusRequest.cs
│   ├── StudentResponse.cs
│   ├── StudentStatsResponse.cs
│   └── PagedResultResponse.cs
│
├── Models/
│   └── Student.cs
│
├── Services/
│   ├── IStudentService.cs
│   └── StudentService.cs
│
├── Program.cs
└── README.md
```

---

# Features

* Create a student
* Retrieve all students
* Retrieve a student by ID
* Update student information
* Activate or deactivate a student
* Delete (or deactivate) a student
* Search students by name or email
* Filter students by track
* Filter active/inactive students
* Pagination
* Student statistics

---

# Student Model

Each student contains:

* StudentId
* FullName
* Email
* PhoneNumber
* TrackName
* EnrollmentDate
* IsActive
* GitHubProfileUrl (Optional)
* LinkedInProfileUrl (Optional)

---

# API Endpoints

## Create Student

**POST**

```http
/api/students
```

Creates a new student.

### Request Example

```json
{
  "fullName": "Sama Samy",
  "email": "ahmed@example.com",
  "phoneNumber": "01012345678",
  "trackName": ".NET",
  "gitHubProfileUrl": "https://github.com/ahmed",
  "linkedInProfileUrl": "https://linkedin.com/in/ahmed"
}
```

### Responses

* 201 Created
* 400 Bad Request (Duplicate email or invalid request)

---

## Get All Students

**GET**

```http
/api/students
```

Supports:

* Search
* Track filtering
* Active status filtering
* Pagination

### Example

```http
GET /api/students?search=Ahmed&pageNumber=1&pageSize=5
```

### Query Parameters

| Parameter  | Description                     |
| ---------- | ------------------------------- |
| search     | Search by full name or email    |
| trackName  | Filter by track                 |
| isActive   | Filter active/inactive students |
| pageNumber | Page number                     |
| pageSize   | Number of records per page      |

### Responses

* 200 OK
* 400 Bad Request (Invalid pagination)

---

## Get Student By Id

**GET**

```http
/api/students/{id}
```

### Responses

* 200 OK
* 404 Not Found

---

## Update Student

**PUT**

```http
/api/students/{id}
```

Updates the student's information.

### Responses

* 200 OK
* 400 Bad Request
* 404 Not Found

---

## Update Student Status

**PATCH**

```http
/api/students/{id}/status
```

Activates or deactivates a student.

### Request Example

```json
{
  "isActive": false
}
```

### Responses

* 200 OK
* 404 Not Found

---

## Delete Student

**DELETE**

```http
/api/students/{id}
```

Deletes (or deactivates) a student based on the selected business rule.

### Responses

* 204 No Content
* 404 Not Found

---

## Get Students By Track

**GET**

```http
/api/students/by-track/{trackName}
```

Example:

```http
GET /api/students/by-track/.NET
```

### Responses

* 200 OK

---

## Student Statistics

**GET**

```http
/api/students/stats
```

Returns:

* Total students
* Active students
* Inactive students
* Number of students in each track

### Sample Response

```json
{
  "totalStudents": 10,
  "activeStudents": 8,
  "inactiveStudents": 2,
  "studentsPerTrack": {
    ".NET": 5,
    "Java": 3,
    "Flutter": 2
  }
}
```

---

# Business Rules

* Full name is required.
* Email is required.
* Email must be unique.
* Track name is required.
* Phone number is required.
* Student ID cannot be modified.
* Missing students return **404 Not Found**.
* Invalid requests return **400 Bad Request**.
* Successful creation returns **201 Created**.
* Successful deletion returns **204 No Content**.

---

# How to Run

1. Open the solution in Visual Studio.
2. Build the project.
3. Run the API.
4. Open Swagger.
5. Test the endpoints using Swagger or Postman.

Swagger URL:

```
https://localhost:5011/swagger
```

---

# Testing

The API was tested using:

* Swagger UI
* Postman

The following scenarios were verified:

* Create student
* Get all students
* Search students
* Filter students
* Pagination
* Get student by ID
* Update student
* Update status
* Delete student
* Get statistics

---

# Evidence

* Swagger screenshots
* Postman request/response screenshots
* Postman Collection

---

# Learning Outcomes

Through this task, I practiced:

* Building RESTful APIs with ASP.NET Core
* Using Controllers and Routing
* Creating Request and Response DTOs
* Applying Dependency Injection
* Separating business logic into a Service Layer
* Working with LINQ
* Implementing filtering, searching, and pagination
* Returning appropriate HTTP status codes
* Following clean architecture principles
