using BookStoreApi.DTOs;
using BookStoreApi.Models;

namespace BookStoreApi.Services.IServices
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAll();
        public Category? GetById(int id);
        public Category Create(CreateCategoryRequest request);
        public bool CategoryExists(int id);
    }
}
