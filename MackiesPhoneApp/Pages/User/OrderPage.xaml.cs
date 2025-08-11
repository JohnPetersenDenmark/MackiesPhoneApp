using CommunityToolkit.Maui.Views;
using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderPage : ContentPage
{
	TruckLocation _selectedTruckLocation;
    

    public OrderPage(TruckLocation selectedTruckLocation)
	{
        _selectedTruckLocation = selectedTruckLocation;

        Appearing += OrderPage_Appearing;
        InitializeAsync();
        InitializeComponent();
	}

    private async void OrderPage_Appearing(object? sender, EventArgs e)
    {

        MackiesPhoneApp.Services.Products.SetIfOrderItemsIsInBasket();

    }

    private async void InitializeAsync()
    {
        var pizzaList = await MackiesPhoneApp.Services.Products.getPizzas();
        var toppingList = await MackiesPhoneApp.Services.Products.getToppings();

        var pizzaOrderItems = MackiesPhoneApp.Services.Products.MakePizzaOrderItems(pizzaList);
        var toppingOrderItems = MackiesPhoneApp.Services.Products.MakeToppingOrderItems(toppingList);

        var allOrderItems = pizzaOrderItems.Concat(toppingOrderItems).ToList();

        MackiesPhoneApp.Services.Products.SetAllOrderItems(allOrderItems);
        MackiesPhoneApp.Services.Products.SetIfOrderItemsIsInBasket();

        OrderItemsCollectionView.ItemsSource = allOrderItems;
    }

  
    private async void OnOrderItemTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Frame frame && frame.BindingContext is OrderItem selectedOrderItem)
        {
            var popup = new PopupOrderItemSelected(selectedOrderItem);        
           var result = await  this.ShowPopupAsync(popup);  
            if (result is bool addedItem)
            {
                if (addedItem)
                {
                    MackiesPhoneApp.Services.Products.SetIfOrderItemsIsInBasket();
                }
            }
        }
    }
  
    private void OnGoToOrderBasket(object sender, EventArgs e)
    {
        // Navigate to your Checkout page 
        Navigation.PushAsync(new OrderBasketPage());
    }
}