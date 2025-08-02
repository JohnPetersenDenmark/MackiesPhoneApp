using MackiesPhoneApp.Services;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderBasketPage : ContentPage
{

	private readonly OrderBasket _orderBasketService;

    public OrderBasketPage()
	{
        _orderBasketService = ServiceHelper.GetService<OrderBasket>();

        BindingContext = _orderBasketService.OrderBasketItems;

        InitializeComponent();
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

}