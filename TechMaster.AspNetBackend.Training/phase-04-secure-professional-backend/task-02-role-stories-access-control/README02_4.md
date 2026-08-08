# Task 02 - Role Stories & Access Control

## TechMaster Academy - ASP.NET Backend Career Training

**Phase:** 04 - Authentication & Authorization  
**Task:** 02 - Role Stories & Access Control  
**Technology:** ASP.NET Core Web API  
**Authentication:** Custom JWT Authentication  
**Authorization:** Role-Based Authorization + Ownership Validation  
**Database:** SQL Server + Entity Framework Core

---

# 1. Task Overview

This task upgrades the Training Center API from basic JWT authentication into a role-based platform.

The system contains three main roles:

- Admin
- Instructor
- Student

Each role has different permissions and access boundaries.

The goal is to ensure that:

- Anonymous users cannot access protected endpoints.
- Admins can perform administrative operations.
- Instructors can access only their assigned tracks and related students.
- Students can access only their own data.
- Users cannot access another user's private information.
- Payment administration is restricted to Admins.
- Administrative reports are restricted to Admins.
- Instructor access is protected by ownership checks.
- Student access is based on the authenticated user's identity.
- Incorrect roles receive `403 Forbidden`.
- Missing or invalid authentication receives `401 Unauthorized`.

---

# 2. Roles

## 2.1 Admin

The Admin is responsible for managing the entire Training Center platform.

Admin permissions:

- Manage students.
- Manage instructors.
- Manage training tracks.
- View all enrollments.
- Update enrollment status.
- Manage payments.
- Update payment status.
- Access all administrative reports.
- Access revenue reports.
- Perform administrative operations.

---

## 2.2 Instructor

The Instructor is responsible for their assigned training tracks.

Instructor permissions:

- View own assigned tracks.
- View students in own tracks.
- View enrollments in own tracks.
- Access own instructor information.
- View track-level information.

Instructor restrictions:

- Cannot manage students globally.
- Cannot create instructors.
- Cannot manage tracks as an Admin.
- Cannot access payment administration.
- Cannot update payment status.
- Cannot access revenue reports.
- Cannot access another instructor's tracks.
- Cannot access another student's private profile.

---

## 2.3 Student

The Student can access their own information and available training tracks.

Student permissions:

- View own profile.
- View own enrollments.
- View own payment history.
- View available training tracks.

Student restrictions:

- Cannot view other students.
- Cannot manage students.
- Cannot manage instructors.
- Cannot manage tracks.
- Cannot access administrative reports.
- Cannot access revenue reports.
- Cannot update payment status.
- Cannot access another student's private information.

---

# 3. Authorization Architecture

The application uses two authorization mechanisms.

## 3.1 Role-Based Authorization

ASP.NET Core `[Authorize]` attributes restrict endpoints according to the user's role.

Example:

```csharp
[Authorize(Roles = "Admin")]

Students Matrix
| Operation          | Admin |     Instructor | Student |
| ------------------ | ----: | -------------: | ------: |
| View all students  |   Yes |             No |      No |
| View student by ID |   Yes | Own-track only |      No |
| Create student     |   Yes |             No |      No |
| Update student     |   Yes |             No |      No |
| Delete student     |   Yes |             No |      No |
| View own profile   |   Yes |             No |     Yes |

Instructors 
| Operation            | Admin |          Instructor | Student |
| -------------------- | ----: | ------------------: | ------: |
| View all instructors |   Yes |                  No |      No |
| View instructor      |   Yes |         Own profile |      No |
| Create instructor    |   Yes |                  No |      No |
| Update instructor    |   Yes | Own profile/limited |      No |
| View assigned tracks |   Yes |          Own tracks |      No |

Tracks
| Operation             | Admin |  Instructor | Student |
| --------------------- | ----: | ----------: | ------: |
| View all tracks       |   Yes |     Limited |      No |
| View assigned tracks  |   Yes |  Own tracks |      No |
| Create track          |   Yes |          No |      No |
| Update track          |   Yes | Own/limited |      No |
| Delete track          |   Yes |          No |      No |
| View available tracks |   Yes |         Yes |     Yes |

Enrollments
| Operation                  | Admin |     Instructor |             Student |
| -------------------------- | ----: | -------------: | ------------------: |
| View all enrollments       |   Yes |             No |                  No |
| View enrollment by ID      |   Yes | Own track only | Own enrollment only |
| Create enrollment          |   Yes |             No |                  No |
| Update enrollment status   |   Yes |             No |                  No |
| View own enrollments       |   Yes |             No |                 Yes |
| View own track enrollments |   Yes |            Yes |                  No |

Payments
| Operation                       | Admin | Instructor | Student |
| ------------------------------- | ----: | ---------: | ------: |
| View all payments               |   Yes |         No |      No |
| Create payment                  |   Yes |         No |      No |
| Update payment status           |   Yes |         No |      No |
| View own payment history        |   Yes |         No |     Yes |
| View another student's payments |   Yes |         No |      No |

Reports
| Operation            |  Admin | Instructor | Student |
| -------------------- | -----: | ---------: | ------: |
| View audit logs      |    Yes |         No |      No |
| Create audit records | System |     System |  System |
| Update audit logs    |     No |         No |      No |

Endpoints Authorization Matrix
| Endpoint                                    | Admin |      Instructor      |  Student |
| ------------------------------------------- | :---: | :------------------: | :------: |
| `GET /api/students`                         |  200  |          403         |    403   |
| `GET /api/students/{id}`                    |  200  | 403 / own-track only |    403   |
| `POST /api/students`                        |  200  |          403         |    403   |
| `PUT /api/students/{id}`                    |  200  |          403         |    403   |
| `DELETE /api/students/{id}`                 |  200  |          403         |    403   |
| `GET /api/students/my-profile`              |  403  |          403         |    200   |
| `GET /api/instructors`                      |  200  |          403         |    403   |
| `GET /api/instructors/{id}`                 |  200  |      Own profile     |    403   |
| `POST /api/instructors`                     |  200  |          403         |    403   |
| `PUT /api/instructors/{id}`                 |  200  |  Own profile/limited |    403   |
| `GET /api/tracks`                           |  200  |        Limited       |    403   |
| `GET /api/tracks/{id}`                      |  200  |      Own/allowed     |    403   |
| `POST /api/tracks`                          |  200  |          403         |    403   |
| `PUT /api/tracks/{id}`                      |  200  |        Limited       |    403   |
| `DELETE /api/tracks/{id}`                   |  200  |          403         |    403   |
| `GET /api/tracks/available`                 |  200  |          200         |    200   |
| `GET /api/tracks/my-track`                  |  403  |          200         |    403   |
| `GET /api/tracks/{id}/students`             |  200  |    Own track only    |    403   |
| `GET /api/enrollments`                      |  200  |          403         |    403   |
| `GET /api/enrollments/{id}`                 |  200  |    Own track only    | Own only |
| `POST /api/enrollments`                     |  200  |          403         |    403   |
| `PUT /api/enrollments/{id}/status`          |  200  |          403         |    403   |
| `GET /api/enrollments/my-enrollments`       |  403  |          403         |    200   |
| `GET /api/enrollments/my-track-enrollments` |  403  |          200         |    403   |
| `GET /api/payments`                         |  200  |          403         |    403   |
| `POST /api/payments`                        |  200  |          403         |    403   |
| `PUT /api/payments/{id}/status`             |  200  |          403         |    403   |
| `GET /api/payments/my-payments`             |  403  |          403         |    200   |
| `GET /api/reports/dashboard-summary`        |  200  |          403         |    403   |
| `GET /api/reports/unpaid-enrollments`       |  200  |          403         |    403   |
| `GET /api/reports/track-capacity`           |  200  |          403         |    403   |
| `GET /api/reports/revenue-summary`          |  200  |          403         |    403   |
| `GET /api/reports/revenue-by-track`         |  200  |          403         |    403   |
