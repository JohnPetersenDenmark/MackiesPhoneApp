using MackiesPhoneApp.Models;

namespace MackiesPhoneApp.Pages.User;

public partial class PopupOrderItemSelected : CommunityToolkit.Maui.Views.Popup
{
    private readonly OrderItem _orderitem;


    private int _quantity = 1;
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
        // int quantity = (int)QuantityStepper.Value;

        //int quantity = (int) QuantityLabel.Text
        //OrderItemAdded?.Invoke(this, (_orderitem, quantity));
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

    private void OnDecreaseClicked(object sender, EventArgs e)
    {
        if (_quantity > 1)
        {
            _quantity--;
            QuantityLabel.Text = _quantity.ToString();
        }
    }

    private void OnIncreaseClicked(object sender, EventArgs e)
    {
        _quantity++;
        QuantityLabel.Text = _quantity.ToString();
    }
}