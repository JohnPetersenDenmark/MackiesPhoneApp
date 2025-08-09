#if ANDROID
using MackiesPhoneApp.Pages.User;
using MackiesPhoneApp.Platforms.Android;
#endif

namespace MackiesPhoneApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());

#if ANDROID
            MainPage = new CustomFlyoutPageV2();
#endif


        }      
    }
}