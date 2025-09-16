using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MackiesPhoneApp.Pages.User
{
    public partial class OrderBasketPage : ContentPage
    {
        private readonly OrderBasket _orderBasketService;
        private ToolbarItem _totalToolbarItem;

        public OrderBasketPage()
        {
            InitializeComponent();

            _orderBasketService = ServiceHelper.GetService<OrderBasket>();

            var grid = SetNavigationBarPageTitle.setContentTotal();

            NavigationPage.SetTitleView(this, grid);

           
            BindingContext = _orderBasketService.order;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            _orderBasketService.ClearBasket();
        }

        private async void OnSubmitOrderClicked(object sender, EventArgs e)
        {
            var orderItemList = MakeOrderItemsDtos();
            var orderDto = MakeOrderDto();

            orderDto.OrderLines = orderItemList;

            var orderContent =JsonSerializer.Serialize(orderDto);
            var url = "/Home/createorder";
            await CustomHttpClient.postRequest(url, false, orderContent, null);

            _orderBasketService.ClearBasket();

           
            await Navigation.PopToRootAsync();

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

        private OrderDto MakeOrderDto ()
        {
            var orderDto = new OrderDto();

            orderDto.CustomerName = _orderBasketService.order.CustomerName;
            orderDto.Email = _orderBasketService.order.CustomerEmail;
            orderDto.Phone = _orderBasketService.order.CustomerPhoneNumber;

            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000); // 100000 inclusive, 1000000 exclusive

            orderDto.CustomerOrderCode = randomNumber.ToString();

            orderDto.TemplateScheduleId = _orderBasketService.order.TemplateSchedule.Id;

            orderDto.DeliveryDate = _orderBasketService.order.TemplateSchedule.Date;

            orderDto.FishShopId = _orderBasketService.order.FishShop.Id;
            orderDto.LocationId = _orderBasketService.order.LocationId;

            orderDto.Comment = _orderBasketService.order.Comment;

            return orderDto;

        }

        private List<OrderItemDto> MakeOrderItemsDtos()
        {
            var OrderItemsList = new List<OrderItemDto>();

            foreach (var orderItem in _orderBasketService.order.OrderItemsList)
            {
                var orderItemDto = new OrderItemDto();

                orderItemDto.OrderId = orderItem.orderid;
                orderItemDto.ProductId = orderItem.productid;
                orderItemDto.ProductName = orderItem.productname;
                orderItemDto.UnitPrice = orderItem.unitprice;
                orderItemDto.Quantity = orderItem.quantity;
                orderItemDto.ProductTypes = orderItem.producttypes;
                orderItemDto.PizzaNumber = orderItem.productnumber;
                orderItemDto.DiscountedUnitPrice = orderItem.discountedunitprice;
                orderItemDto.UnitDiscountPercentage = orderItem.unitdiscountpercentage;

                OrderItemsList.Add(orderItemDto);
            }

            return OrderItemsList;

        }
    }
}
