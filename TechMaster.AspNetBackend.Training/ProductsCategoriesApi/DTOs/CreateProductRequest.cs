using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.DTOs
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Range(0.01,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; }
    }
}
