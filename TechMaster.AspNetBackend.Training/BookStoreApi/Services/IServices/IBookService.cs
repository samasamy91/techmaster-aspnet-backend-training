using BookStoreApi.DTOs;
using BookStoreApi.Models;

namespace BookStoreApi.Services.IServices
{
    public interface IBookService
    {
        public IEnumerable<BookResponse> GetAll(string? search, int? categoryId, int? authorId, bool? isAvailable, int pageNumber, int pageSize);
        public BookResponse? GetById(int id);
        public BookResponse Create(CreateBookRequest request);
        public bool Update(int id, UpdateBookRequest request);
        public bool Delete(int id);
        public SummaryResponse GetSummary();
    }
}
