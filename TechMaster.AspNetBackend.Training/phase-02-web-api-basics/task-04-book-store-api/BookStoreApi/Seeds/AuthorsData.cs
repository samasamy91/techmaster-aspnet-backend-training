using BookStoreApi.Models;

namespace BookStoreApi.Seeds
{
    public class AuthorsData
    {
        public static readonly List<Author> authors = new()
        {
            new Author
            {
                AuthorId = 1,
                FullName = "Robert C. Martin",
                Country = "USA",
                BirthDate = new DateTime(1952,12,5),
                CreatedAt = DateTime.Now
            },
            new Author
            {
                AuthorId = 2,
                FullName = "Martin Fowler",
                Country = "UK",
                BirthDate = new DateTime(1963,12,18),
                CreatedAt = DateTime.Now
            }
        };
    }
}
