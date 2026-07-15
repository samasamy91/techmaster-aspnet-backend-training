using BookStoreApi.DTOs;

namespace BookStoreApi.Services.IServices
{
    public interface IAuthorService
    {
        public IEnumerable<AuthorResponse> GetAll();
        public AuthorResponse? GetById(int id);
        public AuthorResponse Create(CreateAuthorRequest request);
        public bool Delete(int id);
    }
}
