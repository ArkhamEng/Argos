using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Production;
using Argos.Support;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Argos.ViewModels.Operative;
using System;
using Argos.Models.Production;
using Argos.Models.Enums;

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
        public ActionResult SearchAccounts(string client, string phone, int? typeId, int? statusId)
        {
            //si el nombre de cliente vien con datos divido todas las palabras
            var arClient = new List<string>().ToArray();

            if (client != null && client != string.Empty)
                arClient = client.Split(' ');

            var model = (from s in db.Accounts.Include(a=> a.AccountType)
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


            return PartialView("_BeginAccount", model);
        }

        [HttpPost]
        public ActionResult CreateAccount(BeginAccountViewModel model)
        {
            try
            {
                var code = db.AccountTypes.Find(model.AccountTypeId).Code;

                var account = new Account
                {
                    AccountTypeId = model.AccountTypeId,
                    ClientId = model.ClientId,
                    HireDate = model.HireDate,
                    HirePrice = model.HirePrice,
                    StatusId = 0,
                    Code = Extens.GetCode(code, model.HireDate, (int)decimal.Zero)
                };

                db.Accounts.Add(account);
                db.SaveChanges();

                return Json(new JResponse { Id = account.AccountId, Result = Cons.ResponseSuccess });
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Body = ex.Message,
                    Header = "Ocurrio un error al registrar la cuenta"
                });
            }
        }


        public ActionResult ManageAccount(int id)
        {
            //obtengo datos de la cuenta
            var account = db.Accounts.Include(a => a.Client).Include(a => a.AccountFile).Include(a => a.Location).
                Include(a=> a.AccountType).Include(a => a.Location.City).Include(a => a.Policy).
                Include(a => a.Services).FirstOrDefault(a => a.AccountId == id);

            //genero el ViewModel para la pagina
            var model                          = new AccountViewModel(account);
            model.AccountTypes                 = db.AccountTypes.ToSelectList();
            model.LocationViewModel.States     = db.States.ToSelectList();
            model.LocationViewModel.Cities     = new SelectList(new List<City>());
            model.PolicyViewModel.PolicyStatus = db.OperativeStatus.ToSelectList();

            //si la ubicacón tiene datos lleno el combo de ciudades en base al estado
            if (model.LocationViewModel.Location != null)
                model.LocationViewModel.Cities = db.Cities.Where(c => c.StateId == model.LocationViewModel.Location.City.StateId).ToSelectList();
            else
                model.LocationViewModel.Location = new AccountLocation();

            return View(model);
        }




        [HttpPost]
        public ActionResult GetClientAddress(int clientId)
        {
            LocatableEntity model = db.Persons.Include(c => c.City).FirstOrDefault(c => c.PersonId == clientId);
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
