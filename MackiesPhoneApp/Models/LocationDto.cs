using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class LocationDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("locationname")]
        public string LocationName { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }
}
