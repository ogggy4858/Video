using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebYoutube.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
     

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (WebYoutube.Areas.Admin.Session.Admin)Session[WebYoutube.Areas.Admin.Session.Admin.ADMIN_SESSTION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "AdminLogin", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}