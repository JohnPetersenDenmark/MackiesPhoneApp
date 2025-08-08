using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using MackiesPhoneApp.Services;

namespace MackiesPhoneApp.Pages.User;

public partial class LoginView : ContentView
{
	public LoginView()
	{
		InitializeComponent();
	}

    private class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    private class LoginResponse
    {
        public string Token { get; set; }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var usernameEntered = UsernameEntry.Text?.Trim();
        var passwordEntered = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(usernameEntered) || string.IsNullOrWhiteSpace(passwordEntered))
        {
            ErrorLabel.Text = "Please enter both username and password.";
            ErrorLabel.IsVisible = true;
            return;
        }

        var loginRequest = new LoginRequest
        {
            Username = usernameEntered,
            Password = passwordEntered
        };

        var json = JsonConvert.SerializeObject(loginRequest);
       

        try
        {
          
            var url =  "/Login/login";

            var response = await CustomHttpClient.postRequest(url, false, json, this);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SecureStorage.Default.Remove("jwt_token");

                LoggedInUser.resetUser();

                var responseJson = await response.Content.ReadAsStringAsync();

                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseJson);

                if (!string.IsNullOrWhiteSpace(loginResponse?.Token))
                {

                    await SecureStorage.SetAsync("jwt_token", loginResponse.Token);

                    var tokenDecrypted = await JwtHelper.GetJwtPayloadAsync();

                    LoggedInUser.setUserFromDecodedToken(tokenDecrypted);

                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    ErrorLabel.Text = "Invalid login response.";
                    ErrorLabel.IsVisible = true;
                }

            }

          
          
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error: {ex.Message}";
            ErrorLabel.IsVisible = true;
        }
    }
}
