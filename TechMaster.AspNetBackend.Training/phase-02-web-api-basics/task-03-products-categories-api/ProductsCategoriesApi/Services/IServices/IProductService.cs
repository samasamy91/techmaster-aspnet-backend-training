using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services.IServices
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAll(string? search, int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isAvailable);
        public Product Create(CreateProductRequest request);
        public Product? GetById(int id);
        public Product? Update(int id, UpdateProductRequest request);
        public bool UpdateStock(int id, int stockQuantity);
        public bool Delete(int id);
        public IEnumerable<Product> GetLowStock();
        public StockReportResponse GetStockReport();
    }
}
