using ProductCatalogLinq.DTOs;
using ProductCatalogLinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogLinq.Services
{
    public class ProductQueryServices
    {
        private List<Product> products;
        public ProductQueryServices()
        {
            products = ProductSeeder.SeedProduct();
        }
        public List<Product> GetAvailableProducts()
        {
            return products.Where(p=>p.IsAvailable).ToList();
        }
        public List<Product> FilterByCategory(string category)
        {
            return products.Where(p=>p.Category.Equals(category,StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Product> FilterByPriceRange(decimal min,decimal max)
        {
            return products.Where(p => p.Price >= min && p.Price <= max).ToList();
        }
        public List<Product> SearchByProductName(string name)
        {
            return products.Where(p=>p.Name.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Product> SortByPriceAsc()
        {
            return products.OrderBy(p=>p.Price).ToList();
        }
        public List<Product> SortByPriceDesc()
        {
            return products.OrderByDescending(p => p.Price).ToList();
        }
        public IEnumerable<IGrouping<string,Product>> GroupProductsByCategory()
        {
            return products.GroupBy(p=>p.Category).ToList();
        }
        public IEnumerable<dynamic> CountProductsPerCategory()
        {
            return products.GroupBy(p => p.Category).Select(g=>new
            {
                Category = g.Key,
                Count = g.Count() //count number of products in categiory
            });
        }
        public decimal TotalStockValue()
        {
            return products.Sum(p => p.Price * p.StockQuantity);
        }
        public List<CategoryStockValue> StockPerCategory()
        {
            return products.GroupBy(p=>p.Category).Select(g=>new CategoryStockValue
            {
                Category = g.Key,
                StockValue = g.Sum(p=>p.Price * p.StockQuantity)
            }).ToList();
        }
        public List<Product> Top5MostExpensive()
        {
            return products.OrderByDescending(p=>p.Price).Take(5).ToList();
        }
        public List<Product> LowStockProducts()
        {
            return products.Where(p=>p.StockQuantity<=5).ToList();
        }
        public List<Product> OutOfStock()
        {
            return products.Where(p=>p.StockQuantity==0 || !p.IsAvailable).ToList();
        }
        public List<ProductSummary> ProductSummaries()
        {
            return products.Select(p=>new ProductSummary
            {
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                StockStatus = p.StockQuantity > 0?"In Stock": "Out of stock"
            }).ToList();
        }
        public List<SupplierReport> SupplierReport()
        {
            return products.GroupBy(p=>p.SupplierName).Select(g=> new SupplierReport
            {
                SupplierName = g.Key,
                ProductCount = g.Count(),
                StockValue = g.Sum(p=>p.Price * p.StockQuantity),
                AveragePrice = g.Average(p=>p.Price)
            }).ToList();
        }
        public List<Product> RecentlyAdded()
        {
            DateTime today = new DateTime(2026, 7, 5);
            return products.Where(p => p.CreatedAt >= DateTime.Today.AddDays(-60)).ToList();
        }
        public List<CategoryStatus> CategoryStatistics()
        {
            return products.GroupBy(p=>p.Category).Select(g=>new CategoryStatus
            {
                Category = g.Key,
                Count = g.Count(),
                Avg = g.Any()? g.Average(p=>p.Price) : 0,
                Max = g.Any() ? g.Max(p=>p.Price) : 0,
                Min = g.Any() ? g.Min(p=>p.Price) : 0,
                TotalStock = g.Sum(p=>p.Price * p.StockQuantity)
            }).ToList() ;
        }
        public List<Product> ProductAvgPrice()
        {
            decimal avg=products.Average(p=>p.Price);
            return products.Where(p=>p.Price>avg).ToList();
        }
        public List<Product> FilterSearches(string category,decimal min,decimal max ,bool isAvailable)
        {
            IEnumerable<Product> query = products;
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            query = query.Where(p=>p.Price > min && p.Price < max);
            query = query.Where(p => p.IsAvailable == isAvailable);
            return query.ToList();
        }
        public List<Product> ProductsByPage(int pageNum,int pageSize)
        {
            if(pageNum <= 0 || pageSize <= 0)
                return new List<Product>();
            return products.Skip((pageNum -1)* pageSize).Take(pageSize).ToList();
        }
    }
}
