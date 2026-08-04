# Drill 08 – Audit Fields

## Objective

Learn how to automatically track when records are created and updated using Entity Framework Core.

## Concept

Audit fields help monitor data changes without requiring users to provide timestamps manually.

Each audited entity contains:

- CreatedAt
- UpdatedAt

The application automatically sets these values using `DateTime.UtcNow`.

## Audit Fields

### CreatedAt

- Set automatically when a new entity is created.
- Never changed afterward.

### UpdatedAt

- Updated automatically whenever an existing entity is modified.
- Remains `null` until the first update.

## Implementation

A shared `BaseEntity` class stores the audit fields.

`SaveChanges()` and `SaveChangesAsync()` are overridden in `AppDbContext` to automatically assign audit values based on the entity state.

```csharp
if (entry.State == EntityState.Added)
{
    entry.Entity.CreatedAt = DateTime.UtcNow;
}

if (entry.State == EntityState.Modified)
{
    entry.Entity.UpdatedAt = DateTime.UtcNow;
}
```

## Why UTC?

Using UTC ensures timestamps remain consistent regardless of server location or client time zone.

## What I Learned

- How to implement automatic audit fields.
- How to override `SaveChanges()` and `SaveChangesAsync()`.
- Why UTC time should be used in backend applications.
- How to avoid asking API clients to send audit information.

## Evidence

- Screenshot of the `AddAuditFields` migration.
- Screenshot of the database showing `CreatedAt` and `UpdatedAt`.
- Screenshot of a newly created record with `CreatedAt` populated.
- Screenshot of an updated record with `UpdatedAt` changed.
- Swagger response demonstrating the audit fields.

## Result

Successfully implemented automatic audit fields for key entities. The application records creation and update timestamps using UTC without requiring manual input from API clients.