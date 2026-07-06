using ProductCatalogLinq.DTOs;
using ProductCatalogLinq.Models;
using ProductCatalogLinq.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogLinq.UI
{
    public class ConsoleMenu
    {
        private ProductQueryServices productService;

        public ConsoleMenu()
        {
            productService = new ProductQueryServices();
        }

        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("===== Product Catalog LINQ =====");
                Console.WriteLine("1. Query 01 - Available Products");
                Console.WriteLine("2. Query 02 - Filter by Category");
                Console.WriteLine("3. Query 03 - Filter by Price Range");
                Console.WriteLine("4. Query 04 - Search by Product Name");
                Console.WriteLine("5. Query 05 - Sort by Price Ascending");
                Console.WriteLine("6. Query 06 - Sort by Price Descending");
                Console.WriteLine("7. Query 07 - Group Products by Category");
                Console.WriteLine("8. Count Products per Category");
                Console.WriteLine("9. Calculate Total Stock Value");
                Console.WriteLine("10. Stock Value per Category");
                Console.WriteLine("11. Top 5 Most Expensive Products");
                Console.WriteLine("12. Low Stock Products");
                Console.WriteLine("13. Out of Stock Products");
                Console.WriteLine("14. Product Summary");
                Console.WriteLine("15. Supplier Report");
                Console.WriteLine("16. Recently Added Products");
                Console.WriteLine("17. Category Statistics");
                Console.WriteLine("18. Products Above Average Price");
                Console.WriteLine("19. Search and Filter Products");
                Console.WriteLine("20. Pagination Simulation");
                Console.WriteLine("21. Exit");
                Console.Write("Choose: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Query1();
                        break;
                    case "2":
                        Query2();
                        break;
                    case "3":
                        Query3();
                        break;
                    case "4":
                        Query4();
                        break;
                    case "5":
                        Query5();
                        break;
                    case "6":
                        Query6();
                        break;
                    case "7":
                        Query7();
                        break;
                    case "8":
                        Query8();
                        break;
                    case "9":
                        Query9();
                        break;
                    case "10":
                        Query10();
                        break;
                    case "11":
                        Query11();
                        break;
                    case "12":
                        Query12();
                        break;
                    case "13":
                        Query13();
                        break;
                    case "14":
                        Query14();
                        break;
                    case "15":
                        Query15();
                        break;
                    case "16":
                        Query16();
                        break;
                    case "17":
                        Query17();
                        break;
                    case "18":
                        Query18();
                        break;
                    case "19":
                        Query19();
                        break;
                    case "20":
                        Query20();
                        break;
                    case "21":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        Pause();
                        break;
                }            }
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
        private void Query1()
        {
            List<Product> products = productService.GetAvailableProducts();
            foreach (Product product in products)
                Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Price}");
            Pause();
        }
        private void Query2()
        {
            Console.WriteLine("Enter category: ");
            string category = Console.ReadLine();
            List<Product> result = productService.FilterByCategory(category);
            if (result.Count == 0)
                Console.WriteLine("No Products found ");
            else
            {
                foreach (Product product in result)
                    Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Category} - {product.Price} - Stock: {product.StockQuantity}");
            }
            Pause();
        }
        private void Query3()
        {
            Console.WriteLine("Enter min price: ");
            decimal min;
            if(!decimal.TryParse(Console.ReadLine(), out min))
            {
                Console.WriteLine("Invalid min price");
                Pause() ;
                return;
            }
            Console.WriteLine("Enter max price: ");
            decimal max;
            if(!decimal.TryParse(Console.ReadLine(),out max))
            {
                Console.WriteLine("Invalid max price");
                Pause() ;
                return;
            }
            if (min > max)
            {
                Console.WriteLine("min cannot be greater than max ");
                Pause();
                return;
            }
            List<Product> result=productService.FilterByPriceRange(min,max);
            if (result.Count == 0)
                Console.WriteLine("No products found");
            else
            {
                foreach(Product product in result)
                    Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Category} - {product.Price} - Stock: {product.StockQuantity}");
            }
            Pause() ;
        }
        private void Query4()
        {
            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) { 
                Console.WriteLine("Name is required ");
                Pause();
                return; 
            }
            List<Product> result = productService.SearchByProductName(name);
            if (result.Count == 0)
                Console.WriteLine("No Products found");
            else
            {
                foreach( Product product in result)
                    Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Category} - {product.Price} - Stock: {product.StockQuantity}");
            }
            Pause();
        }
        private void Query5()
        {
            List<Product> result = productService.SortByPriceAsc();
            foreach( Product product in result)
            {
                Console.WriteLine($"{product.Name} - {product.Price:C}");
            }
            Pause();
        }
        private void Query6()
        {
            List<Product> result = productService.SortByPriceDesc();
            foreach (Product product in result)
            {
                Console.WriteLine($"{product.Name} - {product.Price:C}");
            }
            Pause();
        }
        private void Query7()
        {
            var groups = productService.GroupProductsByCategory();
            foreach(var group in groups)
            {
                Console.WriteLine($"===== {group.Key} ======");
                foreach(Product product in group)
                    Console.WriteLine($"{product.Name} - {product.Price:C}");
            }
            Pause();
        }
        private void Query8()
        {
            var result = productService.CountProductsPerCategory();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Category} : {item.Count} products");
            }
            Pause();
        }
        private void Query9()
        {
            decimal total = productService.TotalStockValue();
            Console.WriteLine($"Total Stock value : {total}");
            Pause();
        }
        private void Query10()
        {
            List<CategoryStockValue> result = productService.StockPerCategory();
            foreach( CategoryStockValue item in result)
                Console.WriteLine($"{item.Category} : {item.StockValue:C}");
            Pause() ;
        }
        private void Query11()
        {
            List<Product> result = productService.Top5MostExpensive();
            if(result.Count==5)
                Console.WriteLine("No Products found");
            else
            {
                foreach(Product product in result)
                    Console.WriteLine($"{product.Name} | {product.Price:C}");
            }
            Pause();
        }
        private void Query12()
        {
            List<Product> result = productService.LowStockProducts();
            if(result.Count==0)
                Console.WriteLine("No low stock products found.");
            else
            {
                Console.WriteLine("Low Stock Products\n");
                foreach (Product product in result)
                    Console.WriteLine($"{product.Name} | Quantity: {product.StockQuantity}");
            }
            Pause();
        }
        private void Query13()
        {
            List<Product> result = productService.OutOfStock();
            if (result.Count == 0)
                Console.WriteLine("No out stock products found.");
            else
            {
                Console.WriteLine("Low Stock Products\n");
                foreach (Product product in result)
                    Console.WriteLine($"{product.Name} | Quantity: {product.StockQuantity} | Available: {product.IsAvailable}");
            }
            Pause() ;
        }
        private void Query14()
        {
            List<ProductSummary> result = productService.ProductSummaries();
            foreach(ProductSummary product in result)
                Console.WriteLine($"{product.Name} | {product.Category} | {product.Price:C} | {product.StockStatus}");
            Pause() ;
        }
        private void Query15()
        {
            List<SupplierReport> reports = productService.SupplierReport();
            Console.WriteLine("Supplier Report\n");
            foreach (SupplierReport report in reports)
                Console.WriteLine($"{report.SupplierName} | Products: {report.ProductCount} | Stock Value: {report.StockValue:C} | Average Price: {report.AveragePrice:C}");
            Pause();
        }
        private void Query16()
        {
            List<Product> result = productService.RecentlyAdded();
            if(result.Count == 0)
                Console.WriteLine("No recently added products found.");
            else
            {
                Console.WriteLine("Recently Added Products");
                foreach(Product product in result)
                    Console.WriteLine($"{product.Name} | Created: {product.CreatedAt:yyyy-MM-dd}");
            }
            Pause() ;
        }
        private void Query17()
        {
            List<CategoryStatus> result = productService.CategoryStatistics();
            Console.WriteLine("Category Statistics \n");
            foreach (CategoryStatus item in result)
            {
                Console.WriteLine($"{item.Category} | " +$"Count: {item.Count} | " +$"Avg: {item.Avg:C} | " +
                    $"Max: {item.Max:C} | " +$"Min: {item.Min:C} | " +$"Stock Value: {item.TotalStock:C}");
            }
            Pause(); 
        }
        private void Query18()
        {
            List<Product> result = productService.ProductAvgPrice();
            if (result.Count == 0)
                Console.WriteLine("No products found above avg price ");
            else
            {
                decimal avgprice=productService.GetAvailableProducts().Average(p => p.Price);
                Console.WriteLine($"Average price : {avgprice:C}\n");
                foreach(Product product in result)
                    Console.WriteLine($"{product.Name} | {product.Price:C}");
            }
            Pause();
        }
        private void Query19()
        {
            Console.Write("Category: ");
            string category = Console.ReadLine();
            Console.Write("Minimum Price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Maximum Price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Available Only (true/false): ");
            bool isAvailable = bool.Parse(Console.ReadLine());
            List<Product> result = productService.FilterSearches(category, minPrice, maxPrice, isAvailable);
            if(result.Count == 0)
                Console.WriteLine("No products found ");
            else
            {
                foreach(Product product in result)
                    Console.WriteLine($"{product.Name} | {product.Category} | {product.Price:C} | Stock: {product.StockQuantity}");
            }
        }
        private void Query20()
        {
            Console.Write("Enter Page Number: ");
            int pageNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter Page Size: ");
            int pageSize = int.Parse(Console.ReadLine());
            List<Product> result = productService.ProductsByPage(pageNumber, pageSize);
            if (result.Count == 0)
                Console.WriteLine("No products found or invalid page number");
            else
            {
                Console.WriteLine($"Page {pageNumber}");
                foreach(Product product in result)
                    Console.WriteLine($"{product.ProductId} - {product.Name} | {product.Price:C}");
            }
            Pause();
        }
    }
}
