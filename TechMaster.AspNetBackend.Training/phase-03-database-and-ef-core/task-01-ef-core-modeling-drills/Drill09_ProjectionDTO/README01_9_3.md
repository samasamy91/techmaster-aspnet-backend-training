# Drill 09 – Projection DTO

## Objective

Learn how to return API responses using Data Transfer Objects (DTOs) instead of exposing Entity Framework entities directly.

## Concept

Projection uses LINQ `Select()` to shape API responses so that only the required data is returned to clients.

This improves performance, reduces payload size, and prevents exposing internal entity properties or navigation graphs.

## DTOs

### StudentListItemDto

- Id
- Name
- Email

### TrackDetailsDto

- Id
- Name
- DurationInMonths
- InstructorName
- Students

## Implementation

Student list response:

```csharp
context.Students
    .Select(s => new StudentListItemDto
    {
        Id = s.Id,
        Name = s.Name,
        Email = s.Email
    });
```

Track details response:

```csharp
context.TrainingTracks
    .Select(t => new TrackDetailsDto
    {
        Id = t.Id,
        Name = t.Name,
        DurationInMonths = t.DurationInMonths,
        InstructorName = t.Instructor.Name,
        Students = t.Enrollments
            .Select(e => e.Student.Name)
            .ToList()
    });
```

## Benefits

- Prevents exposing EF entities.
- Avoids circular reference issues.
- Returns only the required fields.
- Reduces response size.
- Improves query performance by selecting only needed columns.

## What I Learned

- How to create DTOs for API responses.
- How to use LINQ `Select()` for projection.
- Why DTOs are preferred over returning EF entities.
- How projection can replace `Include()` in many scenarios.

## Evidence

- Screenshot of `GET /api/students`.
- Screenshot of `GET /api/trainingtracks/{id}`.
- Screenshot of the DTO classes.
- Screenshot of the projection query using `Select()`.

## Result

Successfully implemented DTO-based API responses using LINQ projection. The API now returns only the data required by each endpoint without exposing Entity Framework entities or unnecessary navigation properties.