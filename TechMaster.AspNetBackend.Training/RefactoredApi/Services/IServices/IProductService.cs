using RefactoredApi.DTOs;

namespace RefactoredApi.Services.IServices
{
    public interface IProductService
    {
        public IEnumerable<ProductResponse> GetAll();
        public ProductResponse GetById(int id);
        public ProductResponse Create(CreateProductRequest request);
    }
}
