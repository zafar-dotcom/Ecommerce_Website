using System.ComponentModel.DataAnnotations;

namespace OnLineShop.Models
{
    public class Products
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Product Color")]                                                                                                    
        public string ProductColor { get; set; }
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }
        public Product_Type ProductType_prop { get; set; }
        [Display(Name ="Product type")]
        public int ProductTypeId { get; set; }
        public SpecialTag_model  SpecialTag_prop { get; set; }
        [Display(Name ="Special tag")]
        public int SpecialTagId { get; set; }
        
    }
}
