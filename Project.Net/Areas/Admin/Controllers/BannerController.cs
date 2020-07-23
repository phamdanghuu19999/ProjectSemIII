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
    public class BannerController : Controller
    {
        Respository<Banner> _ban;
        Respository<Note> _note;
       
        public BannerController()
        {
            _ban = new Respository<Banner>();
            _note = new Respository<Note>();
        }

        // GET: Admin/Banner
        public ActionResult Index(string table_search)
        {
            var data = _ban.GetAll().AsQueryable().Include(x => x.Notes);
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.Name.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }

        // GET: Admin/Banner/Details/5


        public ActionResult Details(int id)
        {

            return View(_ban.GetAll().AsQueryable().Include(x=>x.Notes).FirstOrDefault(x=>x.Id==id));

        }

        // GET: Admin/Banner/Create

        public ActionResult Create()
        {
            ViewBag.NoteId = new SelectList(_note.GetBy(x => x.Type == 1), "Id", "AttrName");
            return View();
        }

        // POST: Admin/Banner/Create

        [HttpPost]
        public ActionResult Create(Banner b)
        {
            ViewBag.NoteId = new SelectList(_note.GetBy(x => x.Type == 1), "Id", "AttrName");
            if (ModelState.IsValid)
            {
                if (_ban.Add(b))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Admin/Banner/Edit/5
        public ActionResult Edit(int id)
        {
            
            var data = _ban.GetAll().AsQueryable().Include(x=>x.Notes).FirstOrDefault(x => x.Id == id);
            ViewBag.NoteId = new SelectList(_note.GetBy(x => x.Type == 1), "Id", "AttrName",data.NoteId);
            return View(data);
        }

        // POST: Admin/Banner/Edit/5

        [HttpPost]
        public ActionResult Edit(Banner b)
        {
            ViewBag.NoteId = new SelectList(_note.GetBy(x => x.Type == 1), "Id", "AttrName",b.NoteId);
            if (ModelState.IsValid)
            {
                if (_ban.Edit(b))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(b);
                }
            }
            return View(b);
        }

        // GET: Admin/Banner/Delete/5
        [CustomAuthAttributes(Roles = "DELETE")]
        public ActionResult Delete(int id)
        {
            if (_ban.Remove(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        
    }
}
