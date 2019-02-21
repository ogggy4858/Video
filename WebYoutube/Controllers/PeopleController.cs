using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DAO;
using Data.DTO;
using Data.Framework;
namespace WebYoutube.Controllers
{
    public class PeopleController : Controller
    {
        PeopleDetailsDAO dao = new PeopleDetailsDAO();
        PostDAO dao2 = new PostDAO();
        UserDAO user = new UserDAO();

        [HttpGet]
        public ActionResult Details(string se)
        {
            PostCategory2Controller.se = null;
            if (WebYoutube.Session.User.Email != null && WebYoutube.Session.User.Id != 0)
            {
                //lay ra email, info ng dung
                ViewBag.ViewBagEmail = WebYoutube.Session.User.Email;
                Data.DTO.PeopleInfomationDTO Infomation = new PeopleInfomationDTO();
                Infomation = dao.Infomation(WebYoutube.Session.User.Email);
                ViewBag.Infomation = Infomation;

                //lay ra list video cua ng dung
                IEnumerable<Data.DTO.PeopleListVideoDTO> ListVideo;
                ListVideo = dao.ListVideo(WebYoutube.Session.User.Id, se);
                ViewBag.ListVideo = ListVideo.ToList();

                // count like dis comment
                ViewBag.ListCount = dao.ListCount();
      
                return View(ListVideo);
            }
            else
            {
                return View();
            }

        }

        #region EditInfo

        [HttpGet]
        public ActionResult EditInfo(int Id)
        {
            PostCategory2Controller.se = null;
            return View(dao.ViewDetails(Id));
        }

        [HttpPost]
        public ActionResult EditInfo(int Id, HttpPostedFileBase image, PeopleInfomationDTO objec)
        {
            PostCategory2Controller.se = null;
            if (string.IsNullOrEmpty(objec.FullName) && image == null)
            {
                ModelState.AddModelError("", "Ten khong duoc bo trong");
                
            }
            else if (!string.IsNullOrEmpty(objec.FullName) && image == null)
            {
                var res = dao.EditInfo2(objec);
                var ob = dao.ViewDetailsPerson(Id);
                WebYoutube.Session.User.FullName = ob.FullName;

                if (res == "")
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    ModelState.AddModelError("", res);
                }
            }
            else if (string.IsNullOrEmpty(objec.FullName) && image != null)
            {

                var FileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
                image.SaveAs(path);

                objec.Immage = FileName;
                var res = dao.EditInfo3(objec);
                if (res == "")
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    ModelState.AddModelError("", res);
                }
            }
            else if (!string.IsNullOrEmpty(objec.FullName) && image != null)
            {

                var FileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
                image.SaveAs(path);

                var ob = dao.ViewDetailsPerson(Id);
                WebYoutube.Session.User.FullName = ob.FullName;

                objec.Immage = FileName;
                var res = dao.EditInfo(objec);
                if (res == "")
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    ModelState.AddModelError("", res);
                }


            }
            return View(objec);
        }

        public ActionResult EditPass()
        {
            PostCategory2Controller.se = null;
            ViewBag.Enable = user.CheckMailFacebook(WebYoutube.Session.User.Email);
            return View();
        }

        [HttpPost]
        public ActionResult EditPass(int Id, string pass, string newpass, string repass)
        {
            PostCategory2Controller.se = null;
            ViewBag.Enable = user.CheckMailFacebook(WebYoutube.Session.User.Email);
            if (string.IsNullOrEmpty(pass) && string.IsNullOrEmpty(newpass) && string.IsNullOrEmpty(repass))
            {

            }
            else if (string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(newpass) && !string.IsNullOrEmpty(repass))
            {
                var people = dao.ViewDetails(Id);
                if(newpass == repass)
                {
                    var res = dao.EditPass(Id, newpass);
                    if (res == "")
                    {
                        return RedirectToAction("Details");
                    }
                    else
                    {
                        ModelState.AddModelError("", res);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mat Khau Khong Trung Khop");
                }
            }
            else if (!string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(newpass) && !string.IsNullOrEmpty(repass))
            {
                var people = dao.ViewDetails(Id);
                if (people.Pass == pass)
                {
                    if (newpass == repass)
                    {
                        var res = dao.EditPass(Id, newpass);
                        if (res == "")
                        {
                            return RedirectToAction("Details");
                        }
                        else
                        {
                            ModelState.AddModelError("", res);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mat Khau Khong Trung Khop");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Mat Khau Hien Tai Sai");
                }
            }
            return View();
        }

        #endregion

        #region Post

        [HttpGet]
        public ActionResult EditPost(int ID)
        {
            PostCategory2Controller.se = null;
            var res = dao2.ViewDetails(ID);
            ViewBags(res.PostCategoryID);
            return View(res);
        }

        public void ViewBags(int? id = null)
        {
            ViewBag.PostCategoryID = new SelectList(dao2.ListPost(), "ID", "Name", id);
        }

        [HttpPost]
        public ActionResult EditPost(int ID, Post post, string mota, string noidung, HttpPostedFileBase poster)
        {
            PostCategory2Controller.se = null;
            try
            {
                ViewBags(post.PostCategoryID);

                string Poster = null;

                if (poster != null)
                {
                    Poster = Path.GetFileName(poster.FileName);
                    string path2 = Path.Combine(Server.MapPath("~/Common/img"), Poster);
                    poster.SaveAs(path2);
                }

                Post res = new Post();
                res.Description = mota;
                res.Content = noidung;
                res.Title = post.Title;
                res.PostCategoryID = post.PostCategoryID;
                res.Poster = Poster;

                dao.EditPost(ID, res);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            PostCategory2Controller.se = null;
            return View(dao.ViewDetailsPost(ID));
        }

        [HttpPost]
        public ActionResult Delete(long ID)
        {
            PostCategory2Controller.se = null;
            string mess = "";
            mess = dao.DeletePost(ID);
            if (mess == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region History

        [HttpGet]
        public ActionResult LoginHistory(int ID)
        {
            PostCategory2Controller.se = null;
            return View(dao.ListLoginLogoutHistory(ID));
        }

        [HttpGet]
        public ActionResult PostHistory(int ID)
        {
            PostCategory2Controller.se = null;
            return View(dao.ListPostHistory(ID));
        }

        [HttpGet]
        public ActionResult CommentHistory(int ID)
        {
            PostCategory2Controller.se = null;
            return View(dao.ListCommentHistory(ID));
        }

        [HttpGet]
        public ActionResult LikeHistory(int ID)
        {
            PostCategory2Controller.se = null;
            var res = dao.ListLikeHistory(ID);
            return View(res);
        }

        [HttpGet]
        public ActionResult DislikeHistory(int ID)
        {
            PostCategory2Controller.se = null;
            return View(dao.ListDislikeHistory(ID));
        }

        #endregion
    }
}