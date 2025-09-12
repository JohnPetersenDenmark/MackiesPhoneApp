using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public  class EmployeeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
