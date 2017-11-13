using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    public class GroupEventsController : Controller
    {
        // GET: GroupEvents
        public ActionResult Index()
        {
            return View();
        }

        // GET: GroupEvents/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupEvents/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupEvents/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupEvents/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupEvents/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupEvents/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
