# Task 06 — Production Redeployment


## 📌 Task Overview

Task 06 focuses on **redeploying the upgraded secure ASP.NET Core API to a real hosting environment** and proving that the deployed application works with a **remote SQL Server database**.

The API was redeployed using **MonsterASP.NET** hosting with a remote Microsoft SQL Server database.

The deployment verifies:

* Production API hosting
* Remote SQL Server connectivity
* EF Core migrations
* Production database schema
* Swagger availability
* User registration
* User login
* JWT authentication
* Protected endpoints
* Role-based authorization
* `401 Unauthorized` behavior
* `403 Forbidden` behavior
* Production configuration
* Secure handling of secrets

---

# 1. Technologies Used

| Technology               | Purpose                        |
| ------------------------ | ------------------------------ |
| ASP.NET Core Web API     | Backend API                    |
| Entity Framework Core    | Database access and migrations |
| SQL Server               | Remote production database     |
| MonsterASP.NET           | API hosting                    |
| JWT                      | Authentication                 |
| Role-Based Authorization | Access control                 |
| Swagger / OpenAPI        | API testing                    |
| Postman                  | API verification               |
| Visual Studio            | Development and publishing     |
| WebMSSQL                 | Remote database management     |

---

# 2. Production Architecture

The deployed system follows this architecture:

```text
                         Internet
                            │
                            ▼
              ┌─────────────────────────┐
              │     MonsterASP.NET      │
              │                         │
              │ ASP.NET Core Web API    │
              │                         │
              │ Controllers             │
              │ Services                │
              │ DTOs                    │
              │ EF Core                 │
              │ JWT Authentication      │
              │ Authorization           │
              └────────────┬────────────┘
                           │
                           │ SQL Server Connection
                           ▼
              ┌─────────────────────────┐
              │   MonsterASP SQL Server │
              │                         │
              │ Students                │
              │ Instructors             │
              │ TrainingTracks           │
              │ Enrollments              │
              │ Payments                │
              │ EF Migrations History   │
              └─────────────────────────┘
```

---

# 3. Hosting Information

The API was redeployed to:

```text
MonsterASP.NET
```

### Live Website

```text
https://trainingcenterapihosting.runasp.net
```

### Live Swagger

```text
https://trainingcenterapihosting.runasp.net/swagger/index.html
```

> Replace the URL above if the hosting URL changes.

---

# 4. Remote Database

The application uses a **remote SQL Server database hosted by MonsterASP.NET** instead of the local development database.

The production database contains the application's required tables, including:

```text
Students
Instructors
TrainingTracks
Enrollments
Payments
Users
TrackSessions
__EFMigrationsHistory
```

The database was verified through the MonsterASP.NET WebMSSQL interface.

---

# 5. Production Connection Configuration

The application uses the named connection string:

```text
HostDefaultConnection
```

The application retrieves it through ASP.NET Core configuration:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HostDefaultConnection")
    )
);
```

This allows the same application code to work with different databases depending on the environment.

---

# 6. Production Database Configuration

The production database connection contains the required SQL Server information:

```text
Server
Database
User Id
Password
Encrypt
TrustServerCertificate
MultipleActiveResultSets
```


---

# 7. Security of Production Secrets

Production secrets are not committed to the public repository.

The following information is intentionally excluded:

* Database password
* JWT signing key
* Production connection string containing credentials
* Authentication secrets
* Other sensitive hosting credentials

Production configuration should be supplied through hosting/environment configuration whenever possible.


# 8. EF Core Production Migration

Before deployment, the database schema was synchronized with the application's EF Core model.

The migration process was performed against the remote database.

Example command:

```powershell
Update-Database
```

The remote database was verified after the migration.

---

# 9. Migration History Issue and Resolution

During the redeployment process, the remote database already contained application tables such as:

```text
Instructors
Students
TrainingTracks
Enrollments
Payments
```

However, EF Core attempted to execute an older migration that tried to recreate existing tables.

This resulted in:

```text
There is already an object named 'Instructors' in the database.
```

The issue occurred because the existing database schema and EF Core migration history were not synchronized.

The remote database migration history was checked using:

```sql
SELECT *
FROM __EFMigrationsHistory
ORDER BY MigrationId;
```

The database schema was then synchronized using the correct EF Core migrations.

---

# 10. Application Startup Configuration

The deployed application does not rely on running the entire migration history every time the application starts.

Database migrations are performed as part of the deployment/database preparation process.

The application startup is responsible for configuring services and starting the API.

The production application uses:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HostDefaultConnection")
    )
);
```

---

# 11. Database Seeding

The application also performs the required seed operation during startup.

The seeding logic is called through:

```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await DatabaseSeeder.SeedAdmin(context);
}
```

The seeder is intended to create required initial data without duplicating existing records.

---

# 12. Decimal Precision Configuration

During deployment, EF Core reported a warning for:

```text
Enrollment.ProgressPercentage
```

The property was explicitly configured with precision and scale:

```csharp
modelBuilder.Entity<Enrollment>()
    .Property(e => e.ProgressPercentage)
    .HasPrecision(5, 2);
```

This prevents unexpected decimal truncation in SQL Server.

The configuration supports values such as:

```text
0
25.50
50.00
75.25
100.00
```

---

# 13. Publishing Process

The updated application was published from Visual Studio.

### Steps

1. Build the project.
2. Verify the project builds successfully.
3. Open the existing MonsterASP.NET publishing profile.
4. Select the existing hosted application.
5. Publish the updated application.
6. Wait for deployment to finish.
7. Restart the application if required.
8. Open the live Swagger URL.
9. Verify the API starts successfully.

---

# 14. Live Swagger Verification

The deployed Swagger page was used to verify that the API is publicly accessible.

### Swagger URL

```text
https://trainingcenterapihosting.runasp.net/swagger/index.html
```

Swagger allows the deployed endpoints to be tested without depending on the local development machine.

### Evidence

📸 **Screenshot: Live Swagger page**

Show:

* Browser URL
* Swagger UI
* Available API endpoints

---

# 15. Remote Database Verification

The remote MonsterASP.NET database was opened through WebMSSQL.

The database tables were verified to ensure that the production API is connected to the correct remote database.

Expected tables include:

```text
Students
Instructors
TrainingTracks
Enrollments
Payments
```

### Evidence

📸 **Screenshot: MonsterASP.NET database tables**

The screenshot should show the table names and database interface.

Do not show:

* Database password
* Connection string containing the password
* Hosting credentials

---

# 16. Registration Test

A new user was tested against the live API.

Example:

```http
POST /api/Auth/register
```

The request should be sent to the **live MonsterASP.NET API**, not localhost.

### Expected result

```text
200 OK
```

or the appropriate successful registration status used by the API.

### Evidence

📸 **Screenshot: Live registration request**

The screenshot should clearly show that the request was sent to the production URL.

---

# 17. Login Test

The live login endpoint was tested.

Example:

```http
POST /api/Auth/login
```

The request was sent to the hosted API.

### Expected response

A successful login returns a JWT token.

Example:

```json
{
  "token": "eyJ..."
}
```

The actual token can be hidden or partially masked in documentation screenshots.

### Evidence

📸 **Screenshot: Live login returning JWT**

The screenshot should demonstrate:

```text
Live URL
       ↓
Login
       ↓
Successful response
       ↓
JWT token
```

---

# 18. Protected Endpoint Without Token

A protected endpoint was tested without authentication.

Example:

```http
GET /api/ProtectedEndpoint
```

without:

```http
Authorization: Bearer <token>
```

### Expected result

```text
401 Unauthorized
```

This proves that the endpoint is protected by authentication.

### Evidence

📸 **Screenshot: Protected endpoint rejected without token**

---

# 19. Protected Endpoint With JWT

The JWT returned from login was used to access a protected endpoint.

Swagger authorization was configured using:

```text
Bearer <JWT_TOKEN>
```

The protected endpoint was then called.

### Expected result

```text
200 OK
```

with real data retrieved from the remote database.

This proves:

```text
Login
  ↓
JWT generated
  ↓
Bearer token
  ↓
Authentication
  ↓
Authorization
  ↓
Protected endpoint
  ↓
Remote database
  ↓
Data returned
```

### Evidence

📸 **Screenshot: Protected endpoint with Bearer token**

---

# 20. Role-Based Authorization Test

The API's role authorization was also tested.

A user with a lower-privileged role was used to access an endpoint restricted to another role.

For example:

```text
Student
   ↓
Admin Report
```

The request should be rejected.

### Expected response

```text
403 Forbidden
```

This demonstrates that authentication and authorization are working separately.

---

# 21. Authentication vs Authorization

The deployment verifies both concepts:

### Authentication

Answers:

> Who are you?

A valid JWT proves the user's identity.

Without a token:

```text
401 Unauthorized
```

---

### Authorization

Answers:

> Are you allowed to perform this action?

A valid token with the wrong role results in:

```text
403 Forbidden
```

Example:

```text
Student JWT
     ↓
Admin-only endpoint
     ↓
403 Forbidden
```

---

# 22. Production Verification Flow

The complete verification flow is:

```text
1. Open Live Swagger
          ↓
2. Register user
          ↓
3. Login
          ↓
4. Receive JWT
          ↓
5. Call protected endpoint without JWT
          ↓
6. Receive 401
          ↓
7. Add Bearer JWT
          ↓
8. Call protected endpoint
          ↓
9. Receive 200 + database data
          ↓
10. Use wrong role
          ↓
11. Receive 403
```

---

---

# 23. Safe Configuration Example

A safe example can be documented as:

```json
{
  "ConnectionStrings": {
    "HostDefaultConnection": "Server=<REMOTE_SERVER>;Database=<DATABASE>;User Id=<USER>;Password=<SECRET>;"
  }
}
```

This demonstrates the configuration structure without exposing real credentials.

---

# 24. Local vs Production Environment

The project supports different environments.

### Development

```text
Local ASP.NET Core API
        ↓
Local SQL Server
```

### Production

```text
MonsterASP.NET
        ↓
Remote SQL Server
```

The production API must not depend on the developer's local SQL Server.

---

# 25. Production Deployment Result

The final deployment demonstrates that the API can operate independently from the development machine.

The deployed API:

* Runs on MonsterASP.NET
* Uses a remote SQL Server database
* Exposes Swagger publicly
* Supports authentication
* Generates JWT tokens
* Protects endpoints
* Enforces roles
* Returns `401` when authentication is missing
* Returns `403` when authorization fails
* Retrieves data from the remote database

---


# 26. Deployment Lessons Learned

This deployment demonstrated several important production concepts:

### Remote database connectivity

The API must connect to a database that is not running on the developer's local machine.

### Migration management

EF Core migration history must match the actual production database schema.

### Configuration management

Production credentials should be separated from source code.

### Authentication

JWT authentication must work on the deployed application.

### Authorization

Role-based access rules must continue working after deployment.

### Production verification

An application is not considered successfully deployed simply because it publishes successfully. The live API must also be tested.

---

# 32. Final Result

**Task 06 — Production Redeployment** successfully demonstrates the complete production workflow:

```text
ASP.NET Core Secure API
          │
          ▼
     EF Core Model
          │
          ▼
   Remote SQL Database
          │
          ▼
     EF Migrations
          │
          ▼
    MonsterASP.NET
          │
          ▼
      Live Swagger
          │
          ▼
       Register
          │
          ▼
         Login
          │
          ▼
        JWT Token
          │
          ▼
   Protected Endpoint
          │
          ├──────── No Token ────────► 401
          │
          ├──────── Valid Token ─────► 200
          │
          └──────── Wrong Role ──────► 403
```

The deployment proves that the upgraded secure API is running in a real hosting environment and communicating with a remote SQL Server database rather than relying only on the local development environment.
