using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MackiesPhoneApp.Models
{
    public class ProductLabel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string LabelName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
