using Argos.Common.Constants;
using Argos.Data;
using Argos.Data.Context;
using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.Business;
using Argos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Argos.Web.Controllers
{
    [Authorize]
    public class SecurityController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Security
        public ActionResult Users()
        {
            var model = new List<ApplicationUser>();

            return View(model);
        }


        public ActionResult BeginAddUser(int id)
        {
            var model = new ApplicationUser();
            ViewBag.Employees = db.Entities.OfType<Person>().Where(e => e.SystemUser == null).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult BeginCreateUser(int id)
        {
            try
            {
                RegisterViewModel vm = null;

                var person = db.Entities.OfType<Person>().FirstOrDefault(p=> p.EntityId ==id);
                var email = person.EmailAddresses.FirstOrDefault(e => e.EmailTypeId == Common.Enums.EmailTypes.Main).Email ?? string.Empty;
                var phone = person.PhoneNumbers.FirstOrDefault(e => e.PhoneTypeId == Common.Enums.PhoneTypes.Main).Phone ?? string.Empty;

                vm = new RegisterViewModel
                {
                    Id    = person.EntityId,
                    Name  = person.Name,
                    Email = email,
                    Phone = phone,
                    UserName = !string.IsNullOrEmpty(email) ? email.Split('@').First() : person.Name
                };

                return PartialView("_RegistUser", vm);
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Responses.Danger,
                    Header = "Error al obtener el empleado",
                    Body = string.Format("Ocurrio un error al ontener los datos para el usuario detalle:{0}", ex.Message),
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
