using RefactoredApi.DTOs;
using RefactoredApi.Models;
using RefactoredApi.Services.IServices;

namespace RefactoredApi.Services
{
    public class ProductService : IProductService
    {
        private static readonly List<Product> products = new();
        private ProductResponse Map(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
        public IEnumerable<ProductResponse> GetAll()
        {
            return products.Select(Map).ToList();
        }
        public ProductResponse GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return null;
            return Map(product);
        }
        public ProductResponse Create(CreateProductRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new Exception("Name is required");
            var product = new Product
            {
                Id = products.Any() ? products.Max(p => p.Id) + 1 : 1,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            products.Add(product);
            return Map(product);
        }
    }
}
