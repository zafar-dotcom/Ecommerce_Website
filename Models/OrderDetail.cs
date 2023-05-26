using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnLineShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Display(Name ="Order")]
        public int OrderId { get; set; }
        [Display(Name ="Product")]
        public int ProductId { get; set; }

        
        public Order Order { get; set; }
        
        public Products Products { get; set; }
    }
}
