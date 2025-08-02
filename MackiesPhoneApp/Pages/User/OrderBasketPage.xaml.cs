
using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using System.Collections.ObjectModel;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderBasketPage : ContentPage
{

    public ObservableCollection<OrderItem> OrderBasketItems { get; set; }

    private readonly OrderBasket _orderBasketService;

    public OrderBasketPage()
	{
        InitializeComponent(); // ✅ Always first

        _orderBasketService = ServiceHelper.GetService<OrderBasket>();

        OrderBasketItems = _orderBasketService.OrderBasketItems;

        BindingContext = this; // ✅ Bind to the whole page so {Binding OrderBasketItems} works
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        OrderItemsList.ItemsSource = null;
        OrderItemsList.ItemsSource = _orderBasketService.OrderBasketItems;
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _orderBasketService.ClearBasket();
        OrderItemsList.ItemsSource = null;
        OrderItemsList.ItemsSource = _orderBasketService.OrderBasketItems;
    }

    private void OnImagePlusTapped(object? sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is OrderItem item)
        {
            item.quantity++;
        }
    }

    private void OnImageMinusTapped(object? sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is OrderItem item)
        {
            if (item.quantity > 1)
                item.quantity--;
            else
            {
                // remove if you want zero to drop the item
                if (BindingContext is OrderBasket orderBasket) // or your VM
                    orderBasket.OrderBasketItems.Remove(item);
                else if (OrderItemsList.ItemsSource is ObservableCollection<OrderItem> coll)
                    coll.Remove(item);
            }
        }
    }

}