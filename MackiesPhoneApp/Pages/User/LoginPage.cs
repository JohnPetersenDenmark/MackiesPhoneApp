using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MackiesPhoneApp.Pages.User
{
    public  class LoginPage :  ContentPage
    {
        public LoginPage()
        {
            Title = "Home";

            Content = new LoginView(); // Use your existing ContentView
        }

    }
}
