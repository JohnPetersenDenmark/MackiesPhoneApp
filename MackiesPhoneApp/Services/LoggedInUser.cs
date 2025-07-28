using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Services
{
    public static class LoggedInUser
    {

        private static Dictionary<string, object> decodedToken = null;


        public static async Task setUserFromDecodedToken(Dictionary<string, object> token)
        {
            decodedToken = token;
        }



        public static string getUserDisplayName()
        {
            if (decodedToken != null)
            {
                if (decodedToken.TryGetValue("displayname", out var userDisplaynameObj))
                {
                    string userDisplayname = userDisplaynameObj?.ToString();
                    return userDisplayname;
                }
            }

            return "";
        }

        public static string getUserName()
        {
            if (decodedToken != null)
            {
                if (decodedToken.TryGetValue("customusername", out var userNameObj))
                {
                    string userName = userNameObj?.ToString();
                    return userName;
                }
            }

            return "";
        }

        public static string getUserEmail()
        {
            if (decodedToken != null)
            {
                if (decodedToken.TryGetValue("customuseremail", out var userEmailObj))
                {
                    string userEmail = userEmailObj?.ToString();
                    return userEmail;
                }
            }

            return "";
        }

        public static void resetUser()
        {
            if (decodedToken != null)
            {
                decodedToken = null; ;
            }            
        }
    }
}
