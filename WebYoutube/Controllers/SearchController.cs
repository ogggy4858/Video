using Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Framework;

namespace WebYoutube.Controllers
{
    public class SearchController : Controller
    {
        SearchDAO dao = new SearchDAO();
        NewsDAO news = new NewsDAO();
        PostCategoryDAO po = new PostCategoryDAO();

        private static string se = null;

        // GET: Search
        public ActionResult Index(string searchString, int page = 1, int pageSize = 6)
        {
            PostCategory2Controller.se = null;
            if (searchString == null)
            {

            }
            else
            {
                se = searchString;
            }
            
            var list = dao.List(se, page, pageSize);
            int count = dao.Count(se);

            ViewBag.ListNews = news.ListNews2();

            ViewBag.Count = count;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPage = Math.Ceiling(((decimal)count / (decimal)pageSize));
            ViewBag.Prev = page - 1;
            ViewBag.Next = page + 1;
            ViewBag.First = 1;
            ViewBag.ListPostCategory = po.List();

            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            if (WebYoutube.Session.User.Id == 0)
            {
                Search se = new Search();
                se.KeyWord = searchString;
                dao.UpdateCount(null, se, searchString, myIP);
            }
            else
            {
                Search se = new Search();
                se.KeyWord = searchString;
                dao.UpdateCount(WebYoutube.Session.User.Id, se, searchString);
            }

            return View(list);
        }


    }
}