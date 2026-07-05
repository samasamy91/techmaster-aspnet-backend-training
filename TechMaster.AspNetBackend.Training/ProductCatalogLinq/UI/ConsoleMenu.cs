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
                Console.WriteLine("2. Exit");
                Console.Write("Choose: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Query1();
                        break;
                    //case "2":
                    //    exit = true;
                    //    break;
                    case "2":
                        Query2();
                        break;
                    case "3":
                        Query3();
                        break;
                    case "4":
                        Query4();
                        break;
                    case "4":
                        Query4();
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
    }
}
