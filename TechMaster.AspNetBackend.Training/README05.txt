# Task 05 - Debug & Refactor Pack

## Overview

This project refactors a poorly structured order calculator console application into a clean, maintainable, object-oriented design without changing its original functionality. The focus is on separating business logic from user interaction, improving readability, adding validation, and following clean code principles.

---

## Technologies Used

- C#
- .NET Console Application
- Object-Oriented Programming (OOP)
- Separation of Concerns
- Clean Code Principles

---

## Project Structure

```
Task05.OrderCalculatorRefactor/
│
├── original-bad-code/
│   └── Program.cs
│
├── Refactored/
│   ├── Models/
│   │   ├── Customer.cs
│   │   ├── Order.cs
│   │   └── CustomerType.cs
│   │
│   ├── Services/
│   │   ├── OrderCalculator.cs
│   │   └── ValidationHelper.cs
│   │
│   ├── UI/
│   │   ├── ConsoleMenu.cs
│   │   └── ReceiptPrinter.cs
│   │
│   └── Program.cs
│
└── README.md
```

---

# Business Rules

The application follows these business rules:

- Product price must be greater than zero.
- Quantity must be greater than zero.
- Customer name is required.
- Product name is required.
- Tax rate = **14%**.
- Shipping = **50** if subtotal after discount is below **1000**.
- Shipping is free when subtotal after discount is **1000 or more**.
- Discount is applied before tax.
- Tax is calculated after discount.
- Shipping is added after tax.

---

# Models

## Customer

Stores customer information.

Properties:

- Name
- CustomerType

---

## Order

Stores order information.

Properties:

- ProductName
- Price
- Quantity
- SubTotal (calculated)

---

## CustomerType

Enum values:

- Regular
- Silver
- Gold
- VIP

---

# Services

## OrderCalculator

Contains all business logic.

Methods:

- CalculateDiscount()
- CalculateTax()
- CalculateShipping()
- CalculateFinalTotal()

Business constants:

```csharp
TaxRate = 0.14m
ShippingFee = 50m
SilverDiscount = 5%
GoldDiscount = 10%
VipDiscount = 15%
```

No calculation logic exists in the UI.

---

## ValidationHelper

Validates:

- Customer name
- Product name
- Product price
- Quantity

This prevents invalid orders from being processed.

---

# UI

## ConsoleMenu

Responsible only for:

- Reading user input
- Validating input
- Calling OrderCalculator
- Printing the receipt

No business calculations are performed here.

---

## ReceiptPrinter

Displays a formatted receipt including:

- Customer
- Customer Type
- Product
- Price
- Quantity
- Subtotal
- Discount
- Tax
- Shipping
- Final Total

---

# Before Refactoring

The original application had:

- One large Main() method
- Unclear variable names
- No validation
- Magic numbers
- Mixed UI and business logic
- Difficult to maintain
- Difficult to test

---

# Improvements Made

The following improvements were implemented:

1. Preserved the original code in a separate folder.
2. Created a Customer model.
3. Created an Order model.
4. Added CustomerType enum.
5. Extracted all calculations into OrderCalculator.
6. Added ValidationHelper for input validation.
7. Moved receipt printing into ReceiptPrinter.
8. Replaced magic numbers with named constants.
9. Simplified Program.cs.
10. Separated UI, Models, and Services.
11. Improved variable names.
12. Added clean project structure.
13. Followed Separation of Concerns.
14. Improved readability and maintainability.

---

# Console Flow

```
Enter Customer Name

↓

Enter Product Name

↓

Enter Price

↓

Enter Quantity

↓

Enter Customer Type

↓

Validate Input

↓

Calculate Order

↓

Print Receipt
```

---

# Example Output

```
========== RECEIPT ==========

Customer : Ahmed Ali
Type     : Gold

Product  : Laptop
Price    : 10000
Quantity : 2

-----------------------------

Subtotal : 20000
Discount : 2000
Tax      : 2520
Shipping : 0

-----------------------------

Final Total : 20520

=============================
```

---

# Refactoring Benefits

The refactored version provides:

- Better readability
- Easier maintenance
- Reusable business logic
- Cleaner project structure
- Easier debugging
- Better validation
- Separation of concerns
- More professional code organization

---

# Commit History

Example commits:

```
add original messy order calculator

extract customer and order models

add validation helper

move calculations to order calculator

extract receipt printer

refactor console menu and simplify program

write README documentation
```

---

# Learning Outcomes

This project demonstrates:

- Object-Oriented Programming
- Refactoring legacy code
- Clean Code principles
- Separation of Concerns
- Business logic encapsulation
- Validation
- Constants instead of magic numbers
- Professional project organization

---

# Project Status

| Feature | Status |
|----------|--------|
| Original Bad Code | Done |
| Customer Model | Done |
| Order Model | Done |
| CustomerType Enum | Done |
| OrderCalculator | Done |
| ValidationHelper | Done |
| ReceiptPrinter | Done |
| ConsoleMenu | Done |
| Business Rules | Done |
| Input Validation | Done |
| Refactoring Complete | Done |
| README | Done |

---

# Future Improvements

- Save orders to a database using Entity Framework Core.
- Add multiple products per order.
- Generate receipt numbers.
- Export receipts to PDF.
- Build an ASP.NET Core Web API version.
- Add unit tests for OrderCalculator.