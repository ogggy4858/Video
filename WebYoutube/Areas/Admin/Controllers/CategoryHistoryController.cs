using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DAO;
using Data.Framework;

namespace WebYoutube.Areas.Admin.Controllers
{
    public class CategoryHistoryController : BaseController
    {
        PeopleDetailsDAO dao = new PeopleDetailsDAO();
        // GET: Admin/CategoryHistory
        public ActionResult Index()
        {

            return View(dao.ListCategoryHistory());
        }

        // GET: Admin/CategoryHistory/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ViewDetailsCategoryHistory(id));
        }

        // GET: Admin/CategoryHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryHistory/Create
        [HttpPost]
        public ActionResult Create(CategoryHistory collection)
        {
            try
            {
                // TODO: Add insert logic here
                dao.InsertCategoryHistory(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CategoryHistory/Edit/5
        public ActionResult Edit(int id)
        {

            return View(dao.ViewDetailsCategoryHistory(id));
        }

        // POST: Admin/CategoryHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryHistory collection)
        {
            try
            {
                // TODO: Add update logic here
                dao.UpdateCategoryHistory(id, collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CategoryHistory/Delete/5
        public ActionResult Delete(int id)

        {
            return View(dao.ViewDetailsCategoryHistory(id));
        }

        // POST: Admin/CategoryHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CategoryHistory collection)
        {
            try
            {
                // TODO: Add delete logic here
                var res = dao.DeleteCategoryHistory(id, collection);
                if (res == "")
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
