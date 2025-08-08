using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Pages.User
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Title = "Home";

            Content = new HomeView(); // Use your existing ContentView
        }
    }
}
