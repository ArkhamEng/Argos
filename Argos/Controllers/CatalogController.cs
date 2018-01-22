using System.Linq;
using Argos.Models;
using System.Web.Mvc;
using Argos.Models.Catalog;
using Argos.Support;
using Argos.Models.Config;
using System.Collections.Generic;
using System;
using System.Data.Entity;

namespace Argos.Controllers
{

    public class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Client Methods
        public ActionResult Clients()
        {
            var model = db.Clients.Include(c => c.City).ToList();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();
          
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchClients(string ftr, string name, int? stateId, int? cityId)
        {
            var model = db.Clients.Where(c=> (ftr == string.Empty || ftr == null || c.FTR == ftr) 
            && (name == string.Empty || name == null || c.Name.Contains(name))
            && (cityId == null || c.CityId == cityId) && (stateId == null || c.City.StateId == stateId)).Include(c => c.City).ToList();

            return PartialView("_ClientList",model);
        }

        [HttpPost]
        public ActionResult BeginAddClient()
        {
            var model = new Client();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();

            return PartialView("_ClientEdit",model);
        }

        [HttpPost]
        public ActionResult AddClient(Client client)
        {
            try
            {
                client.InsDate = DateTime.Now;
                client.UpdDate = DateTime.Now;
                client.UpdUser = "Administrador";
                client.InsUser = "Administrador";

                db.Clients.Add(client);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Header = "Error al guardar!",
                    Message = "Ocurrion un error al agregar el cliente " + ex.InnerException.Message });
            }
           

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = db.Cities.Where(c => c.CityId == client.CityId).ToSelectList();

            return PartialView("_ClientEdit", client);
        }
        #endregion

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