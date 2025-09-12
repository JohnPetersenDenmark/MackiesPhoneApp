using CommunityToolkit.Maui.Views;
using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Microsoft.Maui.Controls;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderPage : ContentPage
{
    TemplateScheduleDto _selectedLocation;
    private OrderBasket _orderBasketService;
    private ToolbarItem _totalToolbarItem;
    private OrderItem _selectedItem;

    private List<OrderItem> allOrderItems;



    public OrderPage(TemplateScheduleDto selectedLocation)
	{
        InitializeComponent();

        _selectedLocation = selectedLocation;

        _orderBasketService = ServiceHelper.GetService<OrderBasket>();

        var grid = SetNavigationBarPageTitle.SetContentLogoAndTotal(this);
        NavigationPage.SetTitleView(this, grid);

        BindingContext = ServiceHelper.GetService<OrderBasket>();       
        InitializeAsync();
       
	}
   
    private async void InitializeAsync()
    {
        var productList = await MackiesPhoneApp.Services.Products.getProducts();

        allOrderItems = MackiesPhoneApp.Services.Products.MakeProductItems(productList);
             
        _orderBasketService.UpdateAllProductsItems(allOrderItems);

        OrderItemsCollectionView.ItemsSource = allOrderItems;
    }

  
    private async void OnOrderItemTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is OrderItem selectedOrderItem)
        {
            _selectedItem = selectedOrderItem;

           if (! _orderBasketService.IsProductInBasket(selectedOrderItem.producttype, selectedOrderItem.productid ))
            {             
                foreach(var orderItem in allOrderItems)
                {
                    orderItem.IsQuantityEditable = false;
                }             
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
                orderItem.quantity = 1;    
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
}