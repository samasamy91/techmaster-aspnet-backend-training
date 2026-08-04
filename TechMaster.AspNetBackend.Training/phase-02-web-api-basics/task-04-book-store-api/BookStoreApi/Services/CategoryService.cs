using BookStoreApi.DTOs;
using BookStoreApi.Models;
using BookStoreApi.Seeds;
using BookStoreApi.Services.IServices;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStoreApi.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly List<Category> categories = CategoryData.categories;
        private Category Map(Category category)
        {
            return new Category
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }
        public IEnumerable<Category> GetAll()
        {
            return categories.Where(c => c.IsActive).Select(Map);
        }
        public Category? GetById(int id)
        {
            var category = categories.FirstOrDefault(c=>c.CategoryId == id);
            if (category == null)
                return null;
            return Map(category);
        }
        public Category Create(CreateCategoryRequest request)
        {
            if (categories.Any(c => c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Category already exists");
            }
            var category = new Category
            {
                CategoryId = categories.Count+1,
                Name = request.Name,
                Description = request.Description,
                IsActive = true,
            };
            categories.Add(category);
            return Map(category);
        }
        public bool CategoryExists(int id)
        {
            return categories.Any(c => c.CategoryId == id && c.IsActive);
        }
    }
}
