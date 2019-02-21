using Data.DAO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebYoutube.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryDAO dao = new CategoryDAO();
        // GET: Admin/Category
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var list = dao.ListAllPading(search, page, pageSize);

            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category collection)
        {
            try
            {
                // TODO: Add insert logic here
                var res = dao.Create(collection);
                if(res == 1)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                // TODO: Add update logic here
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

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {

            return View(dao.ViewDetails(id));
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
