using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.DTOs
{
    public class StockReportResponse
    {
        public decimal TotalStockValue  { get; set; }
        public Dictionary<int,decimal> StockValuePerCategory { get; set; }
        public Dictionary<int,int> ProductCountPerCategory { get; set; }
        public IEnumerable<Product> LowStockProducts { get; set; } = new List<Product>();
        public IEnumerable<Product> OutOfStockProducts { get; set; } = new List<Product>();

    }
}
