# Scenario A - Library Management System

## Overview

This project implements a relational database for a Library Management System using SQL Server. The database manages authors, book categories, books, library members, and borrowing records. It demonstrates database design, table relationships, primary and foreign keys, and SQL queries for data retrieval and reporting.

---

# Objectives

* Design a normalized relational database.
* Implement one-to-many relationships.
* Insert sample data into all tables.
* Practice SQL queries using filtering, grouping, joins, and aggregate functions.
* Simulate a real-world library management system.

---

# Database Structure

The database consists of the following tables:

* Authors
* Categories
* Books
* Members
* BorrowRecords

---

# Tables

## Authors

| Column    | Description      |
| --------- | ---------------- |
| AuthorId  | **Primary Key**  |
| FullName  | Author name      |
| BirthDate | Date of birth    |
| Country   | Author's country |

---

## Categories

| Column      | Description          |
| ----------- | -------------------- |
| CategoryId  | **Primary Key**      |
| Name        | Category name        |
| Description | Category description |

---

## Books

| Column          | Description                              |
| --------------- | ---------------------------------------- |
| BookId          | **Primary Key**                          |
| Title           | Book title                               |
| ISBN            | ISBN number                              |
| PublishedYear   | Publication year                         |
| AvailableCopies | Number of available copies               |
| AuthorId        | **Foreign Key → Authors(AuthorId)**      |
| CategoryId      | **Foreign Key → Categories(CategoryId)** |

---

## Members

| Column      | Description     |
| ----------- | --------------- |
| MemberId    | **Primary Key** |
| FullName    | Member name     |
| Email       | Email address   |
| PhoneNumber | Contact number  |
| JoinDate    | Membership date |
| IsActive    | Member status   |

---

## BorrowRecords

| Column         | Description                         |
| -------------- | ----------------------------------- |
| BorrowRecordId | **Primary Key**                     |
| BookId         | **Foreign Key → Books(BookId)**     |
| MemberId       | **Foreign Key → Members(MemberId)** |
| BorrowDate     | Borrow date                         |
| DueDate        | Due date                            |
| ReturnDate     | Return date                         |
| Status         | Borrow status                       |

---

# Primary Keys

| Table         | Primary Key    |
| ------------- | -------------- |
| Authors       | AuthorId       |
| Categories    | CategoryId     |
| Books         | BookId         |
| Members       | MemberId       |
| BorrowRecords | BorrowRecordId |

---

# Foreign Keys

| Table         | Foreign Key | References             |
| ------------- | ----------- | ---------------------- |
| Books         | AuthorId    | Authors(AuthorId)      |
| Books         | CategoryId  | Categories(CategoryId) |
| BorrowRecords | BookId      | Books(BookId)          |
| BorrowRecords | MemberId    | Members(MemberId)      |

---

# Relationships

The database uses the following one-to-many relationships:

* **Author (1) → (Many) Books**
* **Category (1) → (Many) Books**
* **Member (1) → (Many) BorrowRecords**
* **Book (1) → (Many) BorrowRecords**

---

# Sample Data

The database contains sample records for:

* Authors
* Categories
* Books
* Members
* Borrow Records

The inserted data allows all required SQL queries to be executed and tested successfully.

---

# SQL Queries Implemented

The SQL file [Queries] inculdes all the queries

The project includes the following SQL queries:

1. Select all books.
2. Select all active members.
3. Select books by category.
4. Count books per category.
5. Display borrow records with member names and book titles using JOIN.
6. Select overdue books.
7. Display borrowing history for a specific member.
8. Select available books.
9. Count how many books each author has.
10. Display the top 5 most borrowed books.

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
* TOP
* Aggregate Functions
* Variables using `DECLARE`

---

# Project Structure

```text
ScenarioA-LibraryManagement/
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
* Build one-to-many relationships.
* Insert realistic sample data.
* Retrieve data using SQL queries.
* Combine multiple tables using JOIN operations.
* Generate reports using GROUP BY and COUNT().
* Filter records using WHERE.
* Identify overdue borrowed books.
* Build SQL reports similar to those used in real library systems.

---

# Conclusion

This project demonstrates the fundamentals of relational database design and SQL querying through a Library Management System. It provides practical experience in designing database schemas, managing relationships, inserting data, and generating meaningful reports using SQL Server. These concepts provide a strong foundation for future work with ASP.NET Core and Entity Framework Core.
