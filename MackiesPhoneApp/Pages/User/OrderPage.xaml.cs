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
       
        InitializeAsync();
        InitializeComponent();
	}

    private async void InitializeAsync()
    {
        var pizzaList = await MackiesPhoneApp.Services.Products.getPizzas();
        var toppingList = await MackiesPhoneApp.Services.Products.getToppings();

        var pizzaOrderItems =  MackiesPhoneApp.Services.Products.MakePizzaOrderItems(pizzaList);
        var toppingOrderItems = MackiesPhoneApp.Services.Products.MakeToppingOrderItems(toppingList);

        var allOrderItems = pizzaOrderItems.Concat(toppingOrderItems).ToList();

        OrderItemsCollectionView.ItemsSource = allOrderItems;
    }

    private async void OnOrderItemTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Frame frame && frame.BindingContext is OrderItem selectedOrderItemp)
        {
            var popup = new PopupOrderItemSelected(selectedOrderItemp);
            popup.OrderItemAdded += OnOrderItemAddedToBasket;
            this.ShowPopup(popup);
         //   MyCollectionView.SelectedItem = null;
            
        }
    }

    private void OnOrderItemAddedToBasket(object sender, (OrderItem OrderItem, int Quantity) e)
    {
        var (orderItem, quantity) = e;
        // Add to basket logic here
        // e.g. BasketService.Add(pizza, quantity);

      


      //  Console.WriteLine($"Added {quantity} of {pizza.Name} to basket.");
    }

    private void OnGoToOrderBasket(object sender, EventArgs e)
    {
        // Navigate to your Checkout page or process order
        Navigation.PushAsync(new OrderBasketPage());
    }
}