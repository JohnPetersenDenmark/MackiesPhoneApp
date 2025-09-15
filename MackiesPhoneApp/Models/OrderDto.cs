using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class OrderDto
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("customerorderCode")]
        public string? CustomerOrderCode { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string? Email { get; set; } = string.Empty;

        [JsonPropertyName("locationId")]
        public int LocationId { get; set; }

        [JsonPropertyName("locationname")]
        public string? LocationName { get; set; } = string.Empty;

        [JsonPropertyName("payeddatetime")]
        public DateTime? PayedDateTime { get; set; }

        [JsonPropertyName("createddatetime")]
        public DateTime? CreatedDateTime { get; set; }

        [JsonPropertyName("modifieddatetime")]
        public DateTime? ModifiedDateTime { get; set; }

        [JsonPropertyName("deliveryDate")]
        public DateTime? DeliveryDate { get; set; }

        [JsonPropertyName("locationstartdatetime")]
        public string? LocationStartDateTime { get; set; }

        [JsonPropertyName("locationenddatetime")]
        public string? LocationEndDateTime { get; set; }


        [JsonPropertyName("locationbeautifiedstartdatetime")]
        public string? LocationBeautifiedStartDateTime { get; set; }

        [JsonPropertyName("locationbeautifiedTimeInterval")]
        public string? LocationBeautifiedTimeInterval { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("orderlines")]
        public List<OrderItemDto>? OrderLines { get; set; } = new();

        [JsonPropertyName("templateScheduleId")]
        public int? TemplateScheduleId { get; set; }

        [JsonPropertyName("fishShopId")]
        public int? FishShopId { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("productCategories")]
        public List<ProductCategoryDto>? ProductCategories { get; set; } = new();
    }
}
