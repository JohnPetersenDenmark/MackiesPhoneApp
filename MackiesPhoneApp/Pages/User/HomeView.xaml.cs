using MackiesPhoneApp.Services;

namespace MackiesPhoneApp.Pages.User;

public partial class HomeView : ContentView
{
	public HomeView()
	{
		InitializeComponent();

		var loggedInUser = LoggedInUser.getUserDisplayName();

        LoggedInUserDisplayName.Text = loggedInUser;

    }
}