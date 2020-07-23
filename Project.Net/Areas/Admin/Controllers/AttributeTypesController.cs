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
    public class AttributeTypesController : Controller
    {
        Respository<AttributeType> _atty;
        Respository<Attributes> _attr;
        public AttributeTypesController()
        {
            _atty = new Respository<AttributeType>();
            _attr = new Respository<Attributes>();
        }
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Index(string table_search)
        {
            var data = _atty.GetAll().AsQueryable().Include(x => x.Attributes);
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.TypeName.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }

        // GET: Admin/AttributeTypes/Details/5
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Details(int id)
        {
            return View(_atty.Get(id));
        }

        // GET: Admin/AttributeTypes/Create
        [CustomAuthAttributes(Roles = "ADD")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AttributeTypes/Create
        [CustomAuthAttributes(Roles = "ADD")]
        [HttpPost]
        public ActionResult Create(AttributeType atty)
        {
            if (ModelState.IsValid)
            {
                if (_atty.Add(atty))
                {
                    return RedirectToAction("Index");

                }
                return View();
            }
            return View();
        }

        // GET: Admin/AttributeTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_atty.Get(id));
        }

        // POST: Admin/AttributeTypes/Edit/5
        [HttpPost]
        public ActionResult Edit(AttributeType atty)
        {
            if (ModelState.IsValid)
            {
                if (_atty.Edit(atty))
                {
                    return RedirectToAction("Index");

                }
                return View();
            }
            return View();
        }

        // GET: Admin/AttributeTypes/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (_attr.GetAll().Any(x => x.TypeId == id))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-danger",
                        Message = "Thất bại! Thuộc tính đang có sản phẩm"
                    };
                    return RedirectToAction("Index");
                }
                _atty.Remove(id);
                TempData["msg"] = new ResponseMessage()
                {
                    Type = "alert-success",
                    Message = "Xóa thành công!"
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

        // POST: Admin/AttributeTypes/Delete/5
       
    }
}
