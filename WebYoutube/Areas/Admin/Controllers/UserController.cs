using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Framework;
using Data.DAO;
using System.IO;
namespace WebYoutube.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserDAO dao = new Data.DAO.UserDAO();
        // GET: Admin/User
        public ActionResult Index(string se, int page = 1, int pageSize = 5)
        {
            var list = dao.ListAllPading(se, page, pageSize);
            return View(list);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            ViewBags();
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(Person collection, HttpPostedFileBase image)
        {

            try
            {
                // TODO: Add insert logic here
                ViewBags();
                var FileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
                image.SaveAs(path);
                collection.Immage = FileName;
                collection.CreateDate = DateTime.Now;
                collection.Status = true;
                var res = dao.Create(collection);
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }
                if (res == -1)
                {
                    ModelState.AddModelError("", "Email da ton tai");
                }
                else
                {
                    ModelState.AddModelError("", "Thieu Thong Tin");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public void ViewBags(int? id = null)
        {
            ViewBag.PositionID = new SelectList(dao.ListPosition(), "ID", "Name", id);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBags(id);
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person collection, HttpPostedFileBase image)
        {
            try
            {
                // TODO: Add update logic here
                ViewBags(id);
                if (image != null)
                {
                    var FileName = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Common/img"), FileName);
                    image.SaveAs(path);
                    collection.Immage = FileName;
                }
                var res = dao.Edit(id, collection);
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Person collection)
        {
            try
            {
                // TODO: Add delete logic here
                var res = dao.Delete(id);
                if (res == 1)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}

