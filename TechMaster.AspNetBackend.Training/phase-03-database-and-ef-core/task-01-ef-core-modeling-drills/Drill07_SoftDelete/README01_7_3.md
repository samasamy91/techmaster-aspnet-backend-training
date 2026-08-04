# Drill 07 – Soft Delete

## Objective

Learn how to implement soft delete in Entity Framework Core by marking records as deleted instead of permanently removing them from the database.

## Concept

Soft delete allows applications to preserve historical data while hiding deleted records from normal API queries.

Instead of deleting a row, the application updates two fields:

- `IsDeleted`
- `DeletedAt`

A global query filter ensures deleted records are automatically excluded from standard queries.

## Entity

### Student

Added fields:

- IsDeleted
- DeletedAt

## EF Core Configuration

```csharp
modelBuilder.Entity<Student>()
    .HasQueryFilter(s => !s.IsDeleted);
```

## API Endpoints

### Delete Student

```
DELETE /api/students/{id}
```

Marks the student as deleted by setting:

- `IsDeleted = true`
- `DeletedAt = DateTime.UtcNow`

The row remains in the database.

---

### Get Active Students

```
GET /api/students
```

Returns only students where `IsDeleted` is `false`.

---

### Get All Students

```
GET /api/students/all
```

Uses `IgnoreQueryFilters()` to return both active and deleted students.

## What I Learned

- How to implement soft delete using `IsDeleted` and `DeletedAt`.
- How global query filters automatically hide deleted records.
- How to use `IgnoreQueryFilters()` for administrative queries.
- Why soft delete is preferred over permanent deletion in many production systems.

## Evidence

- Screenshot of the `AddSoftDeleteFields` migration.
- Screenshot of the `Students` table before deletion.
- Screenshot of the `Students` table after soft deletion.
- Screenshot of `GET /api/students` showing deleted students are excluded.
- Screenshot of `GET /api/students/all` showing deleted students are still stored.

## Result

Successfully implemented a soft delete mechanism where deleted students remain in the database but are excluded from normal API queries through a global query filter.