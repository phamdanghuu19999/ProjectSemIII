using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Models.DataModel
{
    public class CustomAuthAttributes : AuthorizeAttribute

    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // check đăng nhập
            if(HttpContext.Current.Session["User"]== null)
            {
                return false;

            }
            if (String.IsNullOrEmpty(this.Roles))
            {
                return true;
            }
            // lấy controller hiện tại
            var controller = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");
            // lấy quyền người dùng
            Respository<GroupRoles> _groupRole = new Respository<GroupRoles>();
            var _user = HttpContext.Current.Session["User"] as User;
            var _groupRoles = _groupRole.GetBy(x => x.GroupId == _user.GroupId);
            // check xem trong đó có quyền yêu cầu hay không
            if (!_groupRoles.Any(x=>x.BusinessId==controller && x.RoleId==this.Roles))
            {
                return false;

            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "Unauthorized"
            };
        }

    }
}