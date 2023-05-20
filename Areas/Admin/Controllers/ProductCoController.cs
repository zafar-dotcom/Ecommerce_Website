using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnLineShop.IServices;
using OnLineShop.Models;
using Microsoft.AspNetCore.Hosting;



namespace OnLineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCoController : Controller
    {
        private readonly IProduct _dal;
        public IWebHostEnvironment _he;
        public ProductCoController(IProduct dal, IWebHostEnvironment hee)
        {
            _dal = dal;           
            _he=hee;
        }
        public IActionResult Index()
        {
            var lst = _dal.ProductList();
            return View(lst);
        }

        // filter thins based on price
        [HttpPost]
        public async Task<IActionResult> Index(decimal? loweramount,decimal? higheramount)
        {
            if(loweramount==null || higheramount == null)
            {
                var list = _dal.ProductList();
                return View(list);
            }
            var lst =await _dal.ProductPriceRange(loweramount, higheramount);
            return View(lst);

        }
        //get product method
        public IActionResult Create()
        {
            // ViewBag["producttypeid"] = new SelectList(_dal.GetProductTypes(), "Id", "producttype");
           //  ViewBag["specialtagid"] = new SelectList(_dal.Getspecialtag(), "tag_id", "specialtag");
           List<SelectListItem> producttype=_dal.GetProductTypes();
            ViewBag.producttype = producttype;
            List<SelectListItem> sptag = _dal.Getspecialtag();
            ViewBag.sptag = sptag;
            return View();
        }
        


        //post product method
        [HttpPost]
        public async Task<IActionResult> Create(Products product,IFormFile image)
        {
            
            
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image="Images/"+image.FileName;
                }
            if (image == null)
            {
                product.Image = "Images/Noimage.png";
            }
            TempData["product_created"] = "Product created successfully";
            TempData["createalert"] = "created";
               await _dal.AddProducts(product);
                return RedirectToAction("Index");
            
            
        }
        public IActionResult Edit(int id)
        {
          List<SelectListItem> prpducttypetpedit= _dal.GetProductTypes();
            ViewBag.prpducttypedit=prpducttypetpedit;
            List<SelectListItem> stagid = _dal.Getspecialtag();
            ViewBag.sptagedit = prpducttypetpedit;
            if (id == null)
            {
                return NotFound();
            }
            Products obj = _dal.GetProductById(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Products product ,IFormFile image)
        {
            if (image != null)
            {
                var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                product.Image = "Images/" + image.FileName;
            }
            if (image == null)
            {
                product.Image = "Images/Noimage.png";
            }
           bool result= await _dal.UpdateProduct(product);
            if (result == true)
            {
                TempData["updated"] = "Product update successfully";
                TempData["updatealert"] = "updated";
                return RedirectToAction("Index");
            }
            else
                TempData["updatedfail"] = " Update failed ! try again";
            TempData["update_fail_alert"] = "updatefailed";
            return View(product);
        }

        public IActionResult Details(int id)
        {
            List<SelectListItem> producttype = _dal.GetProductTypesById(id);  
            ViewBag.producttype = producttype;
            List<SelectListItem> sptag = _dal.GetspecialtagById(id);
            ViewBag.sptag = sptag;
            if (id == null)
            {
                return NotFound();
            }
           
            Products product= _dal.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
           List<SelectListItem> ptype= _dal.GetProductTypesById(id);
            ViewBag.ptype = ptype;
            List<SelectListItem> stg=_dal.GetspecialtagById(id);
            ViewBag.stg = stg;
            Products product = _dal.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Deleteproduct(int id)
        {
           bool res= _dal.DeleteProduct(id);
            if (res == true)
            {
                TempData["deleted"] = "Deleted Successfully";
                TempData["delsuccalert"] = "true";
                return RedirectToAction("Index");
            }
            TempData["deletefail"] = "delete failed";
            TempData["delfailalert"] = "fail";
            return RedirectToAction("Index");

        }
    }
}
