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
    }
}
