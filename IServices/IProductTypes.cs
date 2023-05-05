using OnLineShop.Models;

namespace OnLineShop.IServices
{
    public interface IProductTypes
    {
        List<Product_Type> producttypelist();
         Task AddProductType(Product_Type producttype);
         Product_Type Product_TypeGetProductTypeById(int? id);
       Task UpdateProductType(Product_Type modl);
        Task DeleteProductType(Product_Type modl);
    }
}
