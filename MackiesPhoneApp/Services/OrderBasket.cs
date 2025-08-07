
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

            order.OrderItemsList.Add(orderItem);
        }

        public void RemoveOrderItemFromBasket(OrderItem orderItem)
        {
            order.OrderItemsList.Remove(orderItem);
        }

        public void ClearBasket()
        {
            order.OrderItemsList.Clear();
            order.CustomerName = string.Empty;
            order.CustomerEmail = string.Empty;
            order.CustomerPhoneNumber = string.Empty;
        }

      
    }
}
