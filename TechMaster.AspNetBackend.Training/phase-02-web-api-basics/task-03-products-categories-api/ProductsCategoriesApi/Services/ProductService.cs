using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;
using ProductsCategoriesApi.Services.IServices;

namespace ProductsCategoriesApi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> products = new();
        private readonly ICategoryService categoryService;
        private int Id = 16;
        public ProductService(ICategoryService categoryService)
        {
            products.AddRange(new[]
            {
                new Product
                {
                    ProductId = 1,
                    Name = "Laptop",
                    CategoryId = 1,
                    Price = 25000,
                    StockQuantity = 10,
                    IsAvailable = true,
                    SupplierName = "Dell",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Mouse",
                    CategoryId = 4,
                    Price = 350,
                    StockQuantity = 50,
                    IsAvailable = true,
                    SupplierName = "Logitech",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Keyboard",
                    CategoryId = 1,
                    Price = 800,
                    StockQuantity = 40,
                    IsAvailable = true,
                    SupplierName = "Logitech",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 4,
                    Name = "USB-C Hub",
                    CategoryId = 1,
                    Price = 650,
                    StockQuantity = 25,
                    IsAvailable = true,
                    SupplierName = "Anker",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 5,
                    Name = "External SSD",
                    CategoryId = 1,
                    Price = 3200,
                    StockQuantity = 8,
                    IsAvailable = true,
                    SupplierName = "Kingston",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Office Chair",
                    CategoryId = 2,
                    Price = 4500,
                    StockQuantity = 12,
                    IsAvailable = true,
                    SupplierName = "IKEA",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Office Desk",
                    CategoryId = 2,
                    Price = 7000,
                    StockQuantity = 6,
                    IsAvailable = true,
                    SupplierName = "IKEA",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Desk Lamp",
                    CategoryId = 2,
                    Price = 950,
                    StockQuantity = 20,
                    IsAvailable = true,
                    SupplierName = "Philips",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Notebook",
                    CategoryId = 3,
                    Price = 75,
                    StockQuantity = 100,
                    IsAvailable = true,
                    SupplierName = "Classmate",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 10,
                    Name = "Pen Set",
                    CategoryId = 3,
                    Price = 120,
                    StockQuantity = 80,
                    IsAvailable = true,
                    SupplierName = "Parker",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 11,
                    Name = "Marker Pack",
                    CategoryId = 3,
                    Price = 95,
                    StockQuantity = 50,
                    IsAvailable = true,
                    SupplierName = "Sharpie",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Paper Pack",
                    CategoryId = 3,
                    Price = 200,
                    StockQuantity = 70,
                    IsAvailable = true,
                    SupplierName = "Double A",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 13,
                    Name = "Mouse",
                    CategoryId = 4,
                    Price = 350,
                    StockQuantity = 50,
                    IsAvailable = true,
                    SupplierName = "Logitech",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductId = 14,
                    Name = "Mouse Pad",
                    CategoryId = 4,
                    Price = 150,
                    StockQuantity = 60,
                    IsAvailable = true,
                    SupplierName = "Redragon",
                    CreatedAt = DateTime.UtcNow
                },

                new Product
                {
                    ProductId = 15,
                    Name = "Laptop Sleeve",
                    CategoryId = 4,
                    Price = 400,
                    StockQuantity = 30,
                    IsAvailable = true,
                    SupplierName = "HP",
                    CreatedAt = DateTime.UtcNow
                }
            });
            this.categoryService = categoryService;
        }
        private Product Map(Product product)
        {
            return new Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable,
                SupplierName = product.SupplierName,
                CreatedAt = DateTime.UtcNow
            };
        }
        public IEnumerable<Product> GetAll(string? search, int?categoryId, decimal? minPrice, decimal? maxPrice, bool?isAvailable)
        {
            var query = products.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            if(minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }
            if(isAvailable.HasValue)
            {
                query = query.Where(p => p.IsAvailable == isAvailable.Value);
            }
            return query.Select(Map);
        }
        public Product Create(CreateProductRequest request)
        {
            if (!categoryService.CategoryExists(request.CategoryId))
            {
                throw new InvalidOperationException("Category does not exist");
            }
            var product = new Product
            {
                ProductId = Id++,
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsAvailable = request.StockQuantity>0,
                SupplierName = request.SupplierName,
                CreatedAt = DateTime.UtcNow
            };
            products.Add(product);
            return Map(product);
        }
        public Product? GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if(product == null) 
                return null;
            return Map(product);
        }
        public Product? Update (int id,UpdateProductRequest request)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if( product == null)
                return null;
            if (!categoryService.CategoryExists(request.CategoryId))
            {
                throw new InvalidOperationException("Category does not exists");
            }
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.IsAvailable = request.IsAvailable;
            product.SupplierName = request.SupplierName;
            return Map(product);
        }
        public bool UpdateStock(int id,int stockQuantity)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if(product==null)
                return false;
            product.StockQuantity = stockQuantity;
            product.IsAvailable = stockQuantity > 0;
            return true;
        }
        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if( product == null) return false;
            product.IsAvailable = false;
            return true;
        }
        public IEnumerable<Product> GetLowStock()
        {
            return products.Where(p => p.StockQuantity < 5).Select(Map);
        }
        public StockReportResponse GetStockReport()
        {
            return new StockReportResponse
            {
                TotalStockValue = products.Sum(p => p.Price * p.StockQuantity),
                StockValuePerCategory = products.GroupBy(p => p.CategoryId).ToDictionary(
                    g => g.Key,
                    g => g.Sum(p => p.Price * p.StockQuantity)
                    ),
                ProductCountPerCategory = products.GroupBy(p => p.CategoryId).ToDictionary(
                    g => g.Key,
                    g => g.Count()),
                LowStockProducts = products.Where(p=>p.StockQuantity<5).Select(Map),
                OutOfStockProducts = products.Where(p=>p.StockQuantity==0).Select(Map)
            };

        }
    }
}
