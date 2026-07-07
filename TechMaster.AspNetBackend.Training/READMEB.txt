# Scenario B - Simple Store & Orders System

## Overview

This project implements a simple relational database for a store management system using SQL Server. 
The system manages customers, products, suppliers, categories, orders, and order items while demonstrating database design concepts such as primary keys, foreign keys, relationships, and SQL queries.

---

# Objectives

* Design a normalized relational database.
* Implement one-to-many relationships.
* Insert sample data into all tables.
* Practice SQL queries using filtering, grouping, aggregation, and joins.
* Simulate a real-world store ordering system.

---

# Database Structure

The database contains the following tables:

* Customers
* Categories
* Suppliers
* Products
* Orders
* OrderItems

---

# Tables

## Customers

| Column      | Description       |
| ----------- | ----------------- |
| CustomerId  | **Primary Key**   |
| FullName    | Customer name     |
| Email       | Email address     |
| PhoneNumber | Contact number    |
| CreatedAt   | Registration date |

---

## Categories

| Column      | Description          |
| ----------- | -------------------- |
| CategoryId  | **Primary Key**      |
| Name        | Category name        |
| Description | Category description |

---

## Suppliers

| Column      | Description     |
| ----------- | --------------- |
| SupplierId  | **Primary Key** |
| Name        | Supplier name   |
| PhoneNumber | Contact number  |
| Email       | Supplier email  |

---

## Products

| Column        | Description                              |
| ------------- | ---------------------------------------- |
| ProductId     | **Primary Key**                          |
| Name          | Product name                             |
| Price         | Product price                            |
| StockQuantity | Quantity in stock                        |
| CategoryId    | **Foreign Key → Categories(CategoryId)** |
| SupplierId    | **Foreign Key → Suppliers(SupplierId)**  |
| IsAvailable   | Product availability                     |

---

## Orders

| Column      | Description                             |
| ----------- | --------------------------------------- |
| OrderId     | **Primary Key**                         |
| CustomerId  | **Foreign Key → Customers(CustomerId)** |
| OrderDate   | Order date                              |
| Status      | Order status                            |
| TotalAmount | Total order amount                      |

---

## OrderItems

| Column      | Description                           |
| ----------- | ------------------------------------- |
| OrderItemId | **Primary Key**                       |
| OrderId     | **Foreign Key → Orders(OrderId)**     |
| ProductId   | **Foreign Key → Products(ProductId)** |
| Quantity    | Ordered quantity                      |
| UnitPrice   | Product price at purchase             |

---

# Primary Keys

| Table      | Primary Key |
| ---------- | ----------- |
| Customers  | CustomerId  |
| Categories | CategoryId  |
| Suppliers  | SupplierId  |
| Products   | ProductId   |
| Orders     | OrderId     |
| OrderItems | OrderItemId |

---

# Foreign Keys

| Table      | Foreign Key | References             |
| ---------- | ----------- | ---------------------- |
| Products   | CategoryId  | Categories(CategoryId) |
| Products   | SupplierId  | Suppliers(SupplierId)  |
| Orders     | CustomerId  | Customers(CustomerId)  |
| OrderItems | OrderId     | Orders(OrderId)        |
| OrderItems | ProductId   | Products(ProductId)    |

---

# Relationships

The database uses the following one-to-many relationships:

* Customer (1) → (Many) Orders
* Order (1) → (Many) OrderItems
* Product (1) → (Many) OrderItems
* Category (1) → (Many) Products
* Supplier (1) → (Many) Products

---

# Sample Data

The database includes sample records for:

* Customers
* Categories
* Suppliers
* Products
* Orders
* Order Items

This data allows all required SQL queries to be tested.

---

# SQL Queries Implemented

1. Select all products.
2. Select available products.
3. Select products by category.
4. Select products with low stock.
5. Select orders for a specific customer.
6. Display order details using JOIN.
7. Calculate total sales.
8. Count products per category.
9. Display best-selling products.
10. Display suppliers with their products.

---

# SQL Concepts Used

* CREATE DATABASE
* CREATE TABLE
* PRIMARY KEY
* FOREIGN KEY
* INSERT INTO
* SELECT
* WHERE
* ORDER BY
* INNER JOIN
* GROUP BY
* COUNT()
* SUM()
* Aggregate Functions

---

# Project Structure

```text
ScenarioB-StoreManagement/
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
* Insert and manage sample data.
* Write SQL queries using JOIN.
* Filter data using WHERE.
* Group records using GROUP BY.
* Calculate totals using SUM.
* Count records using COUNT.
* Generate reports from relational databases.

---

# Conclusion

This project demonstrates the fundamentals of relational database design and SQL querying through a simple store management system. 
It provides practical experience with table relationships, data retrieval, aggregation, and reporting, building a solid foundation for future database and ASP.NET Core development.
