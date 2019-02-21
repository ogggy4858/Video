using Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebYoutube.Controllers
{
    public class HomeController : Controller
    {
        
        HomeDAO dao = new HomeDAO();
        // GET: Home
        public ActionResult Index()
        {
            PostCategory2Controller.se = null;
            if (WebYoutube.Session.User.Email != null)
            {
                ViewBag.ViewBagEmail = WebYoutube.Session.User.Email;
                ViewBag.ViewBagID = WebYoutube.Session.User.Id;
                ViewBag.ListPostNew = dao.ListPostNew();
                ViewBag.Offer = dao.Offer(WebYoutube.Session.User.Id);
            }
            else
            {
                ViewBag.Offer = dao.Offer();
                ViewBag.ListPostNew = dao.ListPostNew();

            }
            if(dao.Banner() == null)
            {
                ViewBag.Banner = "";
            }
            else
            {
                ViewBag.Banner = dao.Banner();
            }



            ViewBag.ListNews = dao.ListNews();
            ViewBag.Review = dao.Review();
       

            return View();
        }
        
        public ActionResult Footer()
        {
            PostCategory2Controller.se = null;
            ViewBag.PostsNew = dao.PostsNew();
            ViewBag.News = dao.News();
            return PartialView();
        }


    }
}