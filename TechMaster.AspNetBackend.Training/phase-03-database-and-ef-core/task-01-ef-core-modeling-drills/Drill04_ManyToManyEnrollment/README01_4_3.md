# Drill 04 – Many-to-Many Relationship via Join Entity

## Objective

Learn how to model a many-to-many relationship in Entity Framework Core using an explicit join entity when the relationship contains additional business data.

## Concept

A **Student** can enroll in many **TrainingTracks**, and a **TrainingTrack** can have many **Students**.

Since the relationship stores extra information (such as enrollment status and grade), an **Enrollment** entity is used instead of EF Core's automatic many-to-many mapping.

## Relationship

```
Student (1)
     |
     | *
Enrollment
     * |
       |
TrainingTrack (1)
```

## Entities

### Student

- Id
- FullName
- Email

### TrainingTrack

- Id
- Name
- DurationInMonths

### Enrollment

- Id
- StudentId (Foreign Key)
- TrainingTrackId (Foreign Key)
- Status
- EnrollmentDate
- FinalGrade (Optional)

## EF Core Configuration

```csharp
modelBuilder.Entity<Enrollment>()
    .HasOne(e => e.Student)
    .WithMany(s => s.Enrollments)
    .HasForeignKey(e => e.StudentId);

modelBuilder.Entity<Enrollment>()
    .HasOne(e => e.TrainingTrack)
    .WithMany(t => t.Enrollments)
    .HasForeignKey(e => e.TrainingTrackId);
```

## Business Rule

A student cannot have more than one **active enrollment** in the same training track.

The API checks for an existing active enrollment before creating a new one.

## API Endpoints

### Create Enrollment

```
POST /api/enrollments
```

Creates a new enrollment between a student and a training track.

---

### Get Student Enrollments

```
GET /api/students/{id}
```

Returns the student with all enrolled training tracks using `Include()` and `ThenInclude()`.

---

### Get Track Students

```
GET /api/trainingtracks/{id}
```

Returns the training track with all enrolled students.

## What I Learned

- How to implement a many-to-many relationship using a join entity.
- Why automatic many-to-many should not be used when additional business data is required.
- How to configure relationships using the Fluent API.
- How to query nested related data using `Include()` and `ThenInclude()`.
- How to implement simple business validation to prevent duplicate active enrollments.

## Evidence

- Screenshot of the `AddEnrollments` migration.
- Screenshot of the `Enrollments` table in SQL Server.
- Screenshot showing the `StudentId` and `TrainingTrackId` foreign keys.
- Screenshot of `GET /api/students/{id}` response.
- Screenshot of `GET /api/trainingtracks/{id}` response.
- Screenshot showing duplicate active enrollment validation returning `400 Bad Request`.

## Result

Successfully implemented a production-style many-to-many relationship using an explicit join entity with additional business data, proper foreign key relationships, and API endpoints to retrieve related entities.