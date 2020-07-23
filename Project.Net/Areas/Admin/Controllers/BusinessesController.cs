using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    public class BusinessesController : Controller
    {
        Reflection rfl;
        Respository<Business> _business;
        public BusinessesController()
        {
            rfl = new Reflection();
            _business = new Respository<Business>();
        }
        // GET: Admin/Busdinesses
     
        public ActionResult Index()
        {
            return View(_business.GetAll());
        }
        public ActionResult Update()
        {
            var controllers = rfl.GetAllController("Project.Net.Areas.Admin.Controllers");
            foreach(Type item in controllers)
            {
                var _ctrlName = item.Name.Replace("Controller", "");
                // Kiểm tra nghiệp vụ đã tồn tại hay chưa
                if (!_business.GetAll().Any(x => x.BusinessId == _ctrlName)) // Nếu chưa tồn tại
                {
                    Business b = new Business()
                    {
                        BusinessId = _ctrlName,
                        BusinessName = "Đang cập nhật...."
                    };
                    _business.Add(b);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            return View(_business.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(Business b)
        {
            if (ModelState.IsValid)
            {
                if (_business.Edit(b))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }
    }
}