using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Operative;
using Argos.Support;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Argos.ViewModels.Operative;
using System;

namespace Argos.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Service
        public ActionResult Accounts()
        {
            var model = new AccountSearchViewModel();
            model.ServiceTypes  = db.ServiceTypes.ToSelectList();
            model.ServiceStatus = db.ServiceStatus.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchAccounts(string client,string phone, int? serviceTypeId, int? serviceStatusId)
        {
            //si el nombre de cliente vien con datos divido todas las palabras
            var arClient = new List<string>().ToArray();

            if(client != null && client != string.Empty)
                arClient =  client.Split(' ');

            var model = (from s in db.ServiceAccounts
                         where (client == string.Empty || arClient.All(w => s.Client.Name.Contains(w))) &&
                               (phone == string.Empty || s.Client.Phone == phone) &&
                               (serviceTypeId == null || s.ServiceTypeId == serviceTypeId) &&
                               (serviceStatusId == null || s.ServiceStatusId == serviceStatusId) 
                         select s).ToList();

            return PartialView("_AccountList", model);
        }

        [HttpPost]
        public ActionResult BeginAccount()
        {
            var model = new BeginAccountViewModel();
            model.ServiceTypes = db.ServiceTypes.ToSelectList();
            

            return PartialView("_BeginAccount",model);
        }

        [HttpPost]
        public ActionResult CreateAccount(BeginAccountViewModel model)
        {
            try
            {
                //datos básicos de la cuenta
                var account = new ServiceAccount
                {
                    ClientId = model.ClientId,
                    ServiceTypeId = model.ServiceTypeId,
                    HirePrice = model.HirePrice,
                    HireDate = model.HireDate,
                    ServiceStatusId = Common.One,
                };

                //obtengo los datos del servicio seleccionado
                var service = db.ServiceTypes.Find(model.ServiceTypeId);

                //Obtengo el nombre corto del tipo de servicio y genero un código provisional
                account.Code = Common.GetCode(service.ShortName, Common.Zero);

                //guardo los datos básicos
                db.ServiceAccounts.Add(account);
                db.SaveChanges();

                //si el servicio require una dirección de instalación
                //ocupo por defecto la dirección del cliente
                if (service.IsLocatable)
                {
                    //obtengo la direccion del client para usarla por defecto
                    var address = (LocatableEntity)db.Clients.Find(model.ClientId);

                    var accountAddress = new AccountAddress
                    {
                        //la relacion cuenta y direccion de cuenta es 1 a 1 por lo tanto debo tomar
                        //el id generado al crear la cuenta
                        AccountAddressId = account.ServiceId,
                        Location = address.Location,
                        CityId = address.CityId,
                        InNumber = address.InNumber,
                        OutNumber = address.OutNumber,
                        Street = address.Street,
                        ZipCode = address.ZipCode
                    };

                    db.AccountAddresses.Add(accountAddress);
                    db.SaveChanges();
                }

                return Json(new JResponse
                {
                    Id = account.ServiceId,
                    Result = Common.ResponseSuccess,
                });

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Common.ResponseDanger,
                    Body = String.Format("Detalle del error {0}", ex.Message),
                    Header = "Error al crear la cuenta"
                });
            }
        }


        public ActionResult ManageAccount(int id)
        {

            var model = db.ServiceAccounts.
                Where(a => a.ServiceId == id).Include(a=> a.AccountAddress).Include(a=> a.AccountAddress.City.State).
                Include(a=> a.ServiceType).Include(a=> a.ServiceStatus).Include(a=> a.Client).FirstOrDefault();


            ViewBag.ServiceTypes = db.ServiceTypes.ToSelectList();
            ViewBag.States       = db.States.ToSelectList();
            ViewBag.Cities       = db.Cities.Where(c=> c.StateId == model.Client.City.StateId).ToSelectList();
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
