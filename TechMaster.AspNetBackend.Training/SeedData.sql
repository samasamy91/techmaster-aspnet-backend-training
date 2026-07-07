Insert into Authors (FullName,BirthDate,County)
Values 
('Sama Samy','2005-01-09','Egypt'),
('Ganna Ahmed','2002-01-09','Egypt'),
('Ahmed Samy','2009-01-07','Egypt'),
('Mohamed Samy','2001-11-09','United States'),
('Mariam Mohamed','2005-07-12','Egypt');

Insert into Categories (Name,Description)
Values 
('Fantasy','Fantasy novels'),
('Romance','Romance novels'),
('Horror','Horror novels'),
('Mystery','Mystery and detective books'),
('Drama','Drama books'),
('Science Fiction','Science fiction books');

INSERT INTO Books(Title, ISBN, PublishedYear, AvailableCopies, AuthorId, CategoryId)
VALUES
('Harry Potter and the Philosopher''s Stone', '9780747532743', 1997, 5, 1, 6),
('Harry Potter and the Chamber of Secrets', '9780747538493', 1998, 4, 1, 6),
('1984', '9780451524935', 1949, 3, 2, 5),
('Animal Farm', '9780451526342', 1945, 6, 2, 5),
('The Shining', '9780307743657', 1977, 2, 3, 2),
('IT', '9781501142970', 1986, 4, 3, 2),
('Murder on the Orient Express', '9780062693662', 1934, 5, 4, 3),
('The Alchemist', '9780061122415', 1988, 7, 5, 4);

INSERT INTO Members (FullName, Email, PhoneNumber, JoinDate, IsActive)
VALUES
('Sama Ali', 'ahmed@test.com', '01000000001', '2025-01-10', 1),
('Sara Mohamed', 'sara@test.com', '01000000002', '2025-02-15', 1),
('Omar Hassan', 'omar@test.com', '01000000003', '2025-03-01', 1),
('Mariam Adel', 'mariam@test.com', '01000000004', '2025-04-12', 0),
('Hossam Nabil', 'khaled@test.com', '01000000005', '2025-05-20', 1);

INSERT INTO BorrowRecords (BookId, MemberId, BorrowDate, DueDate, ReturnDate, Statuss)
VALUES
(8, 3, '2025-05-01', '2025-01-15',  '2026-01-20', 'Borrowed'),
(8, 3, '2026-05-01', '2026-01-15',  '2026-01-20', 'Borrowed'),
(1, 1, '2026-01-01', '2026-01-15', '2026-01-10', 'Returned'),
(3, 2, '2026-02-01', '2026-02-15',  '2026-01-11', 'Borrowed'),
(5, 3, '2026-02-05', '2026-02-19',  '2026-01-12', 'Borrowed'),
(2, 1, '2026-03-01', '2026-03-15', '2026-03-10', 'Returned'),
(1, 2, '2026-04-01', '2026-04-15',  '2026-04-10', 'Borrowed'),
(7, 5, '2026-04-10', '2026-04-24', '2026-04-20', 'Returned'),
(8, 3, '2026-05-01', '2026-05-15',  '2026-01-20', 'Borrowed'),
(3, 5, '2026-05-12', '2026-05-26',  '2026-02-10', 'Borrowed');