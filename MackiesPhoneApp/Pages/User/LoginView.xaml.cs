using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

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
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            using var httpClient = new HttpClient();
         //   var response = await httpClient.PostAsync("https://api.mackies-pizza.dk/Login/login", content);

            var response = await httpClient.PostAsync("http://192.168.8.105:5000/Login/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseJson);

                if (!string.IsNullOrWhiteSpace(loginResponse?.Token))
                {
                    // Store the token securely
                    await SecureStorage.SetAsync("jwt_token", loginResponse.Token);

                    // Navigate to the main page
                    Application.Current.MainPage = new MainPage(); // Or use Shell navigation
                }
                else
                {
                    ErrorLabel.Text = "Invalid login response.";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                ErrorLabel.Text = "Login failed. Check your credentials.";
                ErrorLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error: {ex.Message}";
            ErrorLabel.IsVisible = true;
        }
    }
}
