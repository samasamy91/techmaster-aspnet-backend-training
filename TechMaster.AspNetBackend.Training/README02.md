# Task 02 - OOP Bank Account System

## Overview

This project is a simple console-based banking system developed as part of **Phase 01 - Backend Foundations** in the TechMaster ASP.NET Backend Career Training.

The project demonstrates Object-Oriented Programming (OOP) principles by separating the application into Models, Services, and UI layers while implementing common banking operations.

---

## Learning Objectives

This project demonstrates:

- Object-Oriented Programming (OOP)
- Encapsulation
- Classes and Objects
- Enums
- Collections (List)
- Separation of Concerns
- Business Logic
- Console User Interface
- Input Validation

---

# Project Structure

```
BankAccountSystem
│
├── Models
│   ├── Customer.cs
│   ├── BankAccount.cs
│   ├── Transaction.cs
│   ├── AccountType.cs
│   └── TransactionType.cs
│
├── Services
│   └── BankService.cs
│
├── UI
│   └── ConsoleMenu.cs
│
└── Program.cs
```

---

# Features

## Customer Account Management

- Create customer account
- Store customer information
- Automatic account creation
- Unique account numbers
- Initial balance validation

---

## Banking Operations

- Deposit money
- Withdraw money
- Transfer money between accounts

---

## Account Information

- View account details
- View transaction history
- View all accounts

---

# Business Rules

### Create Account

- Customer name is required
- Email is required
- Phone number is required
- Initial balance cannot be negative
- Account number must be unique

### Deposit

- Account must exist
- Deposit amount must be greater than zero

### Withdraw

- Account must exist
- Withdrawal amount must be greater than zero
- Withdrawal amount cannot exceed account balance

### Transfer

- Source account must exist
- Destination account must exist
- Source and destination cannot be the same
- Source account must have sufficient balance

---

# Implemented Classes

## Models

### Customer

- CustomerId
- FullName
- Email
- PhoneNumber
- CreatedAt

### BankAccount

- AccountNumber
- Customer
- Balance
- AccountType
- CreatedAt
- IsActive
- Transactions

### Transaction

- TransactionId
- AccountNumber
- TransactionType
- Amount
- TransactionDate
- Description
- BalanceAfterTransaction

---

# Services

## BankService

Implemented methods:

- CreateAccount()
- FindAccount()
- Deposit()
- Withdraw()
- Transfer()
- GetAccountDetails()
- GetTransactionHistory()
- GetAllAccounts()

---

# User Interface

Console menu includes:

```
====== TechMaster Bank System ======

1. Create Customer Account
2. Deposit Money
3. Withdraw Money
4. Transfer Money
5. View Account Details
6. View Transaction History
7. View All Accounts
8. Exit
```

---

# OOP Concepts Used

- Classes
- Objects
- Encapsulation
- Methods
- Enums
- Collections
- Separation of Concerns

---

# Validation

The system validates:

- Required customer information
- Duplicate account numbers
- Invalid account lookup
- Negative balances
- Invalid deposit amount
- Invalid withdrawal amount
- Insufficient balance
- Invalid transfers

---

# Screenshots

Include screenshots showing:

- Create Account
- Deposit
- Withdraw
- Transfer
- View Account Details
- Transaction History
- View All Accounts
- Invalid Operation Example

---

# Future Improvements

- Data persistence using a database
- Login system
- Account deletion
- Interest calculation
- Search by customer name
- Better email and phone validation

---
