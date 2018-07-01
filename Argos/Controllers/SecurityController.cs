using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.Business;
using Argos.Models.Operative;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.Controllers
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
                vm = new RegisterViewModel
                {
                    //Id = person.PersonId,
                    //Name = person.Name,
                    //Email = person.Email,
                    //Phone = person.Phone,
                    //UserName = person.Email != null ? person.Email.Split('@').First() : person.Name
                };

                return PartialView("_RegistUser", vm);
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
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
