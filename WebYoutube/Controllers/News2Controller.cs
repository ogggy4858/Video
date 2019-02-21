using Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Framework;
using System.Web.Script.Serialization;

namespace WebYoutube.Controllers
{
    public class News2Controller : Controller
    {
        NewsDAO dao = new NewsDAO();
        private static int NewsID = 0;

        // GET: News2
        public ActionResult Index()
        {
            PostCategory2Controller.se = null;
            return View(dao.ListNews());
        }

        #region Details

        [HttpGet]
        public ActionResult Details(int ID)
        {
            PostCategory2Controller.se = null;
            NewsID = ID;
            dao.UpdateViewCount(ID);
            return View(dao.ViewDetails(ID));
        }

        #endregion

        #region Comment Like Dislike Share

        public JsonResult LoadComment()
        {
            var list = dao.ListComment(NewsID, WebYoutube.Session.User.Id);
            var CommentNewCount = dao.CommentNewCount(NewsID);


            return Json(new
            {
                status = true,
                data = list,
                CommentNewCount = CommentNewCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertComment(string Content)
        {
            string mess = "";

            CommentNew cm = new CommentNew();
            if (NewsID != 0 && WebYoutube.Session.User.Id != 0)
            {
                cm.Content = Content;
                cm.NewsID = NewsID;

                cm.PeopleID = WebYoutube.Session.User.Id;

                cm.Status = true;
                var res = dao.InsertComment(cm);
                mess = res;
            }
            else
            {

            }
            return Json(new
            {
                Mess = mess
            });
        }

        public JsonResult GetCommentByID(string CommentID)
        {
            string text = dao.ViewDetailsCommentNew(Convert.ToInt32(CommentID)).Content;

            return Json(new
            {
                text = text
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditComment(string model)
        {

            JavaScriptSerializer seria = new JavaScriptSerializer();
            CommentNew cm = seria.Deserialize<CommentNew>(model);
            //cm.NewsID = NewsID;
            //cm.PeopleID = WebYoutube.Session.User.Id;
            //cm.

            bool status = false;
            var res = dao.EditComment(cm.ID, cm);

            if (res == "")
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return Json(new
            {
                Mess = res,
                Status = status,

            });
        }

        public JsonResult DeleteComment(string CommentID)
        {
            bool status = false;
            var ID = Convert.ToInt32(CommentID);
            var res = dao.DeleteComment(ID);
            if (res == "")
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return Json(new
            {
                Stauts = status
            });
        }

        public JsonResult LoadLD()
        {
            var like = dao.CheckLike(WebYoutube.Session.User.Id, NewsID);
            var dis = dao.CheckDislike(WebYoutube.Session.User.Id, NewsID);
            var countLike = dao.CountLikeNews(NewsID);
            var countDis = dao.CountDislikeNews(NewsID);

            return Json(new
            {
                Like = like,
                Dis = dis,
                CountLike = countLike,
                CountDis = countDis
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LikeNews()
        {
            string mess = "";
            bool check = false;

            var like = dao.CheckLike(WebYoutube.Session.User.Id, NewsID);
            var dis = dao.CheckDislike(WebYoutube.Session.User.Id, NewsID);

            if (like == false && dis == false)
            {
                mess = dao.InsertLikeNews(WebYoutube.Session.User.Id, NewsID);
            }
            else if (like == true && dis == false)
            {
                mess = dao.DeleteLike(WebYoutube.Session.User.Id, NewsID);
            }
            else if (like == false && dis == true)
            {
                mess = dao.InsertLikeNews(WebYoutube.Session.User.Id, NewsID);
                mess = dao.DeleteDislike(WebYoutube.Session.User.Id, NewsID);
            }

            if (mess == "")
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return Json(new
            {
                Status = check,
                Mess = mess
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DislikeNews()
        {
            string mess = "";
            bool check = false;

            var like = dao.CheckLike(WebYoutube.Session.User.Id, NewsID);
            var dis = dao.CheckDislike(WebYoutube.Session.User.Id, NewsID);

            if (like == false && dis == false)
            {
                mess = dao.InsertDislikeNews(WebYoutube.Session.User.Id, NewsID);
            }
            else if (like == true && dis == false)
            {
                mess = dao.DeleteLike(WebYoutube.Session.User.Id, NewsID);
                mess = dao.InsertDislikeNews(WebYoutube.Session.User.Id, NewsID);
            }
            else if (like == false && dis == true)
            {
                mess = dao.DeleteDislike(WebYoutube.Session.User.Id, NewsID);
            }

            if (mess == "")
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return Json(new
            {
                Status = check,
                Mess = mess
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}