use SimpleStoreDB;

INSERT INTO Customers
(FullName, Email, PhoneNumber, CreatedAt)
VALUES
('Sama Samy','ahmed@test.com','0100000001','2026-01-10'),
('Sara Mohamed','sara@test.com','0100000002','2026-01-15'),
('Omar Hassan','omar@test.com','0100000003','2026-02-01'),
('Mariam Adel','mariam@test.com','0100000004','2026-02-10'),
('Khaled Nabil','khaled@test.com','0100000005','2026-03-01');

INSERT INTO Categories (Name, Description)
VALUES
('Electronics','Electronic devices'),
('Furniture','Home furniture'),
('Accessories','Computer accessories'),
('Office','Office supplies');

INSERT INTO Suppliers (Name, PhoneNumber, Email)
VALUES
('Tech Supplier','0111111111','tech@supplier.com'),
('Home Supplier','0222222222','home@supplier.com'),
('Office Supplier','0333333333','office@supplier.com');

INSERT INTO Products (Name, Price, StockQuantity, CategoryId, SupplierId, IsAvailable)
VALUES
('Laptop',45000,5,1,1,1),
('Mouse',650,40,3,1,1),
('Keyboard',2500,8,3,1,1),
('Office Chair',3500,12,2,2,1),
('Standing Desk',8000,3,2,2,1),
('Notebook',120,100,4,3,1),
('Printer Paper',450,50,4,3,1),
('USB Hub',1250,10,3,1,1),
('Monitor',9000,4,1,1,1),
('Desk Lamp',650,0,2,2,0);

INSERT INTO Orders
(CustomerId, OrderDate, Status, TotalAmount)
VALUES
(1,'2026-04-01','Completed',46300),
(2,'2026-04-02','Completed',9650),
(1,'2026-04-10','Pending',1200),
(3,'2026-04-15','Completed',3650),
(5,'2026-04-20','Completed',9000);

INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
VALUES
(1,1,1,45000),
(1,2,2,650),
(2,9,1,9000),
(2,6,5,120),
(3,8,1,1250),
(4,4,1,3500),
(4,6,1,120),
(5,9,1,9000);
