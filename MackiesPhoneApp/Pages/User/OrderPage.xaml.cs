using MackiesPhoneApp.Models;

namespace MackiesPhoneApp.Pages.User;

public partial class OrderPage : ContentPage
{
	TruckLocation _selectedTruckLocation;

    public OrderPage(TruckLocation selectedTruckLocation)
	{
        _selectedTruckLocation = selectedTruckLocation;

        InitializeComponent();
	}
}