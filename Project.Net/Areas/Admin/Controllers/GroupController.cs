using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    public class GroupController : Controller
    {
        Respository<Group> _group;
        Respository<Business> _business;
        Respository<Role> _role;
        Respository<GroupRoles> _groupRole;
        public GroupController()
        {
            _group = new Respository<Group>();
            _business = new Respository<Business>();
            _role = new Respository<Role>();
            _groupRole = new Respository<GroupRoles>();
        }

        // GET: Admin/Groups
        
        public ActionResult Index()
        {
            ViewBag.businesses = _business.GetAll();
            ViewBag.roles = _role.GetAll();
            return View(_group.GetAll());
        }
       
        [HttpPost]
        public ActionResult Grant(GroupRoles gr)
        {
            string msg = "";
            // 1. Gán
            // 2. Hủy
            // Kiểm tra quyền đã có hay chưa
            if (_groupRole.GetAll().Any(x => x.GroupId == gr.GroupId && x.BusinessId == gr.BusinessId && x.RoleId == gr.RoleId))
            {
                // Quyền đã tồn tại => Hủy
                var _gr = _groupRole.SingleBy(x => x.GroupId == gr.GroupId && x.BusinessId == gr.BusinessId && x.RoleId == gr.RoleId);
                // Hủy quyền (xóa)
                _groupRole.Remove(_gr);
                msg = "Drop Role Successfully!";
            }
            else
            {
                // Quyền chưa tồn tại => Gán quyền
                _groupRole.Add(gr);
                msg = "Assign Role Successfully!";
            }
            return Json(new
            {
                StatusCode = 200,
                Message = msg
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetRoles(int id)
        {
            var data = _groupRole.GetBy(x => x.GroupId == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}