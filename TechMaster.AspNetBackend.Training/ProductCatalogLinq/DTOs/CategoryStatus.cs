using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogLinq.DTOs
{
    public class CategoryStatus
    {
        public string Category {  get; set; }
        public int Count {  get; set; }
        public decimal Avg {  get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal TotalStock {  get; set; }
    }
}
