using Refactored.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.UI
{
    public class ReceiptPrinter
    {
        public void Print(Customer customer,Order order,
            decimal discount,decimal tax,decimal shipping, decimal finaltotal)
        {
            Console.WriteLine("========== RECEIPT ==========");
            Console.WriteLine($"Customer : {customer.Name}");
            Console.WriteLine($"Type     : {customer.Type}");
            Console.WriteLine($"Product  : {order.ProductName}");
            Console.WriteLine($"Price    : {order.Price}");
            Console.WriteLine($"Quantity : {order.Quantity}\n");
            Console.WriteLine($"Subtotal : {order.SubTotal}");
            Console.WriteLine($"Discount : {discount}");
            Console.WriteLine($"Tax      : {tax}");
            Console.WriteLine($"Shipping : {shipping}\n");
            Console.WriteLine($"Final    : {finaltotal}\n");

        }
    }
}
