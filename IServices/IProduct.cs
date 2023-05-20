using Microsoft.AspNetCore.Mvc.Rendering;
using OnLineShop.Models;
using System.Threading.Tasks;

namespace OnLineShop.IServices
{
    public interface IProduct
    {
        List<Products> ProductList();
        Task AddProducts(Products product);
        Products GetProductById(int id);
        Task<bool> UpdateProduct(Products modl);
        bool DeleteProduct(int id);
        List<SelectListItem> GetProductTypes();
        List<SelectListItem> Getspecialtag();
        List<SelectListItem> GetspecialtagById(int id);
        List<SelectListItem> GetProductTypesById(int id);
        Task<List<Products>> ProductPriceRange(decimal? loweramount, decimal? highamount);

    }
}
