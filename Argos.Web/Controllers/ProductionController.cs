using Argos.Common.Constants;
using Argos.Data.Context;
using Argos.Models;
using Argos.Models.Analytic;
using Argos.Models.BaseTypes;
using Argos.Models.Production;
using Argos.Support;
using Argos.Web.Support;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Argos.Web.Controllers
{
    [CustomAuthorize]
    public class ProductionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Production
        public ActionResult Accounts()
        {
            var model = db.Accounts.Where(ac=> ac.IsActive).Take(Numbers.Config.MaxSearchRows).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchAccounts(string filter)
        {

            var model = (from ac in db.Accounts
                         where (string.IsNullOrEmpty(filter) || (ac.Client.Name + ac.Code + ac.AccountType.Name).Contains(filter)) &&
                               (ac.IsActive)

                         select ac).Take(Numbers.Config.MaxSearchRows).ToList();

            return PartialView("_AccountList", model);
        }


        public ActionResult Account(int? id)
        {
            Account model = new Models.Production.Account();
            if (id != null)
            {
                model = db.Accounts.Include(a=> a.AccountHistories).FirstOrDefault(a => a.AccountId == id);

                var occurrences = new List<Occurence>();

                //ultimos 20 servicios
                var services = db.Occurences.OfType<Service>().Where(c => c.AccountId == model.AccountId).OrderByDescending(s => s.ScheduleDate).
                               Take(Numbers.Config.AutoCompleateRows).ToList();

                //ultimos 20 Cargos
                var charges = db.Occurences.OfType<Charge>().Where(c=> c.AccountId == model.AccountId).OrderByDescending(c => c.ScheduleDate).
                               Take(Numbers.Config.AutoCompleateRows).ToList();

                occurrences.AddRange(services);
                occurrences.AddRange(charges);

                model.Occurences = occurrences;

                model.AccountHistories.OrderByDescending(h => h.InsDate);
            }
            else
            {
                model.Occurences = new List<Occurence>();
                model.AccountHistories = new List<AccountHistory>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAccount(Account account)
        {
            try
            {
                if (account.AccountId == Numbers.Zero)
                {
                    var typeCode = (AccountType)AppCache.Instance.AccountTypes.FirstOrDefault(t => ((AccountType)t).AccountTypeId == account.AccountTypeId);

                    var lastAccount = db.Accounts.FirstOrDefault(a => a.AccountTypeId == account.AccountTypeId);

                    var sequential = lastAccount != null ? lastAccount.Sequential + Numbers.One : Numbers.One;

                    account.Sequential = sequential;
                    account.Code = string.Format(Formats.AccoutMask, typeCode.Code, sequential.ToString(Formats.Sequential));
                    account.AccountStatusId = Common.Enums.StatusAccount.Active;
                    account.HasMaintenance = (account.MaintPeriodId > Numbers.Zero);
                    account.HasPolicy = (account.PaymentPeriodId > Numbers.Zero);

                    account.CutOffDate = DateTime.Now.ToLocal();

                    account.Occurences = TaskManager.Instance.CreateOccerrences(account);

                    db.Accounts.Add(account);
                }
                else
                {
                    var original = db.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);

                    var history = new AccountHistory
                    {
                        InsDate = original.UpdDate,
                        InsUser = original.UpdUser,
                        AccountId = original.AccountId,
                        Comment = original.Comment,
                        HasMaintenance = original.HasMaintenance,
                        HasPolicy = original.HasPolicy,
                        MaintenancePeriod = original.MaintPeriodId,
                        PaymentPeriod = original.PaymentPeriodId,
                        PolicyCost = original.PolicyCost,
                        Status = original.AccountStatus.Name
                    };

                    db.AccountHistories.Add(history);

                    original.Comment         = account.Comment;
                    original.PaymentPeriodId = account.PaymentPeriodId;
                    original.MaintPeriodId   = account.MaintPeriodId;
                    original.PolicyCost      = account.PolicyCost;
                    original.UpdDate         = DateTime.Now.ToLocal();
                    original.UpdUser         = HttpContext.User.Identity.Name;


                    db.Entry(original);
                   
                }

               db.SaveChanges();

                return Json(new JResponse
                {
                    Code   = Responses.Codes.Success,
                    Result = Responses.Success,
                    Id     = account.AccountId
                });
            }
            catch (Exception ex)
            {
                db.ErrorLogs.Add(new ErrorLog { Action = "SaveAccount", Controller = GetType().Name, Description = ex.Message });
                db.SaveChanges();

                return Json(new JResponse
                {
                    Code = Argos.Common.Constants.Responses.Codes.ServerError,
                    Result = Argos.Common.Constants.Responses.Danger,
                    Body = "Detalle del error:" + ex.InnerException ?? ex.Message,
                    Header = "Error al guardar los datos"
                });
            }
        }


        // GET: Services
        public ActionResult Services()
        {
            //obtengo los servicios del mes actual y del siguiente
            var model = db.Occurences.OfType<Service>().Include(s=> s.Account.Client).
                        Where(s=> !s.IsDone && s.IsActive).Take(Numbers.Config.MaxSearchRows).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchServices(string filter)
        {

            bool? done = string.IsNullOrEmpty(filter) ? false : new Nullable<bool>();


            var model = (from srv in db.Occurences.OfType<Service>().Include(s => s.Account.Client)

                         where (string.IsNullOrEmpty(filter) || (srv.Account.Client.Name + " " + srv.Account.Code).Contains(filter)) &&
                               (done == null || !srv.IsDone) && 
                               (srv.IsActive) 

                         select srv).OrderBy(c => c.ScheduleDate).Take(Numbers.Config.MaxSearchRows).ToList();

            return PartialView("_ServiceList", model);

        }


        // GET: Services
        public ActionResult Charges()
        {
            var month = DateTime.Now.ToLocal().Month;
            var year = DateTime.Now.ToLocal().Year;

            //obtengo los servicios del mes actual y del siguiente
            //var model = db.Occurences.OfType<Charge>().Include(s => s.Account.Client).
            //            Where(s => !s.IsDone && s.ScheduleDate.Month >= month && s.ScheduleDate.Year == year).ToList();

            var model = db.Occurences.OfType<Charge>().Include(s => s.Account.Client).
                Where(s => !s.IsDone && s.IsActive).OrderByDescending(s=> s.ScheduleDate).Take(Numbers.Config.MaxSearchRows).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchCharges(string filter)
        {
            bool? done = string.IsNullOrEmpty(filter) ? false : new Nullable<bool>();


            var model = (from crs in db.Occurences.OfType<Charge>().Include(s => s.Account.Client)

                         where (string.IsNullOrEmpty(filter)  || (crs.Account.Client.Name + " " + crs.Account.Code).Contains(filter)) &&
                               (done == null  || !crs.IsDone) &&
                               (crs.IsActive)

                         select crs).OrderBy(c => c.ScheduleDate).Take(Numbers.Config.MaxSearchRows).ToList();

            return PartialView("_ChargeList", model);
        }

        [HttpPost]
        public ActionResult GetService(int id)
        {
            var model = db.Occurences.OfType<Service>().FirstOrDefault(c => c.OccurenceId == id);
            return PartialView("_ServiceEdit", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveService(Service service)
        {
            try
            {
                if (service.OccurenceId > Numbers.Zero)
                {
                    if (service.IsDone)
                    {
                        service.StartDate = DateTime.Now.ToLocal();
                        service.CompletionDate = DateTime.Now.ToLocal();
                    }
                        

                    db.Services.Attach(service);

                    db.Entry(service).Property(c => c.UpdDate).IsModified = true;
                    db.Entry(service).Property(c => c.UpdUser).IsModified = true;
                    db.Entry(service).Property(c => c.Comment).IsModified = true;
                    db.Entry(service).Property(c => c.IsDone).IsModified = true;
                    db.Entry(service).Property(c => c.StartDate).IsModified = true;
                    db.Entry(service).Property(c => c.CompletionDate).IsModified = true;
                }

                db.SaveChanges();

                return Json(new JResponse
                {
                    Code = Responses.Codes.Success,
                    Result = Responses.Success,
                    Body = "Se completo el seguimiento del servicio",
                    Header = "Servicio Concluido"
                });
            }
            catch (Exception ex)
            {
                db.ErrorLogs.Add(new ErrorLog { Action = "SaveService", Controller = GetType().Name, Description = ex.Message });
                db.SaveChanges();

                return Json(new JResponse
                {
                    Code = Argos.Common.Constants.Responses.Codes.ServerError,
                    Result = Argos.Common.Constants.Responses.Danger,
                    Body = "Detalle del error:" + ex.InnerException ?? ex.Message,
                    Header = "Error al guardar los datos"
                });
            }
        }


        [HttpPost]
        public ActionResult GetCharge(int id)
        {
            var model = db.Occurences.OfType<Charge>().FirstOrDefault(c => c.OccurenceId == id);
            return PartialView("_ChargeEdit", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCharge(Charge charge)
        {
            try
            {
                if (charge.OccurenceId > Numbers.Zero)
                {
                    if (charge.IsDone)
                        charge.PaymentDate = DateTime.Now.ToLocal();

                    db.Charges.Attach(charge);

                    db.Entry(charge).Property(c => c.UpdDate).IsModified = true;
                    db.Entry(charge).Property(c => c.UpdUser).IsModified = true;
                    db.Entry(charge).Property(c => c.Comment).IsModified = true;
                    db.Entry(charge).Property(c => c.IsDone).IsModified = true;
                    db.Entry(charge).Property(c => c.PaymentDate).IsModified = true;
                }

                db.SaveChanges();

                return Json(new JResponse
                {
                    Code = Responses.Codes.Success,
                    Result = Responses.Success,
                    Body = "Se realizaron los cambios sobre el cargo",
                    Header = "Pago registrado"
                });
            }
            catch (Exception ex)
            {
                db.ErrorLogs.Add(new ErrorLog { Action = "SaveService", Controller = GetType().Name, Description = ex.Message });
                db.SaveChanges();

                return Json(new JResponse
                {
                    Code = Argos.Common.Constants.Responses.Codes.ServerError,
                    Result = Argos.Common.Constants.Responses.Danger,
                    Body = "Detalle del error:" + ex.InnerException ?? ex.Message,
                    Header = "Error al guardar los datos"
                });
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
