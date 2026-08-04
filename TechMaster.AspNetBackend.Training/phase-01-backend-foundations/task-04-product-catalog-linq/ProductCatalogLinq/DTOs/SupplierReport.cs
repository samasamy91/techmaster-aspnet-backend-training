using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogLinq.DTOs
{
    public class SupplierReport
    {
        public string SupplierName {  get; set; }
        public int ProductCount {  get; set; }
        public decimal StockValue {  get; set; }
        public decimal AveragePrice { get; set; }
    }
}
