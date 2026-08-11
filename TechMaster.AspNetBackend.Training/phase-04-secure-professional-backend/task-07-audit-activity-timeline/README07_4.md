# Task 07 — Audit Trail & Activity Timeline

## Overview

Implemented an audit trail system for the Training Center API to track important user and system actions.

The audit system records **who performed an action, what was affected, and when it happened**.

## Implemented Features

* Created `ActivityLog` entity.
* Added activity logging service and interface.
* Added admin-only activity log endpoint.
* Added pagination using the existing `PagedRequest` and `PagedResult<T>`.
* Added filtering by:

  * User ID
  * Entity name
  * Date range
* Added logging for important actions:

  * User registered
  * User logged in
  * Track created
  * Enrollment requested
  * Enrollment status updated
  * Payment created
  * Payment status updated

## ActivityLog Data

The log contains:

```text
Id
UserId
UserRole
Action
EntityName
EntityId
Description
CreatedAt
IpAddress
Metadata
```

## API Endpoint

### Get Activity Logs

```http
GET /api/admin/activity-logs
```

Admin only.

### Filter by User

```http
GET /api/admin/activity-logs?userId=1
```

### Filter by Entity

```http
GET /api/admin/activity-logs?entityName=Payment
```

### Filter by Date

```http
GET /api/admin/activity-logs?from=2026-08-01&to=2026-08-11
```

### Pagination

```http
GET /api/admin/activity-logs?pageNumber=1&pageSize=10
```

## Authorization

The endpoint is protected using:

```csharp
[Authorize(Roles = "Admin")]
```

Therefore:

* Admin → Can view activity logs.
* Student → Receives `403 Forbidden`.
* Unauthenticated user → Receives `401 Unauthorized`.

## Pagination

The existing common pagination classes are reused:

```text
PagedRequest
PagedResult<T>
```

`PagedRequest` handles `PageNumber` and `PageSize`, while `PagedResult<T>` returns the logs together with pagination information.

## Example Response

```json
{
  "success": true,
  "message": "Retrieved successfully",
  "data": {
    "items": [],
    "pageNumber": 1,
    "pageSize": 10,
    "totalRecords": 0,
    "totalPages": 0
  },
  "errors": []
}
```

## Database

Activity records are stored in the:

```text
ActivityLogs
```

table in the SQL Server database.

## Verification

The feature was tested using Postman with an Admin Bearer token.

Verified:

* Activity logs endpoint works.
* Admin authorization works.
* User filtering works.
* Entity filtering works.
* Date filtering works.
* Pagination is supported.
* Important application actions create audit records.

## Security

Activity logs do not store user passwords, JWT signing keys, or other sensitive authentication secrets.
