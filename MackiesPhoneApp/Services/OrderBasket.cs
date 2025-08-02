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
        public ObservableCollection<OrderItem> OrderBasketItems { get; set; } = new();

        public void AddToBasket(OrderItem orderItem)
        {
            OrderBasketItems.Add(orderItem);
        }

        public void RemoveFromBasket(OrderItem orderItem)
        {
            OrderBasketItems.Remove(orderItem);
        }

        public void ClearBasket()
        {
            OrderBasketItems.Clear();
            CustomerName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
