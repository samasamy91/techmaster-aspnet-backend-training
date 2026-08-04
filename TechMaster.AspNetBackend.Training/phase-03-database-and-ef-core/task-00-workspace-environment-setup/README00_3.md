# Task 00 - Workspace & Environment Setup

## Overview

This task prepares the development environment for **Phase 03 - Real Backend Data Systems**. The goal is to establish a clean project structure, configure Entity Framework Core, connect the project to SQL Server, and follow professional practices for configuration management and secret handling.

---

## Objectives

- Create the Phase 03 workspace structure.
- Initialize the ASP.NET Core Web API project.
- Install Entity Framework Core packages.
- Configure SQL Server connectivity.
- Register the application's `DbContext`.
- Keep sensitive configuration out of source control.
- Document the local development setup.

---

## Project Structure

```text
TrainingCenter.Api/
│
├── Controllers/
├── Data/
│   └── AppDbContext.cs
├── Entities/
│   ├── Students/
│   ├── Tracks/
│   ├── Enrollments/
│   ├── Payments/
│   └── Reports/
├── DTOs/
│    ├── Students/
│    ├── Tracks/
│    ├── Enrollments/
│    ├── Payments/
│    └── Reports/
├── Services/
├── Common/
│   ├── ApiResponse.cs
│   └── PaginationResult.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── TrainingCenter.Api.csproj
```

---

## Technologies

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

---

## NuGet Packages

Install the required packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

If the Entity Framework CLI is not installed:

```bash
dotnet tool install --global dotnet-ef
```

---

## Database Configuration

Store the local development connection string in **appsettings.Development.json**.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TechMasterTrainingCenterDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> **Note:** This example uses Windows Authentication. Adjust the connection string if your SQL Server configuration is different.

---

## Local Setup

### 1. Clone the repository

```bash
git clone <repository-url>
```

### 2. Navigate to the project

```bash
cd phase-03-real-backend-data-systems/task-00-workspace-environment-setup/TrainingCenter.Api
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Verify Entity Framework CLI

```bash
dotnet ef
```

### 5. Create the first migration

```bash
dotnet ef migrations add InitialTrainingCenterSchema
```

### 6. Apply the migration

```bash
dotnet ef database update
```

### 7. Run the API

```bash
dotnet run
```

Open Swagger in your browser after the application starts.

---

## Security & Configuration

To protect sensitive information:

- Do not commit production connection strings.
- Store production secrets using your hosting provider or a secure secret manager.
- Use `appsettings.Development.json` only for local development.
- Never expose passwords or database credentials in screenshots or GitHub commits.

---

## Completed Tasks

- Created the Phase 03 workspace.
- Initialized the ASP.NET Core Web API project.
- Installed Entity Framework Core packages.
- Configured SQL Server connection settings.
- Registered `AppDbContext` in the dependency injection container.
- Prepared the project for future database migrations.
- Configured Swagger for API testing.

---

## Expected Outcome

After completing this task, the project should:

- Build successfully.
- Run locally without errors.
- Connect to SQL Server using the local configuration.
- Be ready for Entity Framework Core migrations.
- Follow secure configuration practices.

---
