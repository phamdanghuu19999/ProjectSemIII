using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Controllers
{
    public class UserController : Controller
    {
        Respository<User> _u;

        // GET: User
        public UserController()
        {
            _u = new Respository<User>();
        }
        public ActionResult Index()
        {
            return View(_u.GetAll());
        }
        public ActionResult Details(int id)
        {
            return View(_u.Get(id));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserRegisterModel u)
        {
           
            if (ModelState.IsValid)
            {
                User us = new User();
                us.UserName = u.UserName;
                us.Password = u.Password;
                us.Email = u.Email;
                us.Address = u.Address;
                us.Password = u.Password;
                us.FullName = u.FullName;
                us.Status = true;
                us.GroupId = 2;


                _u.Add(us);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View(_u.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {
            _u.Edit(u);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _u.Remove(id);

            return RedirectToAction("Index");
        }
    }
}