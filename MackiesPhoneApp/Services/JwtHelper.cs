using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Services
{
    public class JwtHelper
    {
        public static async Task<Dictionary<string, object>> GetJwtPayloadAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrEmpty(token))
                return null;

            var parts = token.Split('.');
            if (parts.Length != 3)
                throw new InvalidOperationException("Invalid JWT token");

            var payload = parts[1];

            // Pad base64 string if needed
            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            var bytes = Convert.FromBase64String(payload);
            var json = Encoding.UTF8.GetString(bytes);

            // Parse to dictionary
            var payloadData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            return payloadData;
        }
    }
}
