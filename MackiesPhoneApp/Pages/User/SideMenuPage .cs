using MackiesPhoneApp.Pages.User;

public class SideMenuPage : ContentPage
{
    private readonly CustomFlyoutPage _parent;

   
    public SideMenuPage(CustomFlyoutPage parent)
    {
        _parent = parent;

        var homeBtn = new Button { Text = "Home" };
        homeBtn.Clicked += (s, e) => _parent.SetMainPage(new HomePage());

        var loginBtn = new Button { Text = "Login" };
        loginBtn.Clicked += (s, e) => _parent.SetMainPage(new LoginPage());

        var settingsBtn = new Button { Text = "Settings" };
        settingsBtn.Clicked += (s, e) => _parent.SetMainPage(new SettingsPage());

        Content = new StackLayout
        {
            Children = { homeBtn, loginBtn, settingsBtn }
        };

        Title = "Menu"; // Important for Android
    }
}
