using MackiesPhoneApp;
using MackiesPhoneApp.Pages.User;

public class CustomFlyoutPage : FlyoutPage
{
    bool _isInitialized = false;

    public CustomFlyoutPage()
    {
        Flyout = new SideMenuPage(this);
        Detail = new NavigationPage(new HomePage());

        Appearing += OnAppearing;
        Disappearing += OnDisappearing;
    }

    public void SetMainPage(Page page)
    {
        Detail = new NavigationPage(page);
        IsPresented = false; // Closes the menu
    }

    private void OnAppearing(object sender, EventArgs e)
    {
        if (!_isInitialized)
        {
            ApplyTheme(App.Current.RequestedTheme);
            App.Current.RequestedThemeChanged += OnRequestedThemeChanged;
            _isInitialized = true;
        }
    }

    private void OnDisappearing(object sender, EventArgs e)
    {
        App.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
    }

    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        ApplyTheme(e.RequestedTheme);
    }

    private void ApplyTheme(AppTheme theme)
    {
        SetSystemBarsColors(theme);
        SetToolbarColors(theme);
    }

    private void SetSystemBarsColors(AppTheme theme)
    {
#if ANDROID
        var colorText = theme == AppTheme.Dark ? "AppSystemBarsBackgroundColorDark" : "AppSystemBarsBackgroundColorLight";

        if (Application.Current.Resources.TryGetValue(colorText, out var colorResource) && colorResource is Color backgroundColor)
        {
            MackiesPhoneApp.Platforms.Android.PlatformStatusBarHelper.SetSystemBarsColor(backgroundColor);
        }
#endif
    }

    private void SetToolbarColors(AppTheme theme)
    {
        if (Detail is NavigationPage navPage)
        {
            if (theme == AppTheme.Dark)
            {
                if (Application.Current.Resources.TryGetValue("AppToolbarBackgroundColorDark", out var bg) && bg is Color bgColor)
                    navPage.BarBackgroundColor = bgColor;

                if (Application.Current.Resources.TryGetValue("AppToolbarTextColorDark", out var txt) && txt is Color txtColor)
                    navPage.BarTextColor = txtColor;
            }
            else
            {
                if (Application.Current.Resources.TryGetValue("AppToolbarBackgroundColorLight", out var bg) && bg is Color bgColor)
                    navPage.BarBackgroundColor = bgColor;

                if (Application.Current.Resources.TryGetValue("AppToolbarTextColorLight", out var txt) && txt is Color txtColor)
                    navPage.BarTextColor = txtColor;
            }
        }
    }
}
