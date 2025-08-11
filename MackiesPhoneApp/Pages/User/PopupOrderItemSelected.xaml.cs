using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using System.ComponentModel;

namespace MackiesPhoneApp.Pages.User;

public partial class PopupOrderItemSelected : CommunityToolkit.Maui.Views.Popup, INotifyPropertyChanged
{
    private OrderItem _orderitem;

    private bool _isActionAllowed = true;

    public bool IsActionAllowed
    {
        get => _isActionAllowed;
        set
        {
            if (_isActionAllowed != value)
            {
                _isActionAllowed = value;
                OnPropertyChanged(nameof(IsActionAllowed));
            }
        }
    }


    public string? imageurl { get; set; }
    private OrderBasket _orderBasketService;

    public PopupOrderItemSelected(OrderItem orderItem)
    {

        _orderBasketService = ServiceHelper.GetService<OrderBasket>();

        InitializeComponent();

        _orderitem = orderItem;

        if (_orderBasketService.order.OrderItemsList.Any(item => item.productid == orderItem.productid))
        {
            IsActionAllowed = false;
        }
        else
        {
            IsActionAllowed = true;
        }

           
        BindingContext = _orderitem;
    }

    private void OnAddToBasketClicked(object sender, EventArgs e)
    {
        _orderBasketService.AddOrderItemToBasket(_orderitem);
        Close(true);
    }

    private async void OnImageMinusTapped(object sender, EventArgs e)
    {
        await PlusSignImage.ScaleTo(1.0, 100);
        await MinusSignImage.ScaleTo(2.0, 100);
        if (_orderitem.quantity > 1)
        {
            _orderitem.quantity--;
        }
    }

    private async void OnImagePlusTapped(object sender, EventArgs e)
    {
        //PlusSignImage.RotateTo(360, 400, Easing.CubicInOut); // Spin 1 full rotation
        //PlusSignImage.Rotation = 0; // Reset rotation so it can spin again next time
        await PlusSignImage.ScaleTo(2.0, 100);
        await MinusSignImage.ScaleTo(1.0, 100);
        //  PlusSignImage.ScaleTo(1.0, 100);


        _orderitem.quantity ++;
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}