using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnLineShop.Models
{
    public class Product_Type
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public string Producttype { get; set; }
    }
}
