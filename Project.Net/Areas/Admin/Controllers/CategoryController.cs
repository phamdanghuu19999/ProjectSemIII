using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
   
    public class CategoryController : Controller
    {
        Respository<Category> _cat;
        Respository<Product> _pro;
        public CategoryController()
        {
            _cat = new Respository<Category>();
            _pro = new Respository<Product>();
        }
        // GET: Admin/Category
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Index(string table_search)
        {
            var data = _cat.GetAll().AsQueryable();
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.CategoryName.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }

        // GET: Admin/Category/Details/5
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Details(int id)
        {
            return View(_cat.Get(id));
        }

        // GET: Admin/Category/Create
        [CustomAuthAttributes(Roles = "ADD")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [CustomAuthAttributes(Roles = "ADD")]
        [HttpPost]
        public ActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {
                if (_cat.Add(c))
                {
                    return RedirectToAction("Index");
                    
                }
                return View();
            }
            return View();
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _cat.GetAll().AsQueryable().FirstOrDefault(x => x.CategoryId == id);
            return View(data);
        }

        // POST: Admin/Category/Edit/5
        [CustomAuthAttributes(Roles = "EDIT")]
        [HttpPost]
        public ActionResult Edit(Category c)
        {
            if (ModelState.IsValid)
            {
                if (_cat.Edit(c))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(c);
                }
            }
            return View(c);
        }

        [CustomAuthAttributes(Roles = "DELETE")]
        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (_pro.GetAll().Any(x=>x.CategoryId==id))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-danger",
                        Message = "Fail! Category has products"
					};
                    return RedirectToAction("Index");
                }
                _cat.Remove(id);
                TempData["msg"] = new ResponseMessage()
                {
                    Type = "alert-success",
                    Message = "Delete successfully!"
                };
            }
            catch (Exception ex)
            {

                TempData["msg"] = new ResponseMessage()
                {
                    Type = "alert-danger",
                    Message = ex.Message
                };
            }
            return RedirectToAction("Index");
            
        }

       
       
    }
}
