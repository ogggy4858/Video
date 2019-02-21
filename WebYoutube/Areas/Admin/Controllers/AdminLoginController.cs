using Data.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebYoutube.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        LoginDAO dao = new LoginDAO();
        UserDAO user = new UserDAO();


        public ActionResult Index()
        {
            // login
            return View();
        }
        [HttpPost]
        public ActionResult Index(Data.Framework.Person p)
        {
            // login
            if (string.IsNullOrEmpty(p.Email) && string.IsNullOrEmpty(p.Pass))
            { }
            else
            {
                byte num = 0;

                foreach (var item in dao.ListAdminName())
                {
                    if (p.Email == item)
                    {
                        int res = dao.Login(p.Email, p.Pass);
                        if (res == 1)
                        {
                            var ins = user.ViewDetails(p.Email);
                            CheckInAdmin(p.Email, ins.FullName, ins.ID, ins.Position.Name);
                            return RedirectToAction("Index", "HomeAdmin");
                        }
                        else if (res == -1)
                        {
                            ModelState.AddModelError("", "Khong Thay Email");
                        }
                        else if (res == 0)
                        {
                            ModelState.AddModelError("", "Sai Mat Khau");
                        }
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }

                if(num == dao.ListAdminName().Count)
                {
                    ModelState.AddModelError("", "Ban Khong Phai Admin");
                }
                

            }
            return View();
        }



        public ActionResult Logout()
        {
            CheckOutAdmin();
            return RedirectToAction("Index", "AdminLogin");
        }

        public void CheckInAdmin(string Email, string FullName, int ID, string position)
        {
            WebYoutube.Areas.Admin.Session.Admin a = new Areas.Admin.Session.Admin();
            WebYoutube.Areas.Admin.Session.Admin.Email = Email;
            WebYoutube.Areas.Admin.Session.Admin.FullName = FullName;
            WebYoutube.Areas.Admin.Session.Admin.ID = ID;
            WebYoutube.Areas.Admin.Session.Admin.Position = position;
            Session.Add(WebYoutube.Areas.Admin.Session.Admin.ADMIN_SESSTION, a);
        }

        public void CheckOutAdmin()
        {
            WebYoutube.Areas.Admin.Session.Admin.Email = null;
            WebYoutube.Areas.Admin.Session.Admin.FullName = null;
            WebYoutube.Areas.Admin.Session.Admin.ID = 0;
            WebYoutube.Areas.Admin.Session.Admin.Position = null;
            Session[WebYoutube.Areas.Admin.Session.Admin.ADMIN_SESSTION] = null;
        }
    }
}
