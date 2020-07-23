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
    public class UserController : Controller
    {
        // GET: Admin/User
        Respository<User> _us;
        Respository<Order> _order;
        Respository<Group> _gr;
        
        public UserController()
        {
            _us = new Respository<User>();
            _gr = new Respository<Group>();
            _order = new Respository<Order>();
          
        }
        // GET: Admin/Attributes
        [CustomAuthAttributes(Roles = "VIEW")]
        public ActionResult Index(string table_search)
        {
            var data = _us.GetAll().AsQueryable().Include(x => x.Groups);
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.FullName.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }

        // GET: Admin/Attributes/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.GroupId = new SelectList(_gr.GetAll(), "GroupId", "GroupName");
            return View(_us.Get(id));
        }

        // GET: Admin/Attributes/Create
        public ActionResult Create()
        {

            ViewBag.GroupId = new SelectList(_gr.GetAll(), "GroupId", "GroupName");
            return View();
        }

        // POST: Admin/Attributes/Create
        [CustomAuthAttributes(Roles = "ADD")]
        [HttpPost]
        public ActionResult Create(User attr)
        {
            ViewBag.GroupId = new SelectList(_gr.GetAll(), "GroupId", "GroupName");
            
            if (ModelState.IsValid)
            {
                
                    if (_us.Add(attr))
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
            ViewBag.GroupId = new SelectList(_gr.GetAll(), "GroupId", "GroupName");
            return View(_us.Get(id));
        }

        // POST: Admin/Attributes/Edit/5
        [CustomAuthAttributes(Roles = "EDIT")]
        [HttpPost]
        public ActionResult Edit(User us)
        {
            ViewBag.GroupId = new SelectList(_gr.GetAll(), "GroupId", "GroupName");
            
            try
            {
                if (ModelState.IsValid)
                {
                    if(_us.GetAll().Any(x => x.UserName != us.UserName)){
                        if (_us.Edit(us))
                        {
                            TempData["msg"] = new ResponseMessage()
                            {
                                Type = "alert-success",
                                Message = "Update Done!"
                            };
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            TempData["msg"] = new ResponseMessage()
                            {
                                Type = "alert-danger",
                                Message = "Update Fail!"
                            };
                        }
                    }
                   
                   
                    return RedirectToAction("Index");
                }
               
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

        // GET: Admin/Attributes/Delete/5
        [CustomAuthAttributes(Roles = "DELETE")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_order.GetAll().Any(x => x.UserId == id))
                {
                    TempData["msg"] = new ResponseMessage()
                    {
                        Type = "alert-danger",
                        Message = "Failed! User having order"
                    };
                    return RedirectToAction("Index");
                }
                _us.Remove(id);
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