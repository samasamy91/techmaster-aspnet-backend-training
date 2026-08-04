# Drill 06 – Seed Data

## Objective

Learn how to populate the database with initial sample data using Entity Framework Core so the API is ready for testing immediately after applying migrations.

## Concept

Seed data provides realistic records that allow reviewers to test API endpoints without manually creating data.

This drill uses EF Core's `HasData()` method, which inserts data during database migrations and prevents duplicate records.

## Seeded Data

### Students

| ID | Name |
|----|------|
|1|Ahmed Ali|
|2|Sara Mohamed|
|3|Omar Hassan|
|4|Mona Ibrahim|
|5|Youssef Mahmoud|

### Instructors

| ID | Name |
|----|------|
|1|Mohamed Hassan|
|2|Nour Ahmed|

### Training Tracks

| ID | Name | Instructor ID |
|----|------|---------------|
|1|ASP.NET Core|1|
|2|Entity Framework Core|1|
|3|SQL Server|2|

### Enrollments

| ID | Student ID | Track ID | Status |
|----|------------|----------|--------|
|1|1|1|Active|
|2|2|1|Active|
|3|3|2|Completed|
|4|4|3|Active|
|5|5|2|Pending|

## Sample IDs

Use the following IDs when testing the API:

- Student IDs: **1–5**
- Instructor IDs: **1–2**
- Training Track IDs: **1–3**
- Enrollment IDs: **1–5**

## What I Learned

- How to seed data using `HasData()`.
- Why seed data should be deterministic and repeatable.
- How EF Core migrations manage seeded data.
- How to prepare a database for immediate testing.

## Evidence

- Screenshot of the `SeedInitialData` migration.
- Screenshot of the `Students`, `Instructors`, `TrainingTracks`, and `Enrollments` tables.
- Screenshot of Swagger showing seeded records returned from API endpoints.

## Result

Successfully seeded the database with realistic sample data. The data is inserted only once through EF Core migrations, allowing reviewers to test the application immediately without creating records manually.