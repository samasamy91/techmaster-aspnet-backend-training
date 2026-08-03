using BookStoreApi.DTOs;
using BookStoreApi.Models;
using BookStoreApi.Seeds;
using BookStoreApi.Services.IServices;

namespace BookStoreApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly List<Author> authors = AuthorsData.authors;

        //public AuthorService()
        //{
        //    authors.AddRange(new[]
        //    {
        //        new Author
        //        {
        //            AuthorId = 1,
        //            FullName = "Robert C. Martin",
        //            Country = "USA",
        //            BirthDate = new DateTime(1952,12,5),
        //            CreatedAt = DateTime.Now
        //        },
        //        new Author
        //        {
        //            AuthorId = 2,
        //            FullName = "Martin Fowler",
        //            Country = "UK",
        //            BirthDate = new DateTime(1963,12,18),
        //            CreatedAt = DateTime.Now
        //        }
        //    });
        //}

        private AuthorResponse Map(Author author)
        {
            return new AuthorResponse
            {
                AuthorId = author.AuthorId,
                FullName = author.FullName,
                Country = author.Country,
                BirthDate = author.BirthDate,
                CreatedAt = author.CreatedAt,
            };
        }
        public IEnumerable<AuthorResponse> GetAll()
        {
            return authors.Select(Map);
        }
        public AuthorResponse? GetById(int id)
        {
            var author = authors.FirstOrDefault(a=>a.AuthorId == id);
            if (author == null)
                return null;
            return Map(author);
        }
        public AuthorResponse Create(CreateAuthorRequest request)
        {
            if (authors.Any(a=>a.FullName.Equals(request.Name,StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Author already exists");
            }
            var author = new Author
            {
                AuthorId = authors.Count + 1,
                FullName = request.Name,
                Country = request.Country,
                BirthDate = request.BirthDate,
            };
            authors.Add(author);
            return Map(author);
        }
        public bool Delete(int id)
        {
            var author = authors.FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
                return false;
            authors.Remove(author);
            return true;
        }
    }
}
