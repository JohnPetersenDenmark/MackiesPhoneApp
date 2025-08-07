using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Services
{
    public static class Constants
    {

        private static string API_BASE_URL = "http://192.168.8.105:5000";

      //  private static string API_BASE_URL = "https://api.mackies-pizza.dk";

        public static string getApiBaseUrl()
        {
            return API_BASE_URL;
        }
    }
}
