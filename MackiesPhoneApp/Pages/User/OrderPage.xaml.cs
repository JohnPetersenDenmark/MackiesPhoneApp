using MackiesPhoneApp.Models;

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
        if (sender is Frame frame && frame.BindingContext is OrderItem selectedTruckStop)
        {
            var x = 1;

            // Navigate or handle logic here:
            //await Navigation.PushAsync(new OrderPage(selectedTruckStop));
        }
    }
}