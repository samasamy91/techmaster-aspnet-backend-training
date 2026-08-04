using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogLinq.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsAvailable { get; set; } = true;

        public string SupplierName { get; set; }
    }
}
