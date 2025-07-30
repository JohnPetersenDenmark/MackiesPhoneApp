using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public class Topping
    {
      
        public int Id { get; set; }
       
        public int ProductType { get; set; }
      
        public string Name { get; set; }
      
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }
       
        public Decimal Price { get; set; }


    }
}
