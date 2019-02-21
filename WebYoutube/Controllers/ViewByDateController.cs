using Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebYoutube.Controllers
{
    public class ViewByDateController : Controller
    {
        ViewByDateDAO dao = new ViewByDateDAO();
        private static int year = 0;
    

        public ActionResult Year()
        {
            PostCategory2Controller.se = null;
            ViewBag.ListYear = dao.ListYear();
            return View();
        }

        public ActionResult Month(int id)
        {
            PostCategory2Controller.se = null;
            ViewBag.ListMonth = dao.ListMonth(id);
            year = id;
            ViewBag.Year = id;
            return View();
        }

        public ActionResult Day(string id)
        {
            PostCategory2Controller.se = null;
            PostCategoryDAO po = new PostCategoryDAO();
            NewsDAO news = new NewsDAO();

            ViewBag.ListPostCategory = po.List();
            ViewBag.ListNews = news.ListNews2();

            if (string.IsNullOrEmpty(id))
            {
                ViewBag.ListDay = new List<int>();
                ViewBag.ID = 0;
            }
            else
            {
                string[] list = id.Split('-');
                ViewBag.ListDay = dao.ListDay(Convert.ToInt32(list[0]), Convert.ToInt32(list[1]));
                ViewBag.ID = id;
                ViewBag.ListVideoFirst = dao.ListVideoFirst(Convert.ToInt32(list[0]), Convert.ToInt32(list[1]));
            }
            return View();
        }

        public ActionResult EachDay(string id)
        {
            PostCategory2Controller.se = null;
            PostCategoryDAO po = new PostCategoryDAO();
            NewsDAO news = new NewsDAO();

            ViewBag.ListPostCategory = po.List();
            ViewBag.ListNews = news.ListNews2();

            if (string.IsNullOrEmpty(id))
            {
                ViewBag.ListDay = new List<int>();
                ViewBag.ID = 0;
            }
            else
            {
                string[] list = id.Split('-');
                ViewBag.ListDay = dao.ListDay(Convert.ToInt32(list[0]), Convert.ToInt32(list[1]));
                ViewBag.ID = id;
                ViewBag.ListVideo = dao.ListVideo(Convert.ToInt32(list[0]), Convert.ToInt32(list[1]), Convert.ToInt32(list[2]));
            }
            return View();
        }

    }
}