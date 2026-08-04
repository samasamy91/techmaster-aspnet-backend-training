using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs
{
    public class UpdateBookRequest
    {
        [Required]
        public string Title { get; set; } 
        [Required]
        public string ISBN { get; set; } 
        public int PublishedYear { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
