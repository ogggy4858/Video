using Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DTO;
using Data.Framework;

namespace WebYoutube.Areas.Admin.Controllers
{
    public class PostCategoryController : BaseController
    {
        PostCategoryDAO dao = new PostCategoryDAO();
        // GET: Admin/PostCategory
        public ActionResult Index()
        {
            return View(dao.List());
        }

        // GET: Admin/PostCategory/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // GET: Admin/PostCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostCategory/Create
        [HttpPost]
        public ActionResult Create(PostCategory collection)
        {
            try
            {
                var res = dao.Add(collection);
                if (res == true)
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

        // GET: Admin/PostCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/PostCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PostCategory collection)
        {
            try
            {
                var rres = dao.Edit(id, collection);
                if (rres)
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

        // GET: Admin/PostCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/PostCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PostCategory collection)
        {
            try
            {
                // TODO: Add delete logic here
                var res = dao.Delete(id, collection);
                if (res)
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
