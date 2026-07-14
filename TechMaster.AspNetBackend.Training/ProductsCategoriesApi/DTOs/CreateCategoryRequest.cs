using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.DTOs
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name {  get; set; }
        public string Description { get; set; }
    }
}
