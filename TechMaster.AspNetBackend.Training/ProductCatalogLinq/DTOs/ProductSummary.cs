using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogLinq.DTOs
{
    public class ProductSummary
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category {  get; set; }
        public string StockStatus { get; set; }
    }
}
