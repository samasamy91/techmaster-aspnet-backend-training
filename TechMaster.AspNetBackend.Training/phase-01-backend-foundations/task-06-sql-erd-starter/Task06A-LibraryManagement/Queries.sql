use LibraryManagementDB
 
select * from Books;

select * from Members where IsActive=1;

select B.Title,C.Name As Category from Books B 
	Join Categories C On B.CategoryId = C.CategoryId where C.Name = 'Fantasy';

select C.Name,
		Count(Books.BookId) As BookCount
		from Categories C left join Books 
		ON C.CategoryId=Books.CategoryId Group by C.Name;

select M.FullName,B.Title , BR.BorrowDate, BR.DueDate, BR.ReturnDate, BR.Statuss
	From BorrowRecords BR join Members M ON M.MemberId=BR.MemberId 
	join Books B ON B.BookId=BR.BookId;

select M.Email,B.Title,Br.DueDate
From BorrowRecords Br Join Members M ON M.MemberId=Br.MemberId
Join Books B ON B.BookId=Br.BookId where Br.ReturnDate Is Null
And Br.DueDate < GETDATE();

select B.Title,BR.BorrowDate,BR.ReturnDate,BR.Statuss
from BorrowRecords BR Join Books B ON B.BookId=BR.BookId
where BR.MemberId = 1 Order by BR.BorrowDate Desc;

select Title,AvailableCopies from Books
where AvailableCopies>0;

select A.FullName, COUNT(B.BookId) AS TotalBooks
from Authors A left join Books B 
ON B.AuthorId=A.AuthorId
Group by A.FullName

select top 5 B.Title,COUNT(BR.BookId) AS BorrowCount
from BorrowRecords BR join Books B ON B.BookId=BR.BookId
Group by B.Title Order by BorrowCount DESC;