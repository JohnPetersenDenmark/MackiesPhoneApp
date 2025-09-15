using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class OrderItemDto
    {
        [JsonPropertyName("orderid")]
        public int OrderId { get; set; }

        [JsonPropertyName("producttype")]
        public int ProductType { get; set; }

        [JsonPropertyName("productid")]
        public int ProductId { get; set; }

        [JsonPropertyName("pizzanumber")]
        public string PizzaNumber { get; set; }

        [JsonPropertyName("productname")]
        public string ProductName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unitprice")]
        public decimal? UnitPrice { get; set; }

        [JsonPropertyName("unitdiscountpercentage")]
        public decimal? UnitDiscountPercentage { get; set; }

        [JsonPropertyName("discountedunitprice")]
        public decimal? DiscountedUnitPrice { get; set; }

        public List<ProductCategoryDto>? ProductCategories { get; set; }
    }
}
