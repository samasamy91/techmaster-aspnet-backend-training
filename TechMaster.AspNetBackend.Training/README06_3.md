# Task 06 - Production Hosting & Remote Database

## Overview

This task demonstrates deploying the **Training Center Registration API** to a live hosting environment using **RunASP.NET** and connecting it to a **remote SQL Server database**.

The deployed API is publicly accessible through Swagger, uses Entity Framework Core with SQL Server, and supports both reading and writing data from the hosted database.

---

# Objectives

- Deploy an ASP.NET Core Web API to a production hosting provider.
- Configure a remote SQL Server database.
- Publish the API using Visual Studio Web Deploy.
- Apply Entity Framework Core migrations.
- Verify the API works online.
- Secure production credentials by keeping them out of the GitHub repository.

---

# Technology Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- RunASP.NET Hosting
- Visual Studio 2022
- Swagger / OpenAPI
- Postman

---

# Project Structure

```
task-06-production-hosting/

│
├── README.md
├── evidence/
│   ├── local-swagger.png
│   ├── local-database.png
│   ├── migrations.png
│   ├── hosting-dashboard.png
│   ├── database-overview.png
│   ├── remote-tables.png
│   ├── live-swagger.png
│   ├── live-get-request.png
│   ├── live-post-request.png
│   └── deployment-video.mp4
│
└── postman/
    └── TrainingCenterApi.postman_collection.json
```

---

# Step 1 - Verify Local Application

Before deployment, the API was fully tested locally.

Completed tasks:

- EF Core migrations created.
- Local SQL Server database created.
- Tables generated successfully.
- Swagger tested.
- Postman requests tested.
- CRUD operations verified.

### Evidence

- Local Swagger screenshot
- SQL Server database screenshot
- Migration folder screenshot

---

# Step 2 - Create Hosting Website

A new website was created using **RunASP.NET**.

Website Information

| Item | Value |
|------|-------|
| Hosting Provider | RunASP.NET |
| Runtime | ASP.NET Core |
| HTTPS | Enabled |
| Hosting Plan | Free |
| Deployment Method | Web Deploy |

### Evidence

- Hosting dashboard
- Website overview
- HTTPS URL

---

# Step 3 - Create Remote SQL Server Database

A remote Microsoft SQL Server database was created on RunASP.

Database configuration included:

- Database creation
- Database user
- Secure password
- Connection string generation

The production database is completely separate from the local SQL Server instance.

### Evidence

- Database overview
- Database settings
- Connection string (password hidden)

---

# Step 4 - Configure Production Connection

The application was configured to connect to the remote SQL Server database.

The production connection string contains:

- Server
- Database
- User ID
- Password
- Encryption settings

Passwords are **not stored in GitHub**.

Production configuration is excluded from version control.

---

# Step 5 - Publish the API

The API was deployed using **Visual Studio Web Deploy**.

Deployment steps:

1. Enable Web Deploy in RunASP.
2. Download the Publish Profile.
3. Import the Publish Profile into Visual Studio.
4. Build the project.
5. Publish the application.
6. Verify successful deployment.

Deployment completed successfully without errors.

---

# Step 6 - Apply EF Core Migrations

Existing EF Core migrations were applied to the remote SQL Server database.

Database tables created include:

- Students
- Instructors
- TrainingTracks
- Enrollments
- Payments
- PaymentSummaries
- __EFMigrationsHistory

The database schema matches the local development database.

---

# Step 7 - Verify Live API

After deployment the API was tested online.

Tests performed:

- Swagger loads successfully.
- GET endpoints return data.
- POST endpoints insert data.
- Database updates correctly.
- Remote SQL Server stores records.

---

# Live API

## Swagger

```
http://trainingcenterapihosting.runasp.net/swagger
```

---

## Base URL

```
http://trainingcenterapihosting.runasp.net
```

---

# Security

Production credentials are protected using the following approach:

- Local connection string stored in `appsettings.json`
- Production connection stored separately
- Production configuration excluded from Git
- Passwords never committed to GitHub
- Sensitive information hidden in screenshots

---

# Deployment Verification

The following checks were completed successfully.

| Check | Status |
|--------|--------|
| Local API Tested | ✅ |
| Local Database Working | ✅ |
| EF Core Migrations Created | ✅ |
| Remote Website Created | ✅ |
| Remote SQL Database Created | ✅ |
| API Published | ✅ |
| Live Swagger Working | ✅ |
| GET Endpoint Tested | ✅ |
| POST Endpoint Tested | ✅ |
| Remote Database Updated | ✅ |

---

# Evidence

The `evidence` folder contains:

- Local Swagger
- Local Database
- Migration Folder
- Hosting Dashboard
- Remote Database
- Remote Tables
- Live Swagger
- Live GET Request
- Live POST Request
- Deployment Video

---

# Postman Testing

The project includes a Postman collection containing examples for:

- GET requests
- POST requests
- PUT requests
- DELETE requests

The collection verifies both successful and failure scenarios using the deployed API.

---

# Learning Outcomes

This task demonstrates the ability to:

- Deploy an ASP.NET Core Web API
- Configure production hosting
- Connect to a remote SQL Server database
- Publish applications using Web Deploy
- Apply Entity Framework Core migrations
- Secure production configuration
- Validate production APIs using Swagger and Postman

---

# Result

The Training Center Registration API has been successfully deployed to a live hosting environment.

The production API is fully operational, connected to a remote SQL Server database, documented with Swagger, and tested using Postman. All required deployment, database, and verification steps for Task 06 have been completed successfully.