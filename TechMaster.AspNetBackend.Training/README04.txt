# Task 04 - Product Catalog with LINQ

## Overview

This project is a console application built with C# to practice LINQ queries through a simple product catalog system. It simulates common backend operations such as searching, filtering, sorting, grouping, reporting, projection, and pagination. The project uses a fixed dataset of 25 products to produce consistent query results.

---

## Technologies Used

- C#
- .NET Console Application
- LINQ
- Object-Oriented Programming (OOP)
- Collections (List<T>)
- DTO Projection
- Grouping & Aggregation

---

## Project Structure

```
ProductCatalog/
│
├── Models/
│   ├── Product.cs
│   ├── ProductSummary.cs
│   ├── CategoryCount.cs
│   ├── CategoryStockValue.cs
│   ├── CategoryStats.cs
│   └── SupplierReport.cs
│
├── Services/
│   ├── ProductSeeder.cs
│   └── ProductQueryService.cs
│
├── UI/
│   └── ConsoleMenu.cs
│
└── Program.cs
```

---

## Product Model

Each product contains the following fields:

- ProductId
- Name
- Category
- Price
- StockQuantity
- CreatedAt
- IsAvailable
- SupplierName

---

## Seed Data

The application starts with **25 predefined products** distributed across multiple categories.

Categories include:

- Electronics
- Furniture
- Stationery
- Accessories

Suppliers include:

- TechSupplier
- HomeSupplier
- PaperSupplier
- BagSupplier

---

# Implemented LINQ Queries

| # | Query | LINQ Concept |
|---|-------------------------------|-----------------------------|
| 1 | Get Available Products | Where |
| 2 | Filter by Category | Where |
| 3 | Filter by Price Range | Where |
| 4 | Search by Product Name | Where + Contains |
| 5 | Sort by Price Ascending | OrderBy |
| 6 | Sort by Price Descending | OrderByDescending |
| 7 | Group Products by Category | GroupBy |
| 8 | Count Products per Category | GroupBy + Count |
| 9 | Calculate Total Stock Value | Sum |
|10 | Stock Value per Category | GroupBy + Sum |
|11 | Top 5 Most Expensive Products | OrderByDescending + Take |
|12 | Low Stock Products | Where |
|13 | Out of Stock Products | Where |
|14 | Product Summary Projection | Select |
|15 | Supplier Report | GroupBy + Select |
|16 | Recently Added Products | Where + DateTime |
|17 | Category Statistics | GroupBy + Select |
|18 | Products Above Average Price | Average + Where |
|19 | Search & Filter Combined | Where Chain |
|20 | Pagination Simulation | Skip + Take |

---

## DTO Models

The following DTO classes were created to return summarized data instead of full Product objects.

- ProductSummary
- CategoryCount
- CategoryStockValue
- SupplierReport
- CategoryStats

These simulate the DTOs commonly returned from ASP.NET Core Web APIs.

---

## Query Highlights

### Product Summary Projection

Uses `Select()` to create lightweight objects containing only the required fields instead of returning the full Product model.

Example:

- Name
- Category
- Price
- Stock Status

---

### Supplier Report

Groups products by supplier and calculates:

- Product Count
- Average Price
- Total Stock Value

using:

- GroupBy()
- Select()
- Count()
- Average()
- Sum()

---

### Search & Filter

Implements chained filtering similar to real backend APIs.

Supports filtering by:

- Category
- Minimum Price
- Maximum Price
- Availability

using multiple `Where()` statements.

---

### Pagination

Implements paging using:

- Skip()
- Take()

Example:

```
Page 2
Page Size 5

Returns products 6–10
```

This simulates pagination used in REST APIs.

---

## Console Menu

```
========== Product Catalog ==========
1. Get Available Products
2. Filter by Category
3. Filter by Price Range
4. Search by Product Name
5. Sort by Price Ascending
6. Sort by Price Descending
7. Group Products by Category
8. Count Products per Category
9. Total Stock Value
10. Stock Value per Category
11. Top 5 Most Expensive Products
12. Low Stock Products
13. Out of Stock Products
14. Product Summary
15. Supplier Report
16. Recently Added Products
17. Category Statistics
18. Products Above Average Price
19. Search & Filter
20. Pagination
21. Exit
```

---

## LINQ Concepts Practiced

- Where
- Select
- OrderBy
- OrderByDescending
- GroupBy
- Count
- Sum
- Average
- Max
- Min
- Contains
- Skip
- Take

---

## Validation

The application validates:

- Empty search keyword
- Invalid category
- Invalid page number
- Invalid page size
- Price range input
- Search results not found

---

## Sample Output

```
Top 5 Most Expensive Products

Laptop Pro 14
EGP 45,000

Projector
EGP 22,000

Office Sofa
EGP 15,500

Meeting Table
EGP 12,500

Monitor 27 inch
EGP 9,000
```

---

## Learning Outcomes

This project helped practice:

- LINQ fundamentals
- Filtering collections
- Sorting data
- Grouping and aggregation
- Projection using DTOs
- Reporting
- Pagination
- Backend query thinking
- Writing clean service methods
- Separation of concerns

---

## Project Status

| Feature | Status |
|----------|--------|
| Product Seed Data | Done |
| Product Model | Done |
| Product Query Service | Done |
| Console Menu | Done |
| LINQ Queries 1–20 | Done |
| DTO Projection | Done |
| Reports | Done |
| Pagination | Done |
| Validation | Done |

---

## Future Improvements

- Replace in-memory data with Entity Framework Core.
- Connect to SQL Server.
- Expose queries through ASP.NET Core Web API.
- Add dynamic sorting and filtering.
- Add asynchronous LINQ queries.
- Implement repository pattern.