
using MackiesPhoneApp.Models;
using MackiesPhoneApp.Services;
using Newtonsoft.Json;
using System.Globalization;

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

            var filteredTruckLocations = filterTruckLocations(truckLocations);

            var sortedTruckLocations = sortTruckLocations(filteredTruckLocations);

            foreach (var truckLocation in sortedTruckLocations)
            {
                refineTruckLocation(truckLocation);
            }
            TruckLocationsCollectionView.ItemsSource = sortedTruckLocations;
        }
    }

    private async void OnTruckLocationTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Frame frame && frame.BindingContext is TruckLocation selectedTruckStop)
        {
            var orderBasketService = ServiceHelper.GetService<OrderBasket>();
            orderBasketService.order.LocationId = selectedTruckStop.Id;
            await Navigation.PushAsync(new OrderPage(selectedTruckStop));
        }
    }

    private void refineTruckLocation(TruckLocation truckLocation)
    {

        var danishCulture = new CultureInfo("da-DK");

        string startDateAsString = truckLocation.StartDateTime.Substring(0, 10);
        string endDateAsString = truckLocation.EndDateTime.Substring(0, 10);

        string startDateTimeAsString = truckLocation.StartDateTime.Substring(11, 5);
        string endDateTimeAsString = truckLocation.EndDateTime.Substring(11, 5);

        truckLocation.OpenCloseInterval = startDateTimeAsString + " - " + endDateTimeAsString;

        DateTime startDateTime = DateTime.ParseExact(truckLocation.StartDateTime, "dd-MM-yyyy HH:mm", danishCulture);
        DateTime endDateTime = DateTime.ParseExact(truckLocation.EndDateTime, "dd-MM-yyyy HH:mm", danishCulture);

        string weekday = startDateTime.ToString("dddd", danishCulture);
        weekday = weekday.ToUpper();
        string dayNumber = startDateTime.Day.ToString();
        string StartDateTimeMonth = startDateTime.ToString("MMMM", danishCulture);

        truckLocation.StartDateRefined = weekday + " " + dayNumber + ". " + StartDateTimeMonth;

        string EndDateTimeMonth = endDateTime.ToString("MMMM", danishCulture);
    }

    private List<TruckLocation> filterTruckLocations(List<TruckLocation> truckLocationsList)
    {
        var danishCulture = new CultureInfo("da-DK");
        var today = DateTime.Today;

        List<TruckLocation> upcomingLocations = new List<TruckLocation>();

         upcomingLocations = truckLocationsList
            .Where(truckLocation =>
            {
                // Try to parse the string into a DateTime
                if (DateTime.TryParseExact(truckLocation.StartDateTime, "dd-MM-yyyy HH:mm", danishCulture,
                                           DateTimeStyles.None, out var parsedDate))
                {
                    // Compare only the date part
                    return parsedDate.Date >= today;
                }

                // If parsing fails, exclude the item
                return false;
            })
            .ToList();

        return upcomingLocations;
    }

    private List<TruckLocation> sortTruckLocations(List<TruckLocation> truckLocationsList)
    {
        var danishCulture = new CultureInfo("da-DK");

        List<TruckLocation> sortedLocations = new List<TruckLocation>();
        sortedLocations = truckLocationsList
            .OrderBy(truckLocation =>
            {
                // Try to parse StartDateTime string
                if (DateTime.TryParseExact(truckLocation.StartDateTime, "dd-MM-yyyy HH:mm", danishCulture,
                                           DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate.Date; // Use only the date part
                }

                return DateTime.MaxValue; // Push unparseable items to the end
            })
            .ToList();

        return sortedLocations;
    }
    }