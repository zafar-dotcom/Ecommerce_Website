using System.ComponentModel.DataAnnotations;

namespace OnLineShop.Models
{
    public class Order
    {
        public Order()
        {
            Order_detail = new List<OrderDetail>();
        }
        public int Id { get; set; }
      
        [Display(Name="Order No")]
        public string OrderNo { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> Order_detail { get; set; }
    }
}
