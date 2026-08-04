use TrainingCenterDB;

INSERT INTO Students (FullName, Email, PhoneNumber, CreatedAt)
VALUES
('Sama Samy','sama@test.com','0100000001','2026-01-10'),
('Sara Mohamed','sara@test.com','0100000002','2026-01-15'),
('Omar Hassan','omar@test.com','0100000003','2026-02-01'),
('Mohamed Magdy','mohamed@test.com','0100000004','2026-02-10'),
('Zeina Ahmed','zeina@test.com','0100000005','2026-03-01'),
('Sedra Ahmed','tota@test.com','0100000005','2026-03-01'),
('Malek Ahmed','malek@test.com','0100000005','2026-03-01');

INSERT INTO Instructors (FullName, Email, Specialization)
VALUES
('Rowida Gamal', 'rowida@training.com', 'ASP.NET Core'),
('Nour Emad', 'nour@training.com', 'Java Spring Boot'),
('Hady Mahmoud', 'hady@training.com', 'Data Science');

INSERT INTO Tracks (Title, Description, DurationWeeks, StartDate, InstructorId)
VALUES
('ASP.NET Backend','Backend development using ASP.NET Core',12,'2026-07-01',1),
('Java Spring Boot','Microservices with Spring Boot',10,'2026-08-01',2),
('Data Science','Python and Machine Learning',14,'2026-09-01',3),
('SQL Server','Database Design and SQL',6,'2026-07-15',1),
('Docker & DevOps','Containers and CI/CD',8,'2026-10-01',2);

INSERT INTO Registrations (StudentId, TrackId, RegistrationDate, Status)
VALUES
(1,1,'2026-06-20','Completed'),
(2,1,'2026-06-22','Completed'),
(3,2,'2026-07-20','Pending'),
(4,3,'2026-08-15','Completed'),
(5,4,'2026-07-01','Completed'),
(1,5,'2026-09-20','Pending'),
(2,3,'2026-08-20','Completed'),
(3,4,'2026-06-30','Completed');

INSERT INTO Payments (RegistrationId, Amount, PaymentDate, PaymentStatus)
VALUES
(1,6000,'2026-06-20','Paid'),
(2,6000,'2026-06-22','Paid'),
(3,5500,'2026-07-20','Unpaid'),
(4,7000,'2026-08-15','Paid'),
(5,3000,'2026-07-01','Paid'),
(6,4500,'2026-09-20','Unpaid'),
(7,7000,'2026-08-20','Paid'),
(8,3000,'2026-06-30','Paid');

