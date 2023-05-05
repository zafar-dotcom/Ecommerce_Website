using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnLineShop.IServices;
using OnLineShop.Models;

namespace OnLineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly IProductTypes _dal;
        public ProductTypesController(IProductTypes dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            var list = _dal.producttypelist();

            return View(list);
        }

        // create Product type action method 
        public IActionResult Create()
        {
            return View();
        }

        //pull values from above create viewa nd throw as argument in following create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product_Type model)
        {
            if (ModelState.IsValid)
            {
              await _dal.AddProductType(model);
                TempData["product_type_created"] = "Product type has been saved";
                TempData["showalert"] = true;
                return RedirectToAction(nameof(Index));
                
            }
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _dal.Product_TypeGetProductTypeById(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product_Type model)
        {
            if (ModelState.IsValid)
            {
               await _dal.UpdateProductType(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _dal.Product_TypeGetProductTypeById(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Product_Type model)
        {
                return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var producttype = _dal.Product_TypeGetProductTypeById(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Product_Type modl)
        {
            if (ModelState.IsValid)
            {
               await _dal.DeleteProductType(modl);
               TempData["delmsg"] = "Deleted Successfully";
                TempData["delalert"] = "deletetag";
                return RedirectToAction(nameof(Index));
            }
            TempData["delfail"] = "Delete failed";
            return View(modl);

        }
    }
}
