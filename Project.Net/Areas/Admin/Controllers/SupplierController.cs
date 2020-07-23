using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    
    public class SupplierController : Controller
    {
        Respository<Supplier> _sup;
		Respository<Product> _pro;
        public SupplierController()
        {
            _sup = new Respository<Supplier>();
        }
        // GET: Admin/Supplier
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Index(string table_search)
        {
            var data = _sup.GetAll().AsQueryable();
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.SupplierName.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Details(int id)
        {
            return View(_sup.Get(id));
        }
        [CustomAuthAttributes(Roles = "ADD")]
        public ActionResult Create()
        {
            return View();
        }
        [CustomAuthAttributes(Roles = "ADD")]
        [HttpPost]
        public ActionResult Create(Supplier sup)
        {
            if (ModelState.IsValid)
            {
                if (_sup.Add(sup))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }
        [CustomAuthAttributes(Roles = "EDIT")]
        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _sup.GetAll().AsQueryable().FirstOrDefault(x => x.SupplierId == id);
            return View(data);
        }
        [CustomAuthAttributes(Roles = "EDIT")]
        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Supplier sup)
        {
            if (ModelState.IsValid)
            {
                if (_sup.Edit(sup))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(sup);
                }
            }
            return View(sup);
        }
        [CustomAuthAttributes(Roles = "DELETE")]
        public ActionResult Delete(int id)
        {
			try
			{
				if (_pro.GetAll().Any(x => x.SupplierId == id))
				{
					TempData["msg"] = new ResponseMessage()
					{
						Type = "alert-danger",
						Message = "Fail! Supplier has products"
					};
					return RedirectToAction("Index");
				}
				_sup.Remove(id);
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