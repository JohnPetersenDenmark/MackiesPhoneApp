using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Microsoft.Maui.Controls;
using System;

namespace MackiesPhoneApp.Pages.User
{
    public partial class OrderBasketPage : ContentPage
    {
        private readonly OrderBasket _orderBasketService;

        public OrderBasketPage()
        {
            InitializeComponent();

            _orderBasketService = ServiceHelper.GetService<OrderBasket>();
          
            BindingContext = _orderBasketService.order;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            _orderBasketService.ClearBasket();
        }

        private void OnImagePlusTapped(object? sender, EventArgs e)
        {
            if (sender is Image img && img.BindingContext is OrderItem orderItem)
            {
                orderItem.quantity++;
            }
        }

        private void OnImageMinusTapped(object? sender, EventArgs e)
        {
            if (sender is Image img && img.BindingContext is OrderItem orderItem)
            {
                if (orderItem.quantity > 1)
                {
                    orderItem.quantity--;
                }
                else
                {
                    // Remove item from basket
                    _orderBasketService.RemoveOrderItemFromBasket(orderItem);
                }
            }
        }
    }
}
