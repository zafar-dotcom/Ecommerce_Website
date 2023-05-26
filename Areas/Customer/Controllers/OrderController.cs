using Microsoft.AspNetCore.Mvc;
using OnLineShop.IServices;
using OnLineShop.Models;
using OnLineShop.Utility;

namespace OnLineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IOrder _dal;
        public OrderController(IOrder dal)
        {
            _dal = dal;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public IActionResult ChekcoutProduct(Order oder) 
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                foreach(var product in products)
                { OrderDetail orderobj=new OrderDetail();
                    orderobj.ProductId = product.Id;
                    oder.Order_detail.Add(orderobj);

                }
                

            }
            oder.OrderNo = GetOrderNo();
            _dal.InsertOrder(oder);
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }
        public string GetOrderNo()
        {
          int rowcount=  _dal.GetOrderCount()+1;
            return rowcount.ToString("000");
        }
    }
}
