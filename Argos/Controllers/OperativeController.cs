using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Operative;
using Argos.Support;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Argos.ViewModels.Operative;
using System;
using Argos.Models.Transaction;

namespace Argos.Controllers
{
    [Authorize]
    public class OperativeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Service
        public ActionResult Accounts()
        {
            var model = new AccountSearchViewModel();
            model.AccountTypes = db.AccountTypes.ToSelectList();
            model.Status = db.OperativeStatus.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchAccounts(string client,string phone, int? typeId, int? statusId)
        {
            //si el nombre de cliente vien con datos divido todas las palabras
            var arClient = new List<string>().ToArray();

            if(client != null && client != string.Empty)
                arClient =  client.Split(' ');

            var model = (from s in db.Accounts
                         where (client == string.Empty || arClient.All(w => s.Client.Name.Contains(w))) &&
                               (phone == string.Empty || s.Client.Phone == phone) &&
                               (typeId == null || s.AccountTypeId == typeId) &&
                               (statusId == null || s.StatusId == statusId) 
                         select s).ToList();

            return PartialView("_AccountList", model);
        }

        [HttpPost]
        public ActionResult BeginAccount()
        {
            var model = new BeginAccountViewModel();
            model.AccountTypes = db.AccountTypes.ToSelectList();
            

            return PartialView("_BeginAccount",model);
        }

        [HttpPost]
        public ActionResult CreateAccount(BeginAccountViewModel model)
        {
            return View();
        }


        public ActionResult ManageAccount(int id)
        {

            Account model = null;

            return View(model);
        }


     

        [HttpPost]
        public ActionResult GetClientAddress(int clientId)
        {
            LocatableEntity model = db.Clients.Include(c => c.City).FirstOrDefault(c => c.ClientId == clientId);
            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = db.Cities.ToSelectList();

            return PartialView("_AccountAddress", model);
        }


        // GET: Service/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
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

        // GET: Service/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Service/Edit/5
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

        // GET: Service/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Service/Delete/5
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
