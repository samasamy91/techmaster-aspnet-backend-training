Create Database LibraryManagementDB;
use LibraryManagementDB;

create table Authors (
	AuthorId int primary key identity(1,1),
	FullName NVARCHAR(100) NOT NULL,
	BirthDate Date,
	County NVARCHAR(100)
);

create table Categories (
	CategoryId int primary key identity(1,1),
	Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR(250)
);

create table Books (
	BookId int primary key identity(1,1),
	Title NVARCHAR(100) NOT NULL,
	ISBN NVARCHAR(30) NOT NULL,
	PublishedYear int,
	AvailableCopies int not null,
	AuthorId int not null,
	CategoryId int not null,
	Foreign key(AuthorId) references Authors(AuthorId),
	Foreign key(CategoryId) references Categories(CategoryId)
);

create table Members (
	MemberId int primary key identity(1,1),
	FullName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(60) NOT NULL,
	PhoneNumber NVARCHAR(30),
	JoinDate Date not null,
	IsActive BIT not null
);


create table BorrowRecords (
	BorrowRecordId int primary key identity(1,1),
	Statuss NVARCHAR(60) NOT NULL,
	BorrowDate Date not null,
	ReturnDate Date not null,
	DueDate Date not null,
	BookId int not null,
	MemberId int not null,
	Foreign key(BookId) references Books(BookId),
	Foreign key(MemberId) references Members(MemberId)
);
