using MackiesPhoneApp.Models;
using System.Net.Http;

namespace MackiesPhoneApp.Pages.User;

public partial class PopupDetailPage : CommunityToolkit.Maui.Views.Popup
{
	public PopupDetailPage(OrderItem orderItem)
	{
		InitializeComponent();

        ProductDetailsWebView.Source = new HtmlWebViewSource
         {
             Html = orderItem.details
        };

        BindingContext = orderItem;
    }
}