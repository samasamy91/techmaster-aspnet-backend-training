# Design Details

## Tables List

The database consists of the following five core tables:

1. Students
2. Instructors
3. TrainingTracks
4. Enrollments
5. Payments

---

# Fields List

## Students

| Field | Type | Description |
|------|------|-------------|
| StudentId | int | Primary Key |
| FullName | nvarchar(100) | Student full name |
| Email | nvarchar(150) | Unique email address |
| PhoneNumber | nvarchar(20) | Optional phone number |
| CreatedAt | datetime | Creation date (UTC) |
| UpdatedAt | datetime | Last update date |
| IsActive | bit | Active status |
| IsDeleted | bit | Soft delete flag |
| DeletedAt | datetime | Soft delete date |

---

## Instructors

| Field | Type | Description |
|------|------|-------------|
| InstructorId | int | Primary Key |
| FullName | nvarchar(100) | Instructor name |
| Email | nvarchar(150) | Unique email |
| Specialization | nvarchar(100) | Teaching specialization |
| Bio | nvarchar(max) | Optional biography |
| IsActive | bit | Active status |
| CreatedAt | datetime | Creation date (UTC) |

---

## TrainingTracks

| Field | Type | Description |
|------|------|-------------|
| TrainingTrackId | int | Primary Key |
| Title | nvarchar(100) | Track title |
| Code | nvarchar(20) | Unique code |
| Description | nvarchar(max) | Track description |
| Level | nvarchar(50) | Beginner / Intermediate / Advanced |
| Capacity | int | Maximum number of students |
| StartDate | datetime | Track start date |
| EndDate | datetime | Track end date |
| Status | nvarchar(30) | Planned / Active / Completed |
| InstructorId | int | Foreign Key |
| CreatedAt | datetime | Creation date |
| IsDeleted | bit | Soft delete flag |

---

## Enrollments

| Field | Type | Description |
|------|------|-------------|
| EnrollmentId | int | Primary Key |
| StudentId | int | Foreign Key |
| TrainingTrackId | int | Foreign Key |
| EnrollmentDate | datetime | Enrollment date |
| Status | nvarchar(30) | Enrollment status |
| ProgressPercentage | decimal(5,2) | Student progress |
| FinalResult | decimal(5,2) | Final grade (optional) |
| CreatedAt | datetime | Creation date |
| UpdatedAt | datetime | Last update date |

---

## Payments

| Field | Type | Description |
|------|------|-------------|
| PaymentId | int | Primary Key |
| EnrollmentId | int | Foreign Key |
| Amount | decimal(18,2) | Payment amount |
| PaymentMethod | nvarchar(50) | Cash / Card / Bank Transfer |
| PaymentDate | datetime | Payment date |
| PaymentStatus | nvarchar(30) | Pending / Paid / Failed |
| ReferenceNumber | nvarchar(100) | Payment reference |
| Notes | nvarchar(max) | Optional notes |

---

# Primary Keys

| Table | Primary Key |
|------|-------------|
| Students | StudentId |
| Instructors | InstructorId |
| TrainingTracks | TrainingTrackId |
| Enrollments | EnrollmentId |
| Payments | PaymentId |

---

# Foreign Keys

| Table | Foreign Key | References |
|------|-------------|------------|
| TrainingTracks | InstructorId | Instructors |
| Enrollments | StudentId | Students |
| Enrollments | TrainingTrackId | TrainingTracks |
| Payments | EnrollmentId | Enrollments |

---

# Relationship Explanation

## Student → Enrollment

- One Student can have many Enrollments.
- Each Enrollment belongs to one Student.

Relationship:

**One-to-Many**

---

## TrainingTrack → Enrollment

- One Training Track can have many Enrollments.
- Each Enrollment belongs to one Training Track.

Relationship:

**One-to-Many**

---

## Instructor → TrainingTrack

- One Instructor can teach many Training Tracks.
- Every Training Track has exactly one Instructor.

Relationship:

**One-to-Many**

---

## Enrollment → Payment

- One Enrollment can have multiple Payments.
- Every Payment belongs to one Enrollment.

Relationship:

**One-to-Many**

---

## Student ↔ TrainingTrack

Students and Training Tracks have a **Many-to-Many** relationship implemented through the **Enrollment** entity.

This allows storing additional business information such as:

- Enrollment Date
- Status
- Progress Percentage
- Final Result

---


# Business Questions & Answers

## 1. Which students are enrolled in a specific track?

Query the **Enrollments** table using the selected `TrainingTrackId` and join it with the **Students** table to retrieve the list of enrolled students.

---

## 2. Which tracks have available seats?

Compare the number of enrollments for each training track with its `Capacity`. Any track where the number of enrolled students is less than the capacity has available seats.

Formula:

Available Seats = Capacity − Current Enrollments

---

## 3. Which enrollments are unpaid?

Retrieve enrollments that have no successful payment records or whose total paid amount is less than the required amount.

These enrollments have a payment status of **Pending** or an outstanding balance.

---

## 4. How much revenue did each track generate?

Calculate the total revenue by summing the `Amount` of all successful payments associated with enrollments for each training track.

Revenue = SUM(Payment.Amount)

---

## 5. Which instructor has the highest workload?

Count the number of training tracks assigned to each instructor or count the total number of enrolled students across those tracks.

The instructor with the highest count has the greatest workload.

---

## 6. Which students have active enrollments?

Return students whose enrollment status is **Active**.

These students are currently participating in one or more training tracks.

---

## 7. Which tracks start this month?

Retrieve all training tracks whose `StartDate` falls within the current month and year.

---

## 8. What is the payment history for an enrollment?

Retrieve all payment records linked to the selected `EnrollmentId`.

The payment history includes:

- Payment Amount
- Payment Method
- Payment Date
- Payment Status
- Reference Number

---

## 9. Which tracks are full?

A training track is considered full when the number of enrolled students equals its capacity.

Condition:

Current Enrollments = Capacity

---

## 10. How many enrollments exist by status?

Group enrollments by the `Status` field and count the number of records in each group.

Example output:

- Active: 42
- Completed: 28
- Cancelled: 7

This report provides an overview of enrollment distribution across different statuses.