using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class OrderItem
    {
        public int orderid { get; set; }

        public int productid { get; set; }

        public string productname { get; set; }

        public string pizzanumber { get; set; }

        public string imageurl { get; set; }

        public string productdescription { get; set; }

        public int quantity { get; set; }

        public bool selected { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? unitprice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? unitdiscountpercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? discountedunitprice { get; set; }

        public int producttype { get; set; }
    }
}
