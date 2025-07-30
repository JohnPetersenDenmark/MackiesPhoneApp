using MackiesPhoneApp.Models;

namespace MackiesPhoneApp.Pages.User;

public partial class PopupOrderItemSelected : CommunityToolkit.Maui.Views.Popup
{
    private readonly OrderItem _orderitem;

     

        public string? imageurl { get; set; }

    public event EventHandler<(OrderItem orderItem, int Quantity)> OrderItemAdded;

    public PopupOrderItemSelected(OrderItem orderItem)
	{
	
         InitializeComponent();

        _orderitem = orderItem;

        ProductImage.Source = orderItem.imageurl;

        ProductNameLabel.Text = orderItem.productname;
        ProducDescriptionLabel.Text = orderItem.productdescription;
        ProductPriceLabel.Text = $"Pris: {orderItem.unitprice:F2} DKK";
    }

    private void OnAddToBasketClicked(object sender, EventArgs e)
    {
        int quantity = (int)QuantityStepper.Value;
        OrderItemAdded?.Invoke(this, (_orderitem, quantity));
        Close();
    }
}