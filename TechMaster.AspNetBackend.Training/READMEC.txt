# Scenario C - Training Center Registration System

## Overview

This project implements a relational database for a Training Center Registration System using SQL Server. 
The database manages students, instructors, training tracks, registrations, and payments. It demonstrates database design principles, relationships, primary and foreign keys, and SQL queries for reporting and data retrieval.

---

# Objectives

* Design a normalized relational database.
* Implement one-to-many and one-to-one relationships.
* Insert sample data into all tables.
* Practice SQL queries using filtering, grouping, joins, and aggregate functions.
* Simulate a real-world training center registration system.

---

# Database Structure

The database consists of the following tables:

* Students
* Instructors
* Tracks
* Registrations
* Payments

---

# Tables

## Students

| Column      | Description           |
| ----------- | --------------------- |
| StudentId   | **Primary Key**       |
| FullName    | Student name          |
| Email       | Student email         |
| PhoneNumber | Phone number          |
| CreatedAt   | Account creation date |

---

## Instructors

| Column         | Description             |
| -------------- | ----------------------- |
| InstructorId   | **Primary Key**         |
| FullName       | Instructor name         |
| Email          | Instructor email        |
| Specialization | Teaching specialization |

---

## Tracks

| Column        | Description                                 |
| ------------- | ------------------------------------------- |
| TrackId       | **Primary Key**                             |
| Title         | Track title                                 |
| Description   | Track description                           |
| DurationWeeks | Track duration                              |
| StartDate     | Track start date                            |
| InstructorId  | **Foreign Key → Instructors(InstructorId)** |

---

## Registrations

| Column           | Description                           |
| ---------------- | ------------------------------------- |
| RegistrationId   | **Primary Key**                       |
| StudentId        | **Foreign Key → Students(StudentId)** |
| TrackId          | **Foreign Key → Tracks(TrackId)**     |
| RegistrationDate | Registration date                     |
| Status           | Registration status                   |

---

## Payments

| Column         | Description                                                |
| -------------- | ---------------------------------------------------------- |
| PaymentId      | **Primary Key**                                            |
| RegistrationId | **Foreign Key → Registrations(RegistrationId)** *(Unique)* |
| Amount         | Payment amount                                             |
| PaymentDate    | Payment date                                               |
| PaymentStatus  | Payment status                                             |

---

# Primary Keys

| Table         | Primary Key    |
| ------------- | -------------- |
| Students      | StudentId      |
| Instructors   | InstructorId   |
| Tracks        | TrackId        |
| Registrations | RegistrationId |
| Payments      | PaymentId      |

---

# Foreign Keys

| Table         | Foreign Key    | References                    |
| ------------- | -------------- | ----------------------------- |
| Tracks        | InstructorId   | Instructors(InstructorId)     |
| Registrations | StudentId      | Students(StudentId)           |
| Registrations | TrackId        | Tracks(TrackId)               |
| Payments      | RegistrationId | Registrations(RegistrationId) |

---

# Relationships

The database uses the following relationships:

* **Instructor (1) → (Many) Tracks**
* **Student (1) → (Many) Registrations**
* **Track (1) → (Many) Registrations**
* **Registration (1) → (1) Payment**

---

# Sample Data

The database contains sample records for:

* Students
* Instructors
* Tracks
* Registrations
* Payments

The inserted data allows all required SQL queries to be executed and tested successfully.

---

# SQL Queries Implemented

The project includes the following SQL queries:

1. Select all students.
2. Select all tracks.
3. Select students registered in a specific track.
4. Count students per track.
5. Select unpaid registrations.
6. Select tracks by instructor.
7. Select registrations with payment status using JOIN.
8. Select tracks starting after a specific date.
9. Count tracks per instructor.
10. Select student registration history.

---

# SQL Concepts Used

This project demonstrates the following SQL concepts:

* CREATE DATABASE
* CREATE TABLE
* PRIMARY KEY
* FOREIGN KEY
* IDENTITY
* INSERT INTO
* SELECT
* WHERE
* INNER JOIN
* LEFT JOIN
* GROUP BY
* ORDER BY
* COUNT()
* Aggregate Functions
* Variables using `DECLARE`

---

# Project Structure

```text
ScenarioC-TrainingCenter/
│
├── Database.sql
├── SeedData.sql
├── Queries.sql
└── README.md
```

---

# Learning Outcomes

Through this project, I learned how to:

* Design relational databases.
* Create primary and foreign keys.
* Implement one-to-many and one-to-one relationships.
* Insert realistic sample data.
* Retrieve data using SQL queries.
* Combine tables using JOIN operations.
* Generate reports using GROUP BY and COUNT().
* Filter records using WHERE.
* Sort results using ORDER BY.
* Build SQL reports similar to those used in real-world applications.

---

# Conclusion

This project demonstrates the fundamentals of relational database design and SQL querying through a Training Center Registration System. 
It provides practical experience in designing database schemas, enforcing relationships, inserting data, and generating reports using SQL Server, preparing a strong foundation for future ASP.NET Core and Entity Framework Core applications.
