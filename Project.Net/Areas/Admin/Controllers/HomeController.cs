using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Respository<User> _user;
        public HomeController()
        {
            _user = new Respository<User>();
        }
        // GET: Admin/Home
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin u)
        {
            if (ModelState.IsValid)
            {
                
                var _u = _user.SingleBy(x => x.UserName == u.UserName && x.Password == u.Password);
                if (_u != null) // Đăng nhập thành công!
                {
                    Session["User"] = _u;
                   
                    return RedirectToAction("Index");
                }
                return View();
            }
            else
            {
                ModelState.AddModelError(String.Empty,"Sai tài khoản hoặc mật khẩu");
            }
            return View();
        }

        public ActionResult Logout()

        {
            Session.Remove("User");
            return RedirectToAction("Login");
        }

    }
    

}