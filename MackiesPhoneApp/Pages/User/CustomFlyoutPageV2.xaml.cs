using Microsoft.Maui.Controls;
using MackiesPhoneApp.Pages.User;
using MackiesPhoneApp.Services;
namespace MackiesPhoneApp.Pages.User
{
    public partial class CustomFlyoutPageV2 : FlyoutPage
    {
        private NavigationPage _navPage;
        private bool _isInitialized = false;

        public CustomFlyoutPageV2()
        {
            InitializeComponent();

            _navPage = Detail as NavigationPage;

          

            Appearing += CustomFlyoutPageV2_Appearing;
            Disappearing += CustomFlyoutPageV2_Disappearing;
        }
         

        private void CustomFlyoutPageV2_Disappearing(object? sender, EventArgs e)
        {
            base.OnDisappearing();
            App.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
        }

        private void CustomFlyoutPageV2_Appearing(object? sender, EventArgs e)
        {
            base.OnAppearing();

            if (!_isInitialized)
            {
                ApplyTheme(App.Current.RequestedTheme);
                App.Current.RequestedThemeChanged += OnRequestedThemeChanged;
                _isInitialized = true;
            }
        }

      
        // Navigation methods
        private void GoHome_Clicked(object sender, EventArgs e)
        {
            SetRootPage(new HomePage());
        }

        private void GoLogin_Clicked(object sender, EventArgs e)
        {
            SetRootPage(new LoginPage());
        }

        private void GoSettings_Clicked(object sender, EventArgs e)
        {
            SetRootPage(new SettingsPage());
        }

        private void GoTestPage_Clicked(object sender, EventArgs e)
        {
            SetRootPage(new TestPage());
        }
        

        private void SetRootPage(Page newRoot)
        {
            // Replace root without replacing NavigationPage
            _navPage.Navigation.InsertPageBefore(newRoot, _navPage.RootPage);
            _navPage.PopToRootAsync(false);

            IsPresented = false; // Close flyout
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
}
