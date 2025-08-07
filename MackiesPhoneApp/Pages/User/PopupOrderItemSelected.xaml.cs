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
        Close();
    }

    private void OnImageMinusTapped(object sender, EventArgs e)
    {      
        if (_orderitem.quantity > 1)
        {
            _orderitem.quantity--;
        }
    }

    private void OnImagePlusTapped(object sender, EventArgs e)
    {
        _orderitem.quantity ++;
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}