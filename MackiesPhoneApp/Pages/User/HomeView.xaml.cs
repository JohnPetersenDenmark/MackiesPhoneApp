
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
        var response = await CustomHttpClient.getRequest("/Admin/fishshoplistSchedules", false, this);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            List<FishShopDtoDetailed> fishShops = JsonConvert.DeserializeObject<List<FishShopDtoDetailed>>(responseJson);

            //var filteredTruckLocations = filterTruckLocations(truckLocations);

            //var sortedTruckLocations = sortTruckLocations(filteredTruckLocations);

            //foreach (var truckLocation in sortedTruckLocations)
            //{
            //    refineTruckLocation(truckLocation);
            //}

            foreach ( var fishShop in fishShops)
            {
                foreach ( var templateSchedule in fishShop.Area.TemplateSchedules)
                {
                    templateSchedule.ShopOpenTimeInterval = "i tidsrummet " + templateSchedule.StartTime + " - " + templateSchedule.EndTime;
                }
                //  _orderBasketService.order.TemplateSchedule.ShopOpenTimeInterval = "I tidsrummet " + _orderBasketService.order.TemplateSchedule.StartTime + " - " + _orderBasketService.order.TemplateSchedule.EndTime;
            }
            FishShopCollectionView.ItemsSource = fishShops;
        }
    }

    private async void  OnShowNoShowLocationsTapped(object sender, EventArgs e)
    {
        if (sender is Border border)
        {
            if (border != null)
            {
                var fishShop = GetParentFishShop(border);

                // If the content is a Label
                var label = border.Content as Label;
                if (label != null)
                {
                    if (fishShop.IsVisibleTemplateSchedule)
                    {
                        fishShop.IsVisibleTemplateSchedule = false;
                         label.Text = "Vis lokationer";

                        border.BackgroundColor = Color.FromArgb("#FFFFFF");
                        label.TextColor = Color.FromArgb("#000000");
                    }
                    else
                    {
                        fishShop.IsVisibleTemplateSchedule = true;
                         label.Text = "Gem lokationer";

                      border.BackgroundColor = Color.FromArgb("#5470a9");

                        label.TextColor =  Color.FromArgb("#FFFFFF");
                    }
                }
            }
        }          
    }

    private async void OnToggleShowContactTapped(object sender, EventArgs e)
    {
        if (sender is Border border)
        {
            if (border != null)
            {
                var fishShop = GetParentFishShop(border);

                // If the content is a Label
                var label = border.Content as Label;
                if (label != null)
                {
                    if (fishShop.IsVisibleContactInfo)
                    {
                        fishShop.IsVisibleContactInfo = false;
                        label.Text = "Vis kontakt";

                        border.BackgroundColor = Color.FromArgb("#FFFFFF");
                        label.TextColor = Color.FromArgb("#000000");
                    }
                    else
                    {
                        fishShop.IsVisibleContactInfo = true;
                        label.Text = "Gem kontakt";

                        border.BackgroundColor = Color.FromArgb("#5470a9");

                        label.TextColor = Color.FromArgb("#FFFFFF");
                    }
                }
            }
        }
    }

    private async void OnTruckLocationTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item

        //if (sender is Frame frame && frame.BindingContext is TemplateScheduleDto templateSchedule)
        //{
            if (sender is Border border)
            {
                // The BindingContext here is TemplateSchedule
                var tappedSchedule = border.BindingContext as TemplateScheduleDto;

                // Walk up until you find the parent FishShop
                var parentFishShop = GetParentFishShop(border);

                if (tappedSchedule != null && parentFishShop != null)
                {
                    var orderBasketService = ServiceHelper.GetService<OrderBasket>();
                    orderBasketService.order.TemplateSchedule = tappedSchedule;
                    orderBasketService.order.FishShop = parentFishShop;
                await Navigation.PushAsync(new OrderPage(tappedSchedule));
                }
            //}

           
        }
       
    }

    private FishShopDtoDetailed GetParentFishShop(Element element)
    {
        var parent = element.Parent;
        while (parent != null)
        {
            if (parent.BindingContext is FishShopDtoDetailed shop)
                return shop;

            parent = parent.Parent;
        }
        return null;
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