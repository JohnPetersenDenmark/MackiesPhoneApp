using MackiesPhoneApp.Pages.User;

using Microsoft.Maui.Controls;

namespace MackiesPhoneApp;

public partial class MainPage : ContentPage
{
    private bool _menuOpen = false;
    bool _isInitialized = false;

    public MainPage()
    {
        InitializeComponent();
        LoadView(new HomeView());
    }

    private async void ToggleMenu_Clicked(object sender, EventArgs e)
    {
        _menuOpen = !_menuOpen;

        if (_menuOpen)
        {
            FlyoutMenu.IsVisible = true;
            await FlyoutMenu.TranslateTo(0, 0, 200, Easing.CubicInOut);
        }
        else
        {
            await FlyoutMenu.TranslateTo(-250, 0, 200, Easing.CubicInOut);
            FlyoutMenu.IsVisible = false;
        }
    }

    private void LoadView(View view)
    {
        MainContent.Content = view;
    }

    private void GoHome_Clicked(object sender, EventArgs e)
    {
        LoadView(new HomeView());
        ToggleMenu_Clicked(null, null);
    }

    private void GoSettings_Clicked(object sender, EventArgs e)
    {
        LoadView(new SettingsView());
        ToggleMenu_Clicked(null, null);
    }

    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        ApplyTheme(e.RequestedTheme);
    }

    private void ApplyTheme(AppTheme theme)
    {

        setSystemBarsrColors(theme);
        setToolbarColors(theme);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!_isInitialized)
        {
            // Set system bar color initially
            ApplyTheme(App.Current.RequestedTheme);

            // Subscribe once to theme change
            App.Current.RequestedThemeChanged += OnRequestedThemeChanged;

            _isInitialized = true;
        }
    }

    private void setSystemBarsrColors(AppTheme theme)
    {
#if ANDROID
        var colorText = theme == AppTheme.Dark ? "AppSystemBarsBackgroundColorDark" : "AppSystemBarsBackgroundColorLight";

        if (Application.Current.Resources.TryGetValue(colorText, out var colorResource))
        {
            if (colorResource is Color backgroundColor)
            {
                MackiesPhoneApp.Platforms.Android.PlatformStatusBarHelper.SetSystemBarsColor(backgroundColor);
            }
        }
#endif
    }

    private void setToolbarColors(AppTheme theme)
    {
        // Since MainPage is inside NavigationPage, get parent NavigationPage
        if (this.Parent is NavigationPage navPage)
        {
            if (theme == AppTheme.Dark)
            {
                if (Application.Current.Resources.TryGetValue("AppToolbarBackgroundColorDark", out var colorBackgroundResource))
                {
                    if (colorBackgroundResource is Color backgroundColor)
                    {
                        navPage.BarBackgroundColor = backgroundColor;
                    }
                }

                if (Application.Current.Resources.TryGetValue("AppToolbarTextColorDark", out var colorTextResource))
                {
                    if (colorTextResource is Color textColor)
                    {
                        navPage.BarTextColor = textColor;
                    }
                }

             
            }
            else
            {
                if (Application.Current.Resources.TryGetValue("AppToolbarBackgroundColorLight", out var colorBackgroundResource))
                {
                    if (colorBackgroundResource is Color backgroundColor)
                    {
                        navPage.BarBackgroundColor = backgroundColor;
                    }
                }

                if (Application.Current.Resources.TryGetValue("AppToolbarTextColorLight", out var colorTextResource))
                {
                    if (colorTextResource is Color textColor)
                    {
                        navPage.BarTextColor = textColor;
                    }
                }


            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Best practice: unsubscribe to avoid memory leaks
        App.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
    }

  
}
