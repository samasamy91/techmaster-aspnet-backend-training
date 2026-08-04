using BookStoreApi.DTOs;
using BookStoreApi.Models;
using BookStoreApi.Seeds;
using BookStoreApi.Services.IServices;

namespace BookStoreApi.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> books = BooksData.books;
        private readonly IAuthorService authorService;
        private readonly ICategoryService categoryService;
        public BookService(IAuthorService authorService, ICategoryService categoryService)
        {
            this.authorService = authorService;
            this.categoryService = categoryService;
        }
        private BookResponse Map(Book book)
        {
            var author = authorService.GetAll().First(a => a.AuthorId == book.AuthorId);
            var category = categoryService.GetAll().First(c => c.CategoryId == book.CategoryId);
            return new BookResponse
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = author.FullName,
                CategoryName = category.Name,
                IsAvailable = book.IsAvailable,
                ISBN = book.ISBN,
                Price = book.Price,
                PublishedYear = book.PublishedYear,
                StockQuatity = book.StockQuatity,
                CreatedAt = book.CreatedAt
            };
        }
        public IEnumerable<BookResponse> GetAll(string? search, int? categoryId, int? authorId, bool? isAvailable, int pageNumber, int pageSize)
        {
            var query = books.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.ISBN.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                b.Title.Contains(search, StringComparison.OrdinalIgnoreCase)); 
            }
            if (categoryId.HasValue)
            {
                query = query.Where(b=>b.CategoryId==categoryId.Value);
            }
            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }
            if (isAvailable.HasValue)
            {
                query = query.Where(p => p.IsAvailable == isAvailable.Value);
            }
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return query.Select(Map);
        }
        public BookResponse? GetById(int id)
        {
            var book = books.FirstOrDefault(b=>b.BookId==id);
            if(book==null)
                return null;
            return Map(book);
        }
        public BookResponse Create(CreateBookRequest request)
        {
            if (books.Any(b => b.ISBN == request.ISBN))
            {
                throw new Exception("ISBN already exists");
            }
            var author = authorService.GetById(request.AuthorId);
            if (author == null)
                throw new Exception("Author does not exists");
            var category = categoryService.GetById(request.CategoryId);
            if (category == null)
                throw new Exception("Category not exists");
            if (!category.IsActive)
                throw new Exception("Category is inActive");
            var book = new Book
            {
                BookId = books.Count + 1,
                Title = request.Title,
                ISBN = request.ISBN,
                PublishedYear = request.PublishedYear,
                Price = request.Price,
                StockQuatity = request.StockQuantity,
                AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };
            books.Add(book);
            return Map(book);
        }
        public bool Update(int id,UpdateBookRequest request)
        {
            var book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
                return false;
            if (books.Any(b => b.ISBN == request.ISBN))
            {
                throw new Exception("ISBN already exists");
            }
            var author = authorService.GetById(request.AuthorId);
            if (author == null)
                throw new Exception("Author does not exists");
            var category = categoryService.GetById(request.CategoryId);
            if (category == null)
                throw new Exception("Category not exists");
            if (!category.IsActive)
                throw new Exception("Category is inActive");

            book.ISBN = request.ISBN;
            book.Title = request.Title;
            book.Price = request.Price;
            book.CategoryId = request.CategoryId;
            book.AuthorId = request.AuthorId;
            book.PublishedYear = request.PublishedYear;
            book.StockQuatity = request.StockQuantity;
            book.IsAvailable = request.IsAvailable;

            return true;

        }
        public bool Delete(int id)
        {
            var deleted = books.FirstOrDefault(b => b.BookId == id);
            if(deleted == null)
                return false;
            deleted.IsAvailable = false;
            return true;
        }
        public SummaryResponse GetSummary()
        {
            return new SummaryResponse
            {
                TotalBooks = books.Count,
                AvailableBooks = books.Count(b => b.IsAvailable),
                OutOfStock = books.Count(b => b.StockQuatity == 0),
                TotalInvetoryValue = books.Sum(b => b.Price * b.StockQuatity),
                BooksPerCategory = books.GroupBy(b => categoryService.GetAll().First(c => c.CategoryId == b.CategoryId).Name).ToDictionary
                (g => g.Key, g => g.Count()),
                BooksPerAuthor = books.GroupBy(b => authorService.GetAll().First(a => a.AuthorId == b.AuthorId).FullName).ToDictionary
                (g => g.Key, g => g.Count())
            };
        }
    }
}
