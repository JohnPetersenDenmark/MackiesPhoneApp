using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class TemplateScheduleDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }



        [JsonPropertyName("operationareaid")]
        public int OperationAreaId { get; set; }

        [JsonPropertyName("locationid")]
        public int LocationId { get; set; }

        [JsonPropertyName("locationname")]
        public string LocationName { get; set; }

        [JsonPropertyName("dayofweek")]
        public int DayOfWeek { get; set; } // 1=Mon ... 7=Sun

        [JsonPropertyName("starttime")]
        public string StartTime { get; set; }

        [JsonPropertyName("endtime")]
        public string EndTime { get; set; }

        [JsonPropertyName("shopOpenTimeInterval")]
        public string? ShopOpenTimeInterval { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("location")]
        public LocationDto? Location { get; set; }
    }
}

