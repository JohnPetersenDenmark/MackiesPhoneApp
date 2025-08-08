using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MackiesPhoneApp.Pages.User
{
    public  class SettingsPage : ContentPage
    {

        public SettingsPage() {
            Title = "Indstilinger";

            Content = new SettingsView(); // Use your existing ContentView
        }   
    }
}
