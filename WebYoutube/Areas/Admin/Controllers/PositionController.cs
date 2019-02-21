using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Framework;
using Data.DAO;
namespace WebYoutube.Areas.Admin.Controllers
{
    public class PositionController : BaseController
    {
        PositionDAO dao = new PositionDAO();
        // GET: Admin/Position
        public ActionResult Index()
        {
            return View(dao.List());
        }

        // GET: Admin/Position/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // GET: Admin/Position/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Position/Create
        [HttpPost]
        public ActionResult Create(Position collection)
        {
            try
            {
                // TODO: Add insert logic here
                dao.Add(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Position/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/Position/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Position collection)
        {
            try
            {
                // TODO: Add update logic here
                dao.Edit(id, collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Position/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dao.ViewDetails(id));
        }

        // POST: Admin/Position/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Position collection)
        {
            try
            {
                // TODO: Add delete logic here
                dao.Delete(id, collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
