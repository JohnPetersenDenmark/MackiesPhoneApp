
using MackiesPhoneApp.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Services
{
    public class OrderBasket
    {         
        public Order order { get; set; } = new();

        public OrderBasket()
        {
            order.OrderItemsList = new ObservableCollection < OrderItem > ();
        }

        public void AddOrderItemToBasket(OrderItem orderItem)
        {
            if (order.OrderItemsList.Any(item => item.productid == orderItem.productid))
            {
                return;
            }

            orderItem.IsInBasket = true;

            order.OrderItemsList.Add(orderItem);
        }

        public bool IsProductInBasket(int productType, int productId)
        {
            var x = order.OrderItemsList.Where(product => product.productid == productId && product.producttype == productType);
            
            if (x.Count() > 0) {
                return true;
            }
            return false;
        }

        public void RemoveOrderItemFromBasket(OrderItem orderItem)
        {
            orderItem.IsInBasket = false;
            order.OrderItemsList.Remove(orderItem);
        }

        public void ClearBasket()
        {
            order.OrderItemsList.Clear();
            order.CustomerName = string.Empty;
            order.CustomerEmail = string.Empty;
            order.CustomerPhoneNumber = string.Empty;
            order.Comment = string.Empty;
            order.LocationId = 0;
        }

      
    }
}
