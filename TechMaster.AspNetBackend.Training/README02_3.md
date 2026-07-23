# Task 02 - Requirements to ERD

## Overview

This task converts a business requirements document into a production-ready database design for the TechMaster Academy Training Center system.

The objective is to identify the required entities, define their fields and relationships, establish primary and foreign keys, and produce an Entity Relationship Diagram (ERD) that can later be implemented using Entity Framework Core.

---

## Business Scenario

The system manages:

- Students
- Instructors
- Training Tracks
- Enrollments
- Payments

Students can enroll in multiple training tracks, while each training track can contain multiple students through the Enrollment entity. Payments are associated with enrollments to support installment-based payment tracking.

---

## Deliverables

- ERD Diagram (PNG/PDF)
- DesignDetails.md
- Relationship documentation
- Primary and Foreign Keys
- Business Rules
- Business Questions

---

## Database Design Summary

### Main Entities

- Student
- Instructor
- TrainingTrack
- Enrollment
- Payment

### Relationships

- Instructor → TrainingTrack (One-to-Many)
- Student → Enrollment (One-to-Many)
- TrainingTrack → Enrollment (One-to-Many)
- Enrollment → Payment (One-to-Many)

Student and TrainingTrack have a Many-to-Many relationship implemented through the Enrollment table.

---

## Design Decisions

- Integer identity columns are used as primary keys.
- Email addresses are unique for Students and Instructors.
- Monetary values use `decimal(18,2)`.
- UTC timestamps are used for audit fields.
- Soft Delete is implemented using `IsDeleted` and `DeletedAt`.
- The Enrollment entity is used instead of EF Core's automatic many-to-many relationship because it stores business data such as enrollment status, progress, and final result.
- Payments are linked to enrollments to support multiple payment transactions for a single enrollment.

---

## Business Rules

- A Student can enroll in many Training Tracks.
- A Training Track can contain many Students.
- Each Training Track must have exactly one Instructor.
- An Instructor can teach multiple Training Tracks.
- An Enrollment cannot exist without a Student and a Training Track.
- A Payment cannot exist without an Enrollment.
- Track capacity should not be exceeded.
- Soft deleted records are excluded from normal queries.
- Audit fields are maintained automatically by the system.

---

## Files Included

```
Task-02-Requirements-to-ERD/
│
├── README.md
├── DesignDetails.md
└── ERD.png (or ERD.pdf)
```

---

## Learning Outcomes

After completing this task, I was able to:

- Analyze business requirements.
- Identify database entities and relationships.
- Design normalized relational tables.
- Define primary and foreign keys.
- Model one-to-many and many-to-many relationships.
- Apply business rules to database design.
- Prepare a database structure suitable for implementation using Entity Framework Core.