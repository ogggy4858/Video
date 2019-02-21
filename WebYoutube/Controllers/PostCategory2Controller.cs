using Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebYoutube.Controllers
{
    public class PostCategory2Controller : Controller
    {
        PostCategoryDAO dao = new PostCategoryDAO();
        public static int? idd = 0;
        public static string se = null;

        private static string se2 { get; set; }

        public ActionResult Index()
        {
            PostCategory2Controller.se = null;
            ViewBag.ListPost = dao.ListPost();
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id, string search, int page = 1, int pageSize = 4)
        {
            if (search == null){} else{se2 = search;}

            if (id == null){} else{idd = id;}

            ViewBag.GetName = dao.GetName(Convert.ToInt32(idd));

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPage = Math.Ceiling((decimal)dao.TotalRecord(Convert.ToInt32(idd), se2) / pageSize);
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(dao.ListPostDetails(Convert.ToInt32(idd), page, pageSize, se2));
        }
    }
}