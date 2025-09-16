using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int ProductType { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public string? Description { get; set; }
        public string? Details { get; set; }

        public string? ImageUrl { get; set; }

        public Decimal? Price { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public Decimal? DiscountPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? DiscountPrice { get; set; }

        [JsonPropertyName("productCategories")]
        public List<ProductCategoryDto>? ProductCategories { get; set; } = new();

        [JsonPropertyName("productTypes")]
        public List<ProductType>? ProductTypes { get; set; } = new();
    }
}

