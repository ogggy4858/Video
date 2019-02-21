using Data.DAO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebYoutube.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        NewsDAO dao = new NewsDAO();

        private static int NewsID = 0;

        #region Insert Update Delete List

        [HttpGet]
        public ActionResult Index()
        {

            return View(dao.ListNews());
        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
            NewsID = ID;
            return View(dao.ViewDetails(ID));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string Title, string Content, int DisplayOrder, HttpPostedFileBase Image, HttpPostedFileBase Image2)
        {
            string mess = "";

            try
            {
                if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Content) && DisplayOrder == 0)
                { }
                else
                {

                    var FileName = Path.GetFileName(Image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
                    Image.SaveAs(path);

                    var FileName2 = Path.GetFileName(Image2.FileName);
                    string path2 = Path.Combine(Server.MapPath("~/Common/img"), FileName2);
                    Image.SaveAs(path2);

                    if (string.IsNullOrEmpty(FileName))
                    { }
                    else
                    {
                        News news = new News();
                        news.Content = Content;
                        news.CreateDate = DateTime.Now;
                        news.DisplayOrder = DisplayOrder;
                        news.Image = FileName;
                        news.Title = Title;
                        news.PeopleID = Admin.Session.Admin.ID;
                        news.Image2 = FileName2;
                        var res = dao.InsertNews(news);

                        if (res == "")
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            mess = res;
                            ModelState.AddModelError("", "Error");
                        }
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", mess);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            return View(dao.ViewDetails(ID));
        }
        [HttpPost]
        public ActionResult Edit(int ID, string Title, string Content, int DisplayOrder, HttpPostedFileBase Image)
        {
            string mess = "";
            try
            {

                if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Content) && DisplayOrder == 0)
                { }
                else
                {
                    var FileName = Path.GetFileName(Image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
                    Image.SaveAs(path);
                    if (string.IsNullOrEmpty(FileName))
                    { }
                    else
                    {
                        News news = new News();
                        news.Content = Content;
                        news.DisplayOrder = DisplayOrder;
                        news.Image = FileName;
                        news.Title = Title;
                        news.PeopleID = WebYoutube.Session.User.Id;
                        var res = dao.UpdateNews(ID, news);
                        if (res == "")
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            mess = res;
                            ModelState.AddModelError("", "Error");
                        }

                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", mess);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {

            return View(dao.ViewDetails(ID));
        }
        [HttpPost]
        public ActionResult Delete(int ID, News news)
        {
            string mess = "";
            try
            {
                var res = dao.DeleteNews(ID, news);
                if (res == "")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    mess = res;
                    ModelState.AddModelError("", "Error");
                }

            }
            catch
            {
                ModelState.AddModelError("", mess);
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditOrder(int ID)
        {
            ViewBag.ListNewsOrder = dao.ListNewsOrder();
            return View(dao.ViewDetails(ID));
        }
        [HttpPost]
        public ActionResult EditOrder(int ID, News news, HttpPostedFileBase Image)
        {

            var FileName = Path.GetFileName(Image.FileName);
            string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
            Image.SaveAs(path);

            news.Image2 = FileName;

            dao.EditOrder(ID, news);
            ViewBag.ListNewsOrder = dao.ListNewsOrder();
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
            if (NewsID != 0 && (WebYoutube.Session.User.Id != 0 || WebYoutube.Areas.Admin.Session.Admin.ID != 0))
            {
                cm.Content = Content;
                cm.NewsID = NewsID;
                if (WebYoutube.Session.User.Id == 0)
                {
                    cm.PeopleID = WebYoutube.Areas.Admin.Session.Admin.ID;
                }
                if (WebYoutube.Areas.Admin.Session.Admin.ID == 0)
                {
                    cm.PeopleID = WebYoutube.Session.User.Id;
                }
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

            if(mess == "")
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

            if(mess == "")
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return Json(new
            {
                Status= check,
                Mess = mess
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}