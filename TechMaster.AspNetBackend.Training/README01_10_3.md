# Drill 10 – Pagination

## Objective

Learn how to implement server-side pagination using Entity Framework Core to efficiently return large collections of data.

## Concept

Pagination divides data into smaller pages instead of returning all records in a single request. This improves performance, reduces response size, and provides metadata for client applications.

## Query Parameters

| Parameter | Description | Validation |
|-----------|-------------|------------|
| pageNumber | Current page number | Must be greater than 0 |
| pageSize | Number of records per page | Must be between 1 and 50 |

## Pagination Formula

```csharp
int skip = (pageNumber - 1) * pageSize;
```

The API uses:

- `CountAsync()` to determine the total number of records.
- `Skip()` to ignore records from previous pages.
- `Take()` to retrieve only the requested page.
- `Select()` to project results into DTOs.

## Response Structure

```json
{
  "items": [],
  "totalCount": 15,
  "pageNumber": 1,
  "pageSize": 5,
  "totalPages": 3
}
```

## What I Learned

- How to implement server-side pagination.
- How to validate pagination parameters.
- How to calculate the number of records to skip.
- How to return pagination metadata with the results.
- How to combine pagination with DTO projection.

## Evidence

- Screenshot of the `PaginationResult<T>` DTO.
- Screenshot of the paginated endpoint.
- Swagger response for:
  - `GET /api/students?pageNumber=1&pageSize=5`
  - Invalid `pageNumber=0`
  - Invalid `pageSize=100`

## Result

Successfully implemented server-side pagination using `Skip()`, `Take()`, and `CountAsync()`. The endpoint validates query parameters, returns paginated data as DTOs, and includes metadata such as total records and total pages.