Create Database SimpleStoreDB;

use SimpleStoreDB;

create table Customers(
	CustomerId int Primary Key identity(1,1),
	FullName NVARCHAR(100) not null,
	Email NVARCHAR(60) not null,
	PhoneNumber NVARCHAR(20) ,
	CreatedAt Date not null
);

create table Categories(
	CategoryId int Primary key identity(1,1),
	Name NVARCHAR(50) not null,
	Description NVARCHAR(260)
);

create table Suppliers(
	SupplierId int Primary key identity(1,1),
	Name NVARCHAR(50) not null,
	Email NVARCHAR(60) ,
	PhoneNumber NVARCHAR(20) ,
);

CREATE TABLE Products
(
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    StockQuantity INT NOT NULL,
    CategoryId INT NOT NULL,
    SupplierId INT NOT NULL,
    IsAvailable BIT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId)
);

CREATE TABLE Orders
(
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    OrderDate DATE NOT NULL,
    Status NVARCHAR(30) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE OrderItems
(
    OrderItemId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);