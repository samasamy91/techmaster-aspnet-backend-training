using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services.IServices
{
    public interface ICategoryService
    {
        public Category Create(CreateCategoryRequest request);
        public IEnumerable<Category> GetAll();
        public bool CategoryExists(int id);
    }
}
