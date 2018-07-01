using Argos.Models;
using System.Linq;
using System.Web.Mvc;
using Argos.Support;
using System.Data.Entity;

namespace Argos.Controllers
{
    public class ConfigurationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        #region Common Methods
        [HttpPost]
        public JsonResult GetCities(string id)
        {
            var cities = db.Towns.Where(c => c.StateId == id).OrderBy(c => c.Name).ToSelectList();

           return Json(cities);
        }

        [HttpPost]
        public JsonResult GetSubCategories(int id)
        {
            var cities = db.SubCategories.Where(c => c.CategoryId == id).OrderBy(c => c.Name).ToSelectList();

            return Json(cities);
        }

        [HttpPost]
        public ActionResult CompleateSettlement(string filter)
       {
            var Settlements = db.Settlements.Include(s=> s.Town).
                Where(c =>  (c.Type +" "+ c.Name).Contains(filter)).
                OrderBy(c => c.Town.Name).Take(Cons.AutoCompleateRows).Take(50).
                Select(c => new { Label =  c.Type + " " + c.Name,
                    Id = c.SettlementId, Category = c.Town.State.Name+", "+ c.Town.Name, value= c.Type + " " + c.Name });

            return Json(Settlements);
        }

        [HttpPost]
        public ActionResult CompleateAddress(string filter)
        {
            var Settlements = db.Settlements.Include(s => s.Town).
                Where(c => (c.Type + " " + c.Name).Contains(filter)).
                OrderBy(c => c.Town.Name).Take(Cons.AutoCompleateRows).Take(50).
                Select(c => new {
                    value = c.Type + " " + c.Name,
                    Id = c.SettlementId,
                    data =new { category = c.Town.State.Name + ", " + c.Town.Name },
                }).ToList();

            var result = new { suggestions =  Settlements };

            return Json(result);
        }

        [HttpPost]
        public ActionResult AutoCompleateCode(string filter)
        {
            var clients = db.Settlements.Include(s => s.Town).
                Where(c => c.Code.Contains(filter)).
                OrderBy(c => c.Name).Take(Cons.AutoCompleateRows).
                Select(c => new { Label =  c.Type + " " + c.Name, Id = c.SettlementId, Value = c.Code });


            return Json(clients);
        }

        [HttpPost]
        public ActionResult SettlementSelected(int id)
        {
            var settlement = db.Settlements.Include(s => s.Town).FirstOrDefault(c => c.SettlementId == id);

            var model = new
            {
                Towns   = db.Towns.Where(t => t.StateId == settlement.Town.StateId).ToSelectList(),
                Location = settlement.Type + " " + settlement.Name,
                ZipCode  = settlement.Code,
                TownId   = settlement.TownId,
                StateId  = settlement.Town.StateId
            };

            return Json(model);
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
