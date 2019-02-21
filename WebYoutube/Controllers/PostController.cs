using Data.DAO;
using Data.DTO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebYoutube.Controllers
{
    public class PostController : Controller
    {
        private static int PostID;

        PostDAO dao2 = new PostDAO();

        public ActionResult Create()
        {
            PostCategory2Controller.se = null;
            ViewBags();
            //ViewBag.PostCategoryID = 
            return View();
        }

        public void ViewBags(int? id = null)
        {
            ViewBag.PostCategoryID = new SelectList(dao2.ListPost(), "ID", "Name", id);
        }

        [HttpPost]
        public ActionResult Create(Post p, HttpPostedFileBase video, string noidung, string mota, HttpPostedFileBase poster)
        {
            PostCategory2Controller.se = null;
            ViewBags();
            if (ModelState.IsValid)
            {
          
                var FileName = Path.GetFileName(video.FileName);
                string path = Path.Combine(Server.MapPath("~/Common/video"), FileName);
                video.SaveAs(path);

                var Poster = Path.GetFileName(poster.FileName);
                string path2 = Path.Combine(Server.MapPath("~/Common/img"), Poster);
                poster.SaveAs(path2);

                if (WebYoutube.Session.User.Id == 0)
                { }
                else
                {
                    if (string.IsNullOrEmpty(p.Title) && string.IsNullOrEmpty(p.Content) && string.IsNullOrEmpty(FileName))
                    { }
                    else
                    {
                        p.Video = FileName;
                        p.PeopleID = WebYoutube.Session.User.Id;
                        p.Status = true;
                        p.CreateDate = DateTime.Now;
                        p.Content = noidung;
                        p.Description = mota;
                        p.Poster = Poster;
                        // add
                        try
                        {
                            dao2.Add(p);
                            //return RedirectToAction("");
                            return Redirect("~/");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.ToString());
                        }

                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Show(int ID)
        {
            PostCategory2Controller.se = null;
            PostID = ID;
            var Post = dao2.ViewDetails(ID);
            if (Post.PeopleID != WebYoutube.Session.User.Id && WebYoutube.Session.User.Id != 0)
            {

                if (dao2.CheckView(WebYoutube.Session.User.Id, ID))
                {
                    dao2.UpdateView(WebYoutube.Session.User.Id, ID);
                }
                else
                {
                    Vieww v = new Vieww();
                    v.IP = null;
                    v.PeopleID = WebYoutube.Session.User.Id;
                    v.PostID = ID;
                    dao2.AddView(v);
                }
            }
            else if (WebYoutube.Session.User.Id == 0)
            {
                // get ip
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                // add view
                if (dao2.CheckView(myIP))
                {
                    dao2.UpdateView(myIP);
                }
                else
                {
                    Vieww v = new Vieww();
                    v.PostID = ID;
                    v.IP = myIP;
                    dao2.AddView(v);
                }
            }

            ViewBag.ViewCount = dao2.ViewCount(ID);
            ViewBag.SessionID = WebYoutube.Session.User.Id;
            // comment
            if (WebYoutube.Session.User.Id != 0)
            {
                ViewBag.Comment = dao2.LoadComment(ID, WebYoutube.Session.User.Id);
                ViewBag.Offer = dao2.Offer(WebYoutube.Session.User.Id, ID);
            }
            else
            {
                List<CommentDTO> listComment = new List<CommentDTO>();
                ViewBag.Comment = listComment;
                ViewBag.Offer = dao2.Offer();
            }
            //end
            return View(Post);
        }

        public ActionResult aaa()
        {
            PostCategory2Controller.se = null;
            return View();
        }

        #region Like Dislike Share

        [HttpGet]

        public JsonResult LoadLikeDislike()
        {

            bool Like = dao2.CheckLike(WebYoutube.Session.User.Id, PostID);
            bool Dislike = dao2.CheckDislike(WebYoutube.Session.User.Id, PostID);

            int LikeCount = dao2.LikeCount(PostID);
            int DislikeCount = dao2.DislikeCount(PostID);

            return Json(new
            {
                Like = Like,
                Dislike = Dislike,
                LikeCount = LikeCount,
                DislikeCount = DislikeCount

            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult EditLike()
        {
            string mess = "";
            int res = 0;
            if (WebYoutube.Session.User.Id != 0 && PostID != 0)
            {
                var like = dao2.CheckLike(WebYoutube.Session.User.Id, PostID);
                var dis = dao2.CheckDislike(WebYoutube.Session.User.Id, PostID);

                var deleLike = dao2.ViewDetailsLike(WebYoutube.Session.User.Id, PostID);
                var deleDis = dao2.ViewDetailsDislike(WebYoutube.Session.User.Id, PostID);

                Likee ins = new Likee();
                ins.CreateDate = DateTime.Now;
                ins.PeopleID = WebYoutube.Session.User.Id;
                ins.PostID = PostID;


                if (like == true && dis == false)
                {
                    mess = dao2.DeleteLike(deleLike);
                    res = 1;
                }
                else if (like == false && dis == true)
                {
                    mess = dao2.InsertLike(ins);
                    mess = dao2.DeleteDislike(deleDis);
                    res = 2;
                }
                else if (like == false && dis == false)
                {
                    mess = dao2.InsertLike(ins);
                    res = 3;
                }
                else
                {
                    res = 0;
                }
            }
            else
            {
                res = -1;
            }

            return Json(new
            {
                Mess = mess,
                Res = res

            });
        }
        [HttpPost]

        public JsonResult EditDislike()
        {
            string mess = "";
            int res = 0;

            if (WebYoutube.Session.User.Id != 0 && PostID != 0)
            {
                var like = dao2.CheckLike(WebYoutube.Session.User.Id, PostID);
                var dis = dao2.CheckDislike(WebYoutube.Session.User.Id, PostID);

                var deleLike = dao2.ViewDetailsLike(WebYoutube.Session.User.Id, PostID);
                var deleDis = dao2.ViewDetailsDislike(WebYoutube.Session.User.Id, PostID);

                DisLike ins = new DisLike();
                ins.CreateDate = DateTime.Now;
                ins.PeopleID = WebYoutube.Session.User.Id;
                ins.PostID = PostID;

                if (like == true && dis == false)
                {
                    mess = dao2.DeleteLike(deleLike);
                    mess = dao2.InsertDislike(ins);
                    res = 1;
                }
                else if (like == false && dis == true)
                {
                    mess = dao2.DeleteDislike(deleDis);
                    res = 2;
                }
                else if (like == false && dis == false)
                {
                    mess = dao2.InsertDislike(ins);
                    res = 3;
                }
                else
                {
                    res = 0;
                }
            }
            else
            { res = -1; }
            return Json(new
            {
                Mess = mess,
                Res = res

            });
        }

        #endregion

        #region comment

        [HttpGet]
        public JsonResult LoadComment()
        {
            int CommentCount = dao2.CommentCount(PostID);
            List<CommentDTO> listComment = new List<CommentDTO>();
            if (PostID == 0)
            {

            }
            else
            {

                if (WebYoutube.Session.User.Id == 0)
                {
                    listComment = dao2.LoadComment(PostID);
                }
                else
                {
                    listComment = dao2.LoadComment(PostID, WebYoutube.Session.User.Id);
                }
            }
            return Json(
                new
                {
                    data = listComment,
                    status = true,
                    id = WebYoutube.Session.User.Id,
                    CommentCount = CommentCount
                }, JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult InsertComment(string Comment)
        {
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Data.Framework.Comment cm2 = new Comment();
            //cm2 = js.Deserialize<Data.Framework.Comment>(Comment);

            Data.Framework.Comment cm = new Comment();

            cm.CreateDate = DateTime.Now;
            cm.PeopleID = WebYoutube.Session.User.Id;
            cm.PostID = PostID;
            cm.Status = true;
            cm.Content = Comment;
            var res = dao2.InsertComment(cm);

            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetCommentByID(int ID)
        {
            string res = dao2.GetCommentByID(ID);
            return Json(new
            {
                data = res,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditComment(string model)
        {
            JavaScriptSerializer seria = new JavaScriptSerializer();
            Comment cm = seria.Deserialize<Comment>(model);
            string message = "";

            var res = dao2.EditComment(cm);
            if (res == "")
            {
                return Json(new { });
            }
            else
            {
                message = res;

                return Json(new
                {
                    Message = message
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteComment(int ID)
        {
            string message = "";
            var res = dao2.DeleteComment(ID);
            if (res == "")
            {
                return Json(new { });
            }
            else
            {
                message = res;

                return Json(new
                {
                    Message = message
                });
            }
        }

        #endregion
    }
}