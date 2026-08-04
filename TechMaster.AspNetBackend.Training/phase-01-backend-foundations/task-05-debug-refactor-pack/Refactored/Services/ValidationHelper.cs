using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.Services
{
    public class ValidationHelper
    {
        //public string CustomerName { get; set; }
        //public string ProductName { get; set; }
        //public decimal Price { get; set; }  
        //public int Quantity { get; set; }
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
        public static bool IsValidPrice(decimal price)
        {
            return price > 0;
        }
        public static bool IsValidQuantity(int quantity)
        {
            return quantity>=0;
        }
        
    }
}
