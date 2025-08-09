#if ANDROID
using MackiesPhoneApp.Pages.User;
using MackiesPhoneApp.Platforms.Android;
#endif

using System.Globalization;

namespace MackiesPhoneApp
{
    public partial class App : Application
    {
        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("da-DK");

            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());

#if ANDROID
            MainPage = new CustomFlyoutPageV2();
#endif


        }      
    }
}