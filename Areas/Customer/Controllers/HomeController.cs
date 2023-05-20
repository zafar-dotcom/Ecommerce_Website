using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnLineShop.IServices;
using OnLineShop.Models;
using OnLineShop.Utility;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace OnLineShop.Areas.Customer.Controllers
{
   
    [Area("Customer")]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _dal;

        public HomeController(ILogger<HomeController> logger,IProduct dal)
        {
            _logger = logger;
            _dal= dal;
        }

        public IActionResult Index()
        {
            List<Products> lst = _dal.ProductList(); 
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult Details(int id)
        {
            List<SelectListItem> ptype=_dal.GetProductTypesById(id);
            ViewBag.producttype = ptype;
            List<SelectListItem> stag = _dal.GetspecialtagById(id);
            ViewBag.stag = stag;
            Products product = _dal.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        [ActionName("Details")]
        public IActionResult ProductDetails(int id)
        {
            List<Products> prolist = new List<Products>();
            List<SelectListItem> ptype = _dal.GetProductTypesById(id);
            ViewBag.producttype = ptype;
            List<SelectListItem> stag = _dal.GetspecialtagById(id);
            ViewBag.stag = stag;
            if (id == null)
            {
                return NotFound();
            }
            Products product = _dal.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            prolist = HttpContext.Session.Get<List<Products>>("products");
            if (prolist == null)
            {
                prolist = new List<Products>();
            }
            prolist.Add(product);
            HttpContext.Session.Set("products", prolist);
            return View(product);
        }
        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Products> products= new List<Products>();
           products= HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product= products.FirstOrDefault(c=>c.Id==id);
                if(product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [ActionName("Remove")]
        public IActionResult RemovefromCart(int? id)
        {
            List<Products> products = new List<Products>();
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Cart));
        }
        //get method for cart
        public IActionResult Cart()
        {
            List<Products> products= new List<Products>();
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}