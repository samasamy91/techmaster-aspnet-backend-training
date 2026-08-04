using Refactored.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactored.Services
{
    public class OrderCalculator
    {
        private const decimal TaxRate = 0.14m;
        private const decimal ShippingFee = 50m;
        private const decimal SilverDiscount = 0.05m;
        private const decimal GoldDiscount = 0.10m;
        private const decimal VipDiscount = 0.15m;
        public decimal CalcDiscount (Order order,Customer customer)
        {
            decimal subtotal = order.SubTotal;
            switch (customer.Type)
            {
                case CutomerType.Silver:
                    return subtotal*SilverDiscount;
                case CutomerType.Gold:
                    return subtotal*GoldDiscount;
                case CutomerType.VIP:
                    return subtotal * VipDiscount;
                default:
                    return 0;
            }
        }
        public decimal CalcTax(decimal amountAfterDisc)
        {
            return amountAfterDisc*TaxRate;
        }
        public decimal CalShipping(decimal amountAfterDisc)
        {
            if (amountAfterDisc >= 1000)
            {
                return 0;
            }
            return ShippingFee;
        }
        public decimal CalcFinalTotal(Order order,Customer customer)
        {
            decimal disc = CalcDiscount(order, customer);
            decimal afterDisc = order.SubTotal - disc;
            decimal tax = CalcTax(afterDisc);
            decimal shipping = CalShipping(afterDisc);
            return afterDisc + tax + shipping;
        }
    }
}
