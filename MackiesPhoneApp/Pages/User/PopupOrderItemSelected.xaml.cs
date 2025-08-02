using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;

namespace MackiesPhoneApp.Pages.User;

public partial class PopupOrderItemSelected : CommunityToolkit.Maui.Views.Popup
{
    private  OrderItem _orderitem;


    private int _quantity = 1;
    public string? imageurl { get; set; }
    private OrderBasket _orderBasketService;

   // public event EventHandler<(OrderItem orderItem, int Quantity)> OrderItemAdded;

    public PopupOrderItemSelected(OrderItem orderItem )
	{
       
    _orderBasketService = ServiceHelper.GetService<OrderBasket>();

         InitializeComponent();

        _orderitem = orderItem;

        ProductImage.Source = orderItem.imageurl;

        ProductNameLabel.Text = orderItem.productname;
        ProducDescriptionLabel.Text = orderItem.productdescription;
        ProductPriceLabel.Text = $"Pris: {orderItem.unitprice:F2} DKK";

    }

    private void OnAddToBasketClicked(object sender, EventArgs e)
    {
        _orderitem.quantity = _quantity;
         _orderBasketService.AddToBasket(_orderitem);
        Close();
    }

    private void OnImageMinusTapped(object sender, EventArgs e)
    {
        if (_quantity > 1)
        {
            _quantity--;
            QuantityLabel.Text = _quantity.ToString();
        }
    }

    private void OnImagePlusTapped(object sender, EventArgs e)
    {
        _quantity++;
        QuantityLabel.Text = _quantity.ToString();
    }

   
}