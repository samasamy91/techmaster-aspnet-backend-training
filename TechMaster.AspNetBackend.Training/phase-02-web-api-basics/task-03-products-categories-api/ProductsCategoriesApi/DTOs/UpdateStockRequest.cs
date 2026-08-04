using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.DTOs
{
    public class UpdateStockRequest
    {
        [Range(0,int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
