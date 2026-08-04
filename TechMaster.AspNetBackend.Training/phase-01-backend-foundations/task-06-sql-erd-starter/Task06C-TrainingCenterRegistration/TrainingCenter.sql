Create database TrainingCenterDB;

use TrainingCenterDB;

create table Students(
	StudentId int Primary Key identity(1,1),
	FullName NVARCHAR(100) not null,
	Email NVARCHAR(60) not null,
	PhoneNumber NVARCHAR(20) ,
	CreatedAt Date not null
);

create table Instructors(
	InstructorId int Primary Key identity(1,1),
	FullName NVARCHAR(100) not null,
	Email NVARCHAR(60) not null,
	Specialization NVARCHAR(100)
);

create table Tracks(
	TrackId int Primary Key identity(1,1),
	Title NVARCHAR(100) not null,
	Description NVARCHAR(300),
	DurationWeeks int not null,
	StartDate Date not null,
	InstructorId int not null,
	Foreign Key (InstructorId) references Instructors(InstructorId)
);

create table Registrations(
	RegistrationId int Primary Key identity(1,1),
	Status NVARCHAR(30) not null,
	RegistrationDate Date not null,
	StudentId int not null,
	TrackId int not null,
	Foreign Key (StudentId) references Students(StudentId),
	Foreign Key (TrackId) references Tracks(TrackId)
);

create table Payments(
	PaymentId int Primary Key identity(1,1),
	PaymentStatus NVARCHAR(30) not null,
	PaymentDate Date not null,
	Amount Decimal(10,2) not null,
	RegistrationId int not null unique,
	Foreign Key (RegistrationId) references Registrations(RegistrationId)
);