# Drill 02 – One-to-One Relationship

## Objective

Learn how to model a one-to-one relationship using Entity Framework Core.

## Relationship

Student (1) ------ (1) StudentProfile

Each student has exactly one profile, and each profile belongs to exactly one student.

## Entities

### Student

- Id
- FullName
- Email
- CreatedAt
- IsActive

### StudentProfile

- Id
- NationalId
- Address
- EmergencyPhone
- DateOfBirth
- StudentId (Foreign Key)

## EF Core Configuration

```csharp
modelBuilder.Entity<Student>()
    .HasOne(s => s.Profile)
    .WithOne(p => p.Student)
    .HasForeignKey<StudentProfile>(p => p.StudentId);
```

## What I Learned

- One-to-one relationships require a foreign key that is unique.
- Navigation properties allow traversing between related entities.
- `Include()` eagerly loads related data.
- Fluent API provides explicit configuration for relationships.

## Evidence

- Screenshot of the `StudentProfiles` table showing the `StudentId` foreign key.
- Screenshot of the migration `AddStudentProfile`.
- Screenshot of the API response with `Student` and its `Profile`.