use SimpleStoreDB;

select * from Products;

select * from Products where IsAvailable=1;

select P.Name,C.Name AS Category,P.Price,P.StockQuantity
from Products P join Categories C 
ON P.CategoryId=C.CategoryId where C.Name='Electronics';

select * from Products where StockQuantity<5;

select * from Orders where CustomerId=1;

select O.OrderId,C.FullName,P.Name AS Product,OI.Quantity,OI.UnitPrice,O.Status,O.OrderDate
from Orders O join OrderItems OI
ON O.OrderId=OI.OrderId join Products P 
ON P.ProductId=OI.ProductId join Customers C 
ON C.CustomerId=O.CustomerId;

select SUM(TotalAmount) As TotalSales from Orders;

select C.Name As Category ,COUNT(P.ProductId) AS ProductCount
from Categories C Left join Products P ON C.CategoryId=P.CategoryId
Group by C.Name;

select P.Name,Sum(OI.Quantity) AS TotalSold
from Products P join OrderItems OI
ON P.ProductId=OI.ProductId Group by P.Name
Order by TotalSold DESC;

select S.Name AS Supplier ,P.Name AS Product,P.Price 
from Suppliers S join Products P 
ON S.SupplierId=P.SupplierId;