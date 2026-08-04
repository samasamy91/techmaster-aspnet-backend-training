using BookStoreApi.Models;

namespace BookStoreApi.Seeds
{
    public class CategoryData
    {
        public static readonly List<Category> categories = new()
        {
            new Category
            {
                CategoryId = 1,
                Name = "Programming",
                Description = "Software development books",
                IsActive = true
            },

            new Category
            {
                CategoryId = 2,
                Name = "Database",
                Description = "Database design and SQL",
                IsActive = true
            },
            new Category
            {
                CategoryId = 3,
                Name = "Backend",
                Description = ".NET Entity Framwork",
                IsActive = true
            },
            new Category
            {
                CategoryId = 4,
                Name = "Frontend",
                Description = "React and Bootstrap",
                IsActive = true
            }
        };
    }
}
