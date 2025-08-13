using MackiesPhoneApp.Models;

namespace MackiesPhoneApp.Pages.User;

public partial class PopupDetailPage : CommunityToolkit.Maui.Views.Popup
{
	public PopupDetailPage(OrderItem orderItem)
	{
		InitializeComponent();

        BindingContext = orderItem;
    }
}