using Argos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.Controllers
{
    public class SecurityController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Security
        public ActionResult Users()
        {
            var model = new List<ApplicationUser>();
            
            return View(model);
        }

        // GET: Security/Details/5
        public ActionResult BeginAddUser(int id)
        {
            var model         = new ApplicationUser();
            ViewBag.Employees = db.Employees.Where(e => e.EmployeeUsers == null).ToList();
            return View(model);
        }

        // GET: Security/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Security/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
