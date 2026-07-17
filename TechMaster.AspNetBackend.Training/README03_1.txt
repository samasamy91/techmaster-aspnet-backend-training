# Task 03 - Employee Management Console App

## Overview

This project is a simple HR management console application built with C#. It allows HR employees to manage employee records, search and filter employees, and generate salary reports using OOP principles and LINQ.

---

## Features

- Add Employee
- Update Employee
- Deactivate Employee
- Search Employee by ID or Name
- Filter Employees by Department
- Sort Employees
  - Salary (Ascending)
  - Salary (Descending)
  - Hire Date (Ascending)
  - Hire Date (Descending)
  - Name
- Salary Reports
  - Average Salary
  - Highest Salary Employee
  - Lowest Salary Employee
  - Total Payroll
  - Employee Count by Department
  - Active/Inactive Employee Count
- View All Employees

---

## Technologies Used

- C#
- .NET Console Application
- Object-Oriented Programming (OOP)
- LINQ
- Collections (List, Dictionary)
- Separation of Concerns

---

## Project Structure

```
EmployeeManagement/
│
├── Models/
│   ├── Employee.cs
│   └── Department.cs
│
├── Services/
│   ├── EmployeeService.cs
│   ├── EmployeeReportService.cs
│   └── EmployeeSeeder.cs
│
├── Helpers/
│   └── ValidationHelper.cs
│
├── UI/
│   └── ConsoleMenu.cs
│
└── Program.cs
```

---

## Seed Data

The application starts with **12 predefined employees** distributed across different departments:

- IT
- HR
- Sales
- Finance
- Marketing
- Support

This allows searching, filtering, sorting, and reporting without manually adding employees first.

---

## Validation

The application validates:

- Employee ID uniqueness
- Required fields
- Positive salary
- Hire date cannot be in the future
- Empty values
- Existing employee before update/deactivation

---

## LINQ Used

The project uses LINQ for:

- Searching employees
- Filtering by department
- Sorting employees
- Calculating average salary
- Finding highest salary
- Finding lowest salary
- Calculating total payroll
- Counting employees by department

---

## Menu

```
====== Employee Management System ======

1. Add Employee
2. Update Employee
3. Deactivate Employee
4. Search Employee
5. Filter by Department
6. Sort Employees
7. Show Salary Reports
8. View All Employees
9. Exit
```

---

## Sample Output

```
Employee ID : EMP-001
Name        : Mohamed Ayman
Department  : IT
Position    : Backend Developer
Salary      : 20000
Status      : Active
```

---

## OOP Concepts Used

- Classes
- Objects
- Encapsulation
- Services
- Separation of Concerns
- Reusable Helper Class

---

## Project Status

| Feature | Status = Done all |

| Add Employee 
| Update Employee 
| Deactivate Employee 
| Search Employee 
| Filter by Department 
| Sort Employees 
| Salary Reports 
| View All Employees 
| Validation 
| Seed Data 

---

## Learning Outcomes

This project helped practice:

- Object-Oriented Programming
- LINQ queries
- Collections
- Business validation
- Console application structure
- Clean code organization
- Service-based architecture