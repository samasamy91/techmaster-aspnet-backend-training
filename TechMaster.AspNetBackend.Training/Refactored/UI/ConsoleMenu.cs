using Refactored.Models;
using Refactored.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.UI
{
    public class ConsoleMenu
    {
        private readonly OrderCalculator calc = new OrderCalculator();
        private readonly ReceiptPrinter printer = new ReceiptPrinter();
        public void Run()
        {
            Console.WriteLine("====== Order Calculator ======");
            Customer customer = ReadCustomer();
            Order order = ReadOrder();
            decimal disc = calc.CalcDiscount(order, customer);
            decimal afterDisc = order.SubTotal - disc;
            decimal tax = calc.CalcTax(afterDisc);
            decimal shipping = calc.CalShipping(afterDisc);
            decimal finalTotal = calc.CalcFinalTotal(order, customer);
            printer.Print(customer,order,disc,tax,shipping,finalTotal);
        }
        private Customer ReadCustomer()
        {
            Customer customer = new Customer();
            Console.Write("Customer Name: ");
            customer.Name = Console.ReadLine();
            while (!ValidationHelper.IsValidName(customer.Name))
            {
                Console.Write("Invalid name. Enter again: ");
                customer.Name = Console.ReadLine();
            }
            Console.Write("Customer Type (Regular/Silver/Gold/VIP): ");
            CutomerType type;
            while (!Enum.TryParse(Console.ReadLine(), true, out type))
            {
                Console.Write("Invalid type. Enter again: ");
            }
            customer.Type = type;
            return customer;
        }
        private Order ReadOrder()
        {
            Order order = new Order();
            Console.WriteLine("Product Name: ");
            while (!ValidationHelper.IsValidName(order.ProductName))
            {
                Console.Write("Invalid product name. Enter again: ");
                order.ProductName = Console.ReadLine();
            }
            Console.Write("Price: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) ||
                   !ValidationHelper.IsValidPrice(price))
            {
                Console.Write("Invalid price. Enter again: ");
            }
            order.Price = price;
            Console.Write("Quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) ||
                   !ValidationHelper.IsValidQuantity(quantity))
            {
                Console.Write("Invalid quantity. Enter again: ");
            }
            order.Quantity = quantity;
            return order;
        }

    }
}
