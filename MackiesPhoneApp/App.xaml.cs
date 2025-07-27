#if ANDROID
using MackiesPhoneApp.Platforms.Android;
#endif

namespace MackiesPhoneApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());


        }      
    }
}