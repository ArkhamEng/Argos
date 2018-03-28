using Argos.Models;
using Argos.Models.BaseTypes;
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
            var model         = new ApplicationUser();
            ViewBag.Employees = db.Employees.Where(e => e.EmployeeUsers == null).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult BeginCreateUser(int id, bool isEmployee)
        {
            try
            {
                RegisterViewModel vm = null;
                if (isEmployee)
                {
                    var employee = db.Employees.Find(id);
                    vm = new RegisterViewModel
                    {
                        Id = employee.EmployeeId,
                        Name = employee.Name,
                        Email = employee.Email,
                        Phone = employee.Phone,
                        UserName = employee.Email != null ? employee.Email.Split('@').First() : employee.Name
                    };
                }
                else
                {
                    var client = db.Clients.Find(id);
                    vm = new RegisterViewModel
                    {
                        Id = client.ClientId,
                        Name = client.Name,
                        Email = client.Email,
                        Phone = client.Phone,
                        UserName = client.Email != null ? client.Email.Split('@').First() : client.Name
                    };
                }

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
