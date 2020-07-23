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
  
    public class NotesController : Controller
    {
        Respository<Note> _note;
        Respository<Product> _pro;
        Respository<Banner> _ban;

        public NotesController()
        {
            _note = new Respository<Note>();
            _pro = new Respository<Product>();
            _ban = new Respository<Banner>();
        }
        // GET: Admin/Notes
        public ActionResult Index(string table_search)
        {
            var data = _note.GetAll().AsQueryable();
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.AttrName.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }
        // GET: Admin/Notes/Details/5
        public ActionResult Details(int id)
        {
            return View(_note.Get(id));
        }
        // GET: Admin/Notes/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/Notes/Create
        [HttpPost]
        public ActionResult Create(Note n)
        {
            if (ModelState.IsValid)
            {
                if (_note.Add(n))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }
        // GET: Admin/Notes/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_note.Get(id));
        }
        // POST: Admin/Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(Note n)
        {
            if (ModelState.IsValid)
            {
                if (_note.Edit(n))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }

        // GET: Admin/Notes/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        if (_pro.GetAll().Any(x => x.NoteId == id || _ban.GetAll().Any(b=>b.NoteId==id)))
        //        {
        //            TempData["msg"] = new ResponseMessage()
        //            {
        //                Type = "alert-danger",
        //                Message = "Thất bại! Đang có sản phẩm thuộc note này"
        //            };
        //            return RedirectToAction("Index");
        //        }
        //        _note.Remove(id);
        //        TempData["msg"] = new ResponseMessage()
        //        {
        //            Type = "alert-success",
        //            Message = "Xóa thành công!"
        //        };
        //    }
        //    catch (Exception ex)
        //    {

        //        TempData["msg"] = new ResponseMessage()
        //        {
        //            Type = "alert-danger",
        //            Message = ex.Message
        //        };
        //    }
        //    return RedirectToAction("Index");
        //}

        
    }
}
