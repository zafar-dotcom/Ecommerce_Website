using Microsoft.AspNetCore.Mvc;
using OnLineShop.IServices;
using OnLineShop.Models;

namespace OnLineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private readonly ISpecialTag _dal;
        public SpecialTagController(ISpecialTag dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            var list = _dal.SpecialTagList();

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
        public async Task<IActionResult> Create(SpecialTag_model model)
        {
            if (ModelState.IsValid)
            {
                await _dal.AddSpecialTag(model);
                TempData["sptagcreated"] = "Special tag has been saved";
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
            var producttype = _dal.Get_specialTag(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag_model model)
        {
            if (ModelState.IsValid)
            {
                await _dal.UpdateSpecialTag(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _dal.Get_specialTag(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag_model model)
        {
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var producttype = _dal.Get_specialTag(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(SpecialTag_model modl)
        {
            if (ModelState.IsValid)
            {
                await _dal.DeleteSpecialTag(modl);
                TempData["delmsg"] = "Deleted Successfully";
                TempData["delalert"] = "deletetag";
                return RedirectToAction(nameof(Index));
            }
            TempData["delfail"] = "Delete failed";
            return View(modl);

        }

    }
}
