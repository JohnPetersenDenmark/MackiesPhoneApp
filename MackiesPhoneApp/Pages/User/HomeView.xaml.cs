using GoogleGson;
using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Newtonsoft.Json;

namespace MackiesPhoneApp.Pages.User;

public partial class HomeView : ContentView
{

    public HomeView()
    {
        InitializeComponent();

        var loggedInUser = LoggedInUser.getUserDisplayName();

        LoggedInUserDisplayName.Text = loggedInUser;


        // Call async initializer without await (fire and forget)
        InitializeAsync();


    }

    private async void InitializeAsync()
    {
        var response = await CustomHttpClient.getRequest("/Home/truckcalendarlocationlist", false, this);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            List<TruckLocation> truckLocations = JsonConvert.DeserializeObject<List<TruckLocation>>(responseJson);
            foreach (var truckLocation in truckLocations)
            {

            }
            TruckLocationsCollectionView.ItemsSource = truckLocations;
        }
    }

    private async void OnTruckLocationTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Frame frame && frame.BindingContext is TruckLocation selectedTruckStop)
        {
            var x = 1;

            // Navigate or handle logic here:
            await Navigation.PushAsync(new OrderPage(selectedTruckStop));
        }
    }

    private void refineTruckLocation(TruckLocation truckLocation)
    {

    }
}