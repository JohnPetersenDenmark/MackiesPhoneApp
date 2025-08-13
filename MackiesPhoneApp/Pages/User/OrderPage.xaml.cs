using CommunityToolkit.Maui.Views;
using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Microsoft.Maui.Controls;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderPage : ContentPage
{
	TruckLocation _selectedTruckLocation;
    private OrderBasket _orderBasketService;
    private ToolbarItem _totalToolbarItem;
    private OrderItem _selectedItem;



    public OrderPage(TruckLocation selectedTruckLocation)
	{
        InitializeComponent();

        _selectedTruckLocation = selectedTruckLocation;

        _orderBasketService = ServiceHelper.GetService<OrderBasket>();

        _totalToolbarItem = new ToolbarItem
        {
            Text = $"Total: {_orderBasketService.OrderTotal:C}"
        };

        ToolbarItems.Add(_totalToolbarItem);

        _orderBasketService.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(OrderBasket.OrderTotal))
            {
                _totalToolbarItem.Text = $"Total: {_orderBasketService.OrderTotal:C}";
            }
        };

        Appearing += OrderPage_Appearing;
        InitializeAsync();
       
	}

    private async void OrderPage_Appearing(object? sender, EventArgs e)
    {

       // MackiesPhoneApp.Services.Products.SetIfOrderItemsIsInBasket();

    }

    private async void InitializeAsync()
    {
        var pizzaList = await MackiesPhoneApp.Services.Products.getPizzas();
        var toppingList = await MackiesPhoneApp.Services.Products.getToppings();

        var pizzaOrderItems = MackiesPhoneApp.Services.Products.MakePizzaOrderItems(pizzaList);
        var toppingOrderItems = MackiesPhoneApp.Services.Products.MakeToppingOrderItems(toppingList);

        var allOrderItems = pizzaOrderItems.Concat(toppingOrderItems).ToList();

        _orderBasketService.UpdateAllProductsItems(allOrderItems);


        //MackiesPhoneApp.Services.Products.SetAllOrderItems(allOrderItems);
        //  MackiesPhoneApp.Services.Products.SetIfOrderItemsIsInBasket();

        OrderItemsCollectionView.ItemsSource = allOrderItems;
    }

  
    private async void OnOrderItemTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Frame frame && frame.BindingContext is OrderItem selectedOrderItem)
        {
            _selectedItem = selectedOrderItem;
           if (! _orderBasketService.IsProductInBasket(selectedOrderItem.producttype, selectedOrderItem.productid ))
            {
                //var popup = new PopupOrderItemSelected(selectedOrderItem);
                //var result = await this.ShowPopupAsync(popup);
                //if (result is bool addedItem)
                //{
                //    if (addedItem)
                //    {
                //        MackiesPhoneApp.Services.Products.SetIfOrderItemsIsInBasket();
                //    }
                //}

                selectedOrderItem.IsQuantityEditable = true;
                selectedOrderItem.IsQuantityVisible = true;

            }

            else
            {
                Navigation.PushAsync(new OrderBasketPage());
            }
      
        }
    }

    private void OnImagePlusTapped(object? sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is OrderItem orderItem)
        {
            orderItem.quantity++;
        }
    }

    private void OnAddToBasketClicked(object sender, EventArgs e)
    {
        _orderBasketService.AddOrderItemToBasket(_selectedItem);
        _selectedItem.IsQuantityEditable = false;
        _selectedItem.IsQuantityVisible = true;
        _selectedItem.IsInBasket = true;
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

    private async void OnDetailsTapped(object sender, EventArgs e)
    {
        if (sender is Label detailLabel && detailLabel.BindingContext is OrderItem selectedOrderItem) 
        {
            var popup = new PopupDetailPage(selectedOrderItem);
            var result = await this.ShowPopupAsync(popup);
        }
    
    }

    private void OnGoToOrderBasket(object sender, EventArgs e)
    {
        // Navigate to your Checkout page 
        Navigation.PushAsync(new OrderBasketPage());
    }
}