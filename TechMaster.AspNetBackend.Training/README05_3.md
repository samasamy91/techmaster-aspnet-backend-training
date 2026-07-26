# Task 05 - Business Rules & Data Integrity

## Overview

This task focuses on implementing business rules inside the service layer to ensure the Training Center API enforces valid business behavior rather than acting as a simple CRUD application.

All validation logic is implemented in services, while controllers remain lightweight and responsible only for handling HTTP requests and responses.

---

# Technologies

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- LINQ
- Swagger / OpenAPI
- Postman

---

# Objectives

- Protect data integrity.
- Prevent invalid business operations.
- Implement business validation in the service layer.
- Return meaningful error messages.
- Use proper HTTP status codes.
- Prepare the project for future security and authorization features.

---

# Student Business Rules

## Implemented Rules

- Email must be unique.
- Full name is required.
- Students cannot be permanently deleted.
- Soft delete is used instead of hard delete.
- Inactive or deleted students cannot receive new enrollments.
- Deleted students are excluded from normal queries.

### Validation Examples

| Scenario | Result |
|----------|--------|
| Duplicate email | 400 Bad Request |
| Missing full name | 400 Bad Request |
| Delete student | Soft delete (`IsDeleted = true`) |
| Get students | Deleted students are hidden |

---

# Training Track Business Rules

## Implemented Rules

- Title is required.
- Code must be unique.
- Capacity must be greater than zero.
- Start date must be before end date.
- Instructor is required.
- Track capacity cannot be exceeded.
- Closed tracks cannot accept new enrollments.

### Validation Examples

| Scenario | Result |
|----------|--------|
| Capacity = 0 | 400 Bad Request |
| Duplicate code | 400 Bad Request |
| Missing instructor | 400 Bad Request |
| Full track | 400 Bad Request |
| Closed track enrollment | 400 Bad Request |

---

# Enrollment Business Rules

## Implemented Rules

- Students cannot have multiple active enrollments in the same track.
- New enrollments start with **Pending** status.
- Enrollments become **Active** only after a successful payment or an allowed status transition.
- Completed enrollments cannot be cancelled.
- Cancelled enrollments do not count toward track capacity.

### Validation Examples

| Scenario | Result |
|----------|--------|
| Duplicate active enrollment | 400 Bad Request |
| New enrollment | Status = Pending |
| Cancel completed enrollment | 400 Bad Request |
| Cancelled enrollment | Excluded from capacity calculations |

---

# Payment Business Rules

## Implemented Rules

- Payment amount must be greater than zero.
- Payments cannot exceed the remaining balance.
- Supported payment statuses:
  - Pending
  - Paid
  - Failed
  - Refunded
- Only successful (**Paid**) payments are included in revenue reports.
- Failed payments do not activate enrollments.

### Validation Examples

| Scenario | Result |
|----------|--------|
| Amount = 0 | 400 Bad Request |
| Overpayment | 400 Bad Request |
| Failed payment | Enrollment remains unchanged |
| Revenue reports | Count only Paid payments |

---

# Service Layer Validation

Business validation is implemented inside the service layer rather than controllers.

Examples include:

- Duplicate email validation
- Capacity checking
- Duplicate enrollment prevention
- Payment validation
- Enrollment status transitions
- Track availability checks

Controllers simply call services and return the appropriate HTTP response.

---

# HTTP Response Codes

| Status Code | Description |
|-------------|-------------|
| 200 OK | Request completed successfully |
| 201 Created | Resource created successfully |
| 400 Bad Request | Business rule validation failed |
| 404 Not Found | Requested resource does not exist |

---

# Error Response Format

```json
{
  "success": false,
  "message": "Track capacity has been reached.",
  "data": null
}
```

---

# Business Rule Flow

```
Client Request
        │
        ▼
Controller
        │
        ▼
Service Layer
        │
        ├── Validate Business Rules
        ├── Validate Related Data
        ├── Apply Business Logic
        ▼
Entity Framework Core
        ▼
SQL Server
```

---

# Testing

The business rules were verified using:

- Swagger UI
- Postman

Test scenarios include both successful and invalid requests.

---

# Evidence

The `evidence` folder contains screenshots demonstrating:

## Students

- Duplicate email rejection
- Soft delete
- Deleted students excluded from results

## Tracks

- Capacity validation
- Duplicate code validation
- Missing instructor validation
- Full track validation
- Closed track enrollment rejection

## Enrollments

- Duplicate enrollment prevention
- Default Pending status
- Completed enrollment cancellation rejection

## Payments

- Zero amount rejection
- Overpayment rejection
- Failed payment behavior
- Revenue calculation using only Paid payments

---

# Project Structure

```
TrainingCenter.Api
│
├── Controllers
├── Services
├── DTOs
├── Entities
├── Data
├── Common
├── Migrations
├── Program.cs
└── appsettings.json
```

---

# Learning Outcomes

After completing this task, the API now:

- Enforces business rules through the service layer.
- Protects database consistency.
- Prevents invalid operations.
- Uses soft deletion.
- Validates relationships before saving data.
- Returns consistent API responses.
- Supports reliable business reporting.
- Follows clean architecture principles.

---

# Deliverables

- ✅ Student business rules
- ✅ Track business rules
- ✅ Enrollment business rules
- ✅ Payment business rules
- ✅ Service-layer validation
- ✅ Consistent API error responses
- ✅ Swagger testing
- ✅ Postman collection
- ✅ Evidence screenshots
- ✅ Business rules documentation