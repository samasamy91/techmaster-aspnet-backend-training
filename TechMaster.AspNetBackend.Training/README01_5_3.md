# Drill 05 - One-to-One Payment Summary

## Overview

This drill demonstrates how to implement a **one-to-one relationship** in Entity Framework Core between an **Enrollment** and its **PaymentSummary**.

Each enrollment has exactly one payment summary that stores the student's payment information, including the required amount, paid amount, remaining balance, and payment status.

---

## Learning Objectives

By completing this drill, you will learn how to:

- Create a one-to-one relationship in EF Core.
- Configure a unique foreign key.
- Store monetary values using the `decimal` data type.
- Use enums to represent payment status.
- Generate and apply database migrations.
- Retrieve related entities using navigation properties.

---

## Relationship

```
Enrollment (1)
      │
      │ One-to-One
      │
PaymentSummary (1)
```

Each **Enrollment** has one **PaymentSummary**, and each **PaymentSummary** belongs to one **Enrollment**.

---

## Entity Structure

### Enrollment

| Property | Type |
|----------|------|
| Id | int |
| StudentId | int |
| TrackId | int |
| EnrollmentDate | DateTime |
| PaymentSummary | Navigation Property |

---

### PaymentSummary

| Property | Type |
|----------|------|
| Id | int |
| EnrollmentId | int |
| TotalRequired | decimal |
| TotalPaid | decimal |
| RemainingAmount | decimal |
| PaymentStatus | PaymentStatus |
| Enrollment | Navigation Property |

---

## Payment Status

The payment status is represented using an enum.

Possible values:

- Pending
- PartiallyPaid
- Paid

---

## Database Relationship

- One Enrollment has one Payment Summary.
- PaymentSummary contains a **unique foreign key** (`EnrollmentId`).
- Deleting an enrollment also removes its payment summary if cascade delete is enabled.

---

## Decimal Configuration

Monetary values are stored using:

```csharp
decimal(18,2)
```

This ensures accurate storage of financial data and avoids floating-point precision issues.

---

## Remaining Amount

The remaining amount is calculated using the following formula:

```
RemainingAmount = TotalRequired - TotalPaid
```

This value can be implemented as a calculated property in C# or stored in the database depending on project requirements.

---

## Migration

Create the migration:

```bash
dotnet ef migrations add AddPaymentSummary
```

Apply the migration:

```bash
dotnet ef database update
```

---

## Testing

### Create an Enrollment

Create an enrollment record before creating a payment summary.

### Create a Payment Summary

Example:

```json
{
  "enrollmentId": 1,
  "totalRequired": 5000,
  "totalPaid": 3000,
  "paymentStatus": "PartiallyPaid"
}
```

Expected result:

```
RemainingAmount = 2000
```

---

## Expected Database Schema

### Enrollments

| Column |
|---------|
| Id |
| StudentId |
| TrackId |
| EnrollmentDate |

---

### PaymentSummaries

| Column | Type |
|---------|------|
| Id | int |
| EnrollmentId | int (Unique FK) |
| TotalRequired | decimal(18,2) |
| TotalPaid | decimal(18,2) |
| PaymentStatus | int |

---

## Evidence

This drill includes:

- PaymentSummary entity.
- One-to-one relationship configuration.
- Unique foreign key.
- Decimal money fields.
- PaymentStatus enum.
- EF Core migration (`AddPaymentSummary`).
- SQL Server database schema.
- Successful API execution.
- Screenshot of the PaymentSummary table linked to an Enrollment.

---

## Acceptance Checklist

- [x] PaymentSummary entity created.
- [x] One-to-one relationship implemented.
- [x] EnrollmentId configured as a unique foreign key.
- [x] TotalRequired stored as decimal.
- [x] TotalPaid stored as decimal.
- [x] RemainingAmount calculated correctly.
- [x] PaymentStatus implemented.
- [x] Migration created successfully.
- [x] Database updated successfully.
- [x] Payment summary linked to its enrollment.

---

## Technologies

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server
- Swagger

---

## Author

**Sama Samy**

Backend Developer Trainee

TechMaster Academy – ASP.NET Backend Career Training

Phase 03 – Task 01 – Drill 05