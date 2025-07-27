using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;


namespace MackiesPhoneApp.Platforms.Android
{
    public static class PlatformStatusBarHelper
    {
      
         public static void SetSystemBarsColor(Color color)
        {
           // var window = MainActivity.Instance?.Window;
            var activity = Platform.CurrentActivity;
            var window = activity?.Window;

            if (window == null)
                return;

            var platformColor = color.ToPlatform();

            // Set status bar and navigation bar colors
            window.SetStatusBarColor(platformColor);
            window.SetNavigationBarColor(platformColor);

           
            // Adjust icon color based on brightness
            bool isLight = (color.Red + color.Green + color.Blue) > 1.5;
            var flags = window.DecorView.SystemUiVisibility;

            if (isLight)
                flags |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            else
                flags &= ~(StatusBarVisibility)SystemUiFlags.LightStatusBar;

            window.DecorView.SystemUiVisibility = flags;
        }
    }
}

