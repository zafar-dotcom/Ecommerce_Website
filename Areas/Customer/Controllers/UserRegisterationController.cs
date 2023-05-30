using Microsoft.AspNetCore.Mvc;
using OnLineShop.IServices;
using OnLineShop.Models;

namespace OnLineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserRegisterationController : Controller
    {
        private readonly IUserRegister _dal;
        public string value = "";
        public UserRegisterationController(IUserRegister dal)
        {
            _dal=dal;
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(UserRegister user)
        {
            if(HttpContext.Request.Method == "POST")
            {
              //ViewBag["result"] = 
                   bool result= _dal.InsertUSer(user);
                if (result == true)
                {
                    return RedirectToAction("Login", "UserLogin");
                }
            }
            return View();
        }

    }
}
