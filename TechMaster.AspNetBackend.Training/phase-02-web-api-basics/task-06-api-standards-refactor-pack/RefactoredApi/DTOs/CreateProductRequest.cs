using System.ComponentModel.DataAnnotations;

namespace RefactoredApi.DTOs
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Range(0.01,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public int Stock {  get; set; }
    }
}
