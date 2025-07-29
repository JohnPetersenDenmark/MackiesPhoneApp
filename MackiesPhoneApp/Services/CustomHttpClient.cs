
using MackiesPhoneApp.Pages.User;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Services
{
    public class CustomHttpClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private static readonly string _apiBaseUrl = Constants.getApiBaseUrl();


        public CustomHttpClient() {
           
        }

        public static async Task<HttpResponseMessage> getRequest (string partialUrl, Boolean useToken , ContentView currentView)
        {           
            string fullUrl = _apiBaseUrl + partialUrl;

            HttpResponseMessage response = null;

            if (!useToken)
            {
                 response = await _httpClient.GetAsync(fullUrl);                             
            }
            else
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);                
                response = await _httpClient.SendAsync(request);                
            }

            response = await checkResponseForUauthorized(response, useToken, currentView);
            return response;
        }

        public static async Task<HttpResponseMessage> postRequest(string partialUrl, Boolean useToken, string content, ContentView currentView)
        {
            HttpContent jsonString = new StringContent(content, Encoding.UTF8, "application/json");

            string fullUrl = _apiBaseUrl + partialUrl;
            HttpResponseMessage response = null;

            try
            {
                if (!useToken)
                {
                    response = await _httpClient.PostAsync(fullUrl, jsonString);                    
                }

                else
                {
                    var token = await SecureStorage.GetAsync("jwt_token");
                    var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
                    request.Content = jsonString;
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    response = await _httpClient.SendAsync(request);
                }
                                                 
            } catch (Exception e)
            {
                var x = 1;
            }

            response = await checkResponseForUauthorized(response, useToken, currentView);
            return response;
        }

        public static async Task<HttpResponseMessage> deleteRequest(string partialUrl, Boolean useToken, ContentView currentView)
        {
           
            string fullUrl = _apiBaseUrl + partialUrl;

            HttpResponseMessage response = null;

            if (!useToken)
            {
                response = await _httpClient.DeleteAsync(fullUrl);
                return response;
            }
            else
            {

                var token = await SecureStorage.GetAsync("jwt_token");

                var request = new HttpRequestMessage(HttpMethod.Delete, fullUrl);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                response = await _httpClient.SendAsync(request);
            }

             response = await checkResponseForUauthorized(response, useToken,  currentView);
            return response;
        }

        private static async Task<HttpResponseMessage> checkResponseForUauthorized(HttpResponseMessage response , Boolean useToken, ContentView currentView)
        {
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return response;
            }

            if (useToken)
            {

                var currentPage = new ContentPage
                {
                    Content = currentView
                };

                var loginPage = new ContentPage
                {
                    Content = new LoginView()
                };

                await currentPage.Navigation.PushAsync(loginPage);
            }
            else
            {
               // await currentPage.Navigation.PushAsync(new MainPage());
            }
               
            return response;


        }
    }
}
