using Microsoft.AspNetCore.Mvc;
using OnLineShop.IServices;
using OnLineShop.Models;

namespace OnLineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserLoginController : Controller
    {
        private readonly IUserRegister _dal;
        public UserLoginController(IUserRegister dal)
        {
            _dal = dal;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserRegister user)
        {
          int result=   _dal.Login(user);
            if (result==1)
            {
                TempData["LoginSuccess"] = "Login Successfully";
                TempData["loginflag"] = "login";
                return RedirectToAction("Index", "Home");
            }
            TempData["LoginFailed"] = "Login failed";
            TempData["loginfailedflag"] = "loginfailed";
            return View();
        }

    }
}
