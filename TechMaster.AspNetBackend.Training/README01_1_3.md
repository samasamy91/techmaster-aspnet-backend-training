# Drill 01 – DbContext & First Migration

## Objective

Learn how Entity Framework Core creates a SQL Server database from C# classes.

## Concepts Covered

- Entity
- DbContext
- DbSet
- SQL Server Connection
- Migration
- Database Update

## Entity

Student

- Id
- FullName
- Email
- CreatedAt
- IsActive

## Migration

InitialStudentSchema

## Result

EF Core successfully generated the Students table in SQL Server.

## What I Learned

- DbContext represents the application's database session and manages entity tracking and database operations.
- DbSet<Student> represents the Students table and is used to query and save Student entities.
- Migrations capture model changes and translate them into SQL scripts.
- Update-Database applies migrations to create or modify the database schema.

## Evidence

- Screenshot of InitialStudentSchema migration files.
- Screenshot of the Students table in SQL Server Object Explorer.
- Screenshot of Swagger showing the API running successfully.