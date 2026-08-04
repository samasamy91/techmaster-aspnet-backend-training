using BookStoreApi.Models;

namespace BookStoreApi.Seeds
{
    public class BooksData
    {
        public static readonly List<Book> books = new()
        {
            new Book
            {
                BookId = 1,
                Title = "Clean Code",
                ISBN = "9780132350884",
                PublishedYear = 2008,
                Price = 500,
                StockQuatity = 10,
                AuthorId = 1,
                CategoryId = 1,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            },

            new Book
            {
                BookId = 2,
                Title = "Refactoring",
                ISBN = "9780201485677",
                PublishedYear = 1999,
                Price = 650,
                StockQuatity = 5,
                AuthorId = 1,
                CategoryId = 1,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            }
        };
    }
}
