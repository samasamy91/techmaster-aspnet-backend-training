use TrainingCenterDB;

select * from Students;

select * from Tracks;

select S.FullName,T.Title,R.RegistrationDate,R.Status
from Registrations R join Students S 
ON R.StudentId=S.StudentId join Tracks T
ON R.TrackId=T.TrackId;

select T.Title,COUNT(R.StudentId) AS StudentCount
from Tracks T join Registrations R
ON T.TrackId=R.TrackId Group by T.Title;

select S.FullName,T.Title,P.Amount,P.PaymentStatus 
from Payments P join Registrations R 
ON P.RegistrationId=R.RegistrationId join Students S
ON S.StudentId=R.StudentId join Tracks T
ON T.TrackId=R.TrackId where P.PaymentStatus = 'Unpaid';

select * from Tracks T join Instructors I 
ON T.InstructorId=I.InstructorId

select * from Registrations R join Payments P 
ON P.RegistrationId=R.RegistrationId;

Select Title,StartDate,DurationWeeks from Tracks where StartDate>'2026-08-01';

select I.FullName ,COUNT(T.Title) AS TrackCount from Tracks T
join Instructors I ON I.InstructorId=T.InstructorId
Group by I.FullName;

select S.FullName,T.Title,R.RegistrationDate,R.Status,P.PaymentStatus
from Registrations R join Students S
ON R.StudentId=S.StudentId join Tracks T 
ON T.TrackId=R.TrackId join Payments P
ON R.RegistrationId=P.RegistrationId 
where S.StudentId = 1
