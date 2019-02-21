using Data.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Framework;
using Facebook;
using System.Configuration;

namespace WebYoutube.Controllers
{
    public class LoginController : Controller
    {

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        LoginDAO dao = new LoginDAO();
        UserDAO user = new UserDAO();
        PeopleDetailsDAO peole = new PeopleDetailsDAO();

        public ActionResult Login()
        {
            // login
            PostCategory2Controller.se = null;
            return View();
        }

        public ActionResult LoginFacebook()
        {
            PostCategory2Controller.se = null;
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            PostCategory2Controller.se = null;
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
              
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                Person p = new Person();
                p.Email = email;
                p.FullName = firstname + " " + middlename + " " + lastname;
                p.Status = true;
                p.CreateDate = DateTime.Now;
                p.PositionID = 4;
                var num = dao.AddForFacebook(p);

                if(num == 1)
                {
                    CheckInUser(p.Email, p.FullName, p.ID);
                    History h = new History();
                    InsertHistory(h, p);
                }
                else if(num == 0)
                {
                    var ob = user.ViewDetails(email);
                    CheckInUser(ob.Email, ob.FullName, ob.ID);
                    History h = new History();
                    InsertHistory(h, ob);
                }
                else if(num == -1)
                {
                    ModelState.AddModelError("", "Khong the dang nhap");
                }

            }
            else
            {

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(Data.Framework.Person p)
        {
               PostCategory2Controller.se = null;
            if (string.IsNullOrEmpty(p.Email) && string.IsNullOrEmpty(p.Pass))
            {
                ModelState.AddModelError("", "Thieu Thong Tin");
            }
            else if(!string.IsNullOrEmpty(p.Email) && string.IsNullOrEmpty(p.Pass))
            {
                var res = user.CheckMailFacebook(p.Email);
                if(res == 0)
                {
                    ModelState.AddModelError("", "Please click on facebook button");
                }
                else if(res == -1)
                {
                    ModelState.AddModelError("", "Thieu thong tin");
                }
                else if (res == 1)
                {
                    ModelState.AddModelError("", "Thieu thong tin");
                }
            }
            else if(!string.IsNullOrEmpty(p.Email) && !string.IsNullOrEmpty(p.Pass))
            {
                var ins = user.ViewDetails(p.Email);
                if (ins != null)
                {
                    CheckInUser(p.Email, ins.FullName, ins.ID);
                }
                var res = dao.Login(p.Email, p.Pass);
                if (res == 1)
                {
                    History h = new History();
                    InsertHistory(h, ins);
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Sai Mat Khau");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Sai Ten Tai Khoan");
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            PostCategory2Controller.se = null;
            return View();
        }

        [HttpPost]
        public ActionResult Register(Data.Framework.Person p, string repass)
        {
            PostCategory2Controller.se = null;
            if (string.IsNullOrEmpty(p.Email)
                && string.IsNullOrEmpty(p.Pass)
                && string.IsNullOrEmpty(repass)
                && string.IsNullOrEmpty(p.FullName))
            { }
            else
            {
                p.CreateDate = DateTime.Now;
                p.Status = true;
                p.PositionID = 4;
                var res = dao.Register(p);
                if (res == 1)
                {

                    CheckInUser(p.Email, p.FullName, p.ID);
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Thieu Thong Tin");
                }
                else
                {
                    ModelState.AddModelError("", "Xin moi nhap lai email");

                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            PostCategory2Controller.se = null;
            History h = new History();
            h.CreateDate = DateTime.Now;
            h.CategoryHistoryID = 2;
            h.PeopleID = WebYoutube.Session.User.Id;
            peole.InsertHistory(h);

            CheckOutUser();

            return RedirectToAction("Index", "Home");
        }

        public void CheckOutUser()
        {

            WebYoutube.Session.User.Email = null;
            WebYoutube.Session.User.FullName = null;
            WebYoutube.Session.User.Id = 0;
            Session[WebYoutube.Session.CommonConstants.USER_SESSTION] = null;
        }

        public void CheckInUser(string Email, string FullName, int id)
        {
          
            WebYoutube.Session.User u = new Session.User();
            WebYoutube.Session.User.Email = Email;
            WebYoutube.Session.User.FullName = FullName;
            WebYoutube.Session.User.Id = id;
            Session.Add(WebYoutube.Session.CommonConstants.USER_SESSTION, u);
        }

        public void InsertHistory(History h, Person p)
        {
           
            h.CreateDate = DateTime.Now;
            h.CategoryHistoryID = 1;
            h.PeopleID = p.ID;
            peole.InsertHistory(h);
        }

    }
}
