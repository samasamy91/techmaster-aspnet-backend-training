using Microsoft.AspNetCore.Http.HttpResults;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;
using ProductsCategoriesApi.Services.IServices;
using System.Xml.Linq;

namespace ProductsCategoriesApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> categories = new();
        private int Id = 5;
        public CategoryService()
        {
            categories.AddRange(new[]
            {
                new Category
                {
                    CategoryId = 1,
                    Name = "Electronics",
                    Description = "Electronic devices",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Furniture",
                    Description = "Home and office furniture",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Stationery",
                    Description = "Office supplies",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Accessories",
                    Description = "Computer accessories",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            });
        }
        private Category Map(Category category)
        {
            return new Category
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
            };
        }
        public IEnumerable<Category> GetAll()
        {
            return categories.Where(c => c.IsActive).Select(Map);
        }
        public Category Create(CreateCategoryRequest request)
        {
            if (categories.Any(c => c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Category already exists");
            }
            var category = new Category
            {
                CategoryId = Id++,
                Name = request.Name,
                Description = request.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
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
