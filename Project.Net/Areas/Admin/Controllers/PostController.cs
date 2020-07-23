using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        Respository<Post> _pos;
        
        public PostController()
        {
            _pos = new Respository<Post>();
        }
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(_pos.GetAll());
        }
        // GET: Admin/Post/Details/5

        public ActionResult Details(int id)
        {
            return View(_pos.Get(id));
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Post/Create
        [HttpPost]
        public ActionResult Create(Post p)
        {
            if (ModelState.IsValid)
            {
                if (_pos.Add(p))
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

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_pos.Get(id));
        }

        // POST: Admin/Post/Edit/5
        [HttpPost]
        public ActionResult Edit(Post p)
        {

            if (ModelState.IsValid)
            {
                if (_pos.Edit(p))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(p);
                }
            }
            return View(p);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int id)
        {
            if (_pos.Remove(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Post/Delete/5
       
    }
}
