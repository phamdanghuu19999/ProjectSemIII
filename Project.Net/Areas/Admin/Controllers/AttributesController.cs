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
    public class AttributesController : Controller
    {
        Respository<AttributeType> _atty;
      
        Respository<Attributes> _attr;
        Respository<Product> _pro;
        Respository<ProductAttr> _prat;
        public AttributesController()
        {
            _attr = new Respository<Attributes>();
            _atty = new Respository<AttributeType>();
            _pro = new Respository<Product>();
            _prat = new Respository<ProductAttr>();
        }
        // GET: Admin/Attributes
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Index(string table_search)
        {
            var data = _attr.GetAll().AsQueryable().Include(x => x.AttributeTypes);
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.Name.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }

        // GET: Admin/Attributes/Details/5
        public ActionResult Details(int id)
        {
            return View(_attr.Get(id));
        }

        // GET: Admin/Attributes/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(_atty.GetAll(), "AttrTypeId", "TypeName");
            return View();
        }

        // POST: Admin/Attributes/Create
        [CustomAuthAttributes(Roles = "ADD")]
        [HttpPost]
        public ActionResult Create(Attributes attr)
        {
            ViewBag.TypeId = new SelectList(_atty.GetAll(), "AttrTypeId", "TypeName");
            if (ModelState.IsValid)
            {
                if (_attr.Add(attr))
                {
                    return RedirectToAction("Index");

                }
                return View();
            }
            return View();
        }

        // GET: Admin/Attributes/Edit/5
        [CustomAuthAttributes(Roles = "EDIT")]
        public ActionResult Edit(int id)
        {
            ViewBag.TypeId = new SelectList(_atty.GetAll(), "AttrTypeId", "TypeName");
            return View(_attr.Get(id));
        }

        // POST: Admin/Attributes/Edit/5
        [CustomAuthAttributes(Roles = "EDIT")]
        [HttpPost]
        public ActionResult Edit(Attributes attr)
        {
            ViewBag.TypeId = new SelectList(_atty.GetAll(), "AttrTypeId", "TypeName");
            if (ModelState.IsValid)
            {
                if (_attr.Edit(attr))
                {
                    return RedirectToAction("Index");

                }
                return View();
            }
            return View();
        }

        // GET: Admin/Attributes/Delete/5
        [CustomAuthAttributes(Roles = "DELETE")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_prat.GetAll().Any(x => x.AttrId== id))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-danger",
                        Message = "Thất bại! Thuộc tính đang có sản phẩm"
                    };
                    return RedirectToAction("Index");
                }
                _attr.Remove(id);
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

        // POST: Admin/Attributes/Delete/5
        
    }
}
