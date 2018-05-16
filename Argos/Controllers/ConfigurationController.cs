using Argos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Argos.Support;

namespace Argos.Controllers
{
    public class ConfigurationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        #region Common Methods
        [HttpPost]
        public JsonResult GetCities(int id)
        {
            var cities = db.Cities.Where(c => c.StateId == id).ToSelectList();

           return Json(cities);
        }


        [HttpPost]
        public JsonResult GetModels(int id)
        {
            var cities = db.Models.Where(c => c.MakerId == id).ToSelectList();

            return Json(cities);
        }

        [HttpPost]
        public JsonResult GetYears(int id)
        {
            var cities = db.Compatibilities.Where(c => c.ModelId == id).ToSelectList();

            return Json(cities);
        }

        [HttpPost]
        public JsonResult GetProductSubcategories(int id)
        {
            var cities = db.SubCategories.Where(c => c.CategoryId == id).ToSelectList();

            return Json(cities);
        }

        #endregion

        // GET: Configuration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuration/Create
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

        // GET: Configuration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configuration/Edit/5
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

        // GET: Configuration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configuration/Delete/5
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
    }
}
