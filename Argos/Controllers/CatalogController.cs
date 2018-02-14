using System.Linq;
using Argos.Models;
using System.Web.Mvc;
using Argos.Models.Catalog;
using Argos.Support;
using Argos.Models.Config;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using Argos.Models.BaseTypes;

namespace Argos.Controllers
{

    public class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Client Methods
        public ActionResult Clients()
        {
            var model = new List<Client>();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();
          
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchClients(string ftr, string name, int? stateId, int? cityId,int? id)
        {
            var model = db.Clients.Where(c=> (ftr == string.Empty || ftr == null || c.FTR == ftr) 
            && (id==null || c.ClientId == id)
            && (name == string.Empty || name == null || c.Name.Contains(name))
            && (cityId == null || c.CityId == cityId) && (stateId == null || c.City.StateId == stateId) 
            && c.IsActive).Include(c => c.City).ToList();

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
                client.InsDate = DateTime.Now.ToLocal();
                client.UpdDate = DateTime.Now.ToLocal();
                client.UpdUser = User.Identity.Name;
                client.InsUser = User.Identity.Name;
                client.IsActive = true;

                db.Clients.Add(client);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse { Result = JResponse.Warning, Header = "Error al guardar el cliente",
                    Body = "Ocurrion un error al agregar el cliente " + ex.Message });
            }
            return Json(new JResponse
            {
                Result = JResponse.Success,
                Header = "Datos del cliente guardados",
                Body = string.Format("El cliente {0} fue agregado al catálogo", client.Name),
                Id = client.ClientId
            });
        }
        [HttpPost]
        public ActionResult BeginUpdateClient(int id)
        {
            try
            {
                var model = db.Clients.Include(c=> c.City).
                    FirstOrDefault(c => c.ClientId == id && c.IsActive);

                if (model != null)
                {
                    ViewBag.States = new SelectList(db.States,nameof(State.StateId),nameof(State.Name), model.City.StateId);
                    ViewBag.Cities = db.Cities.Where(c => c.StateId == model.City.StateId).ToSelectList();

                    return PartialView("_ClientEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = JResponse.Warning,
                        Header = "Cliente inexistente!",
                        Body = string.Format("Este cliente ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al eliminar el cliente",
                    Body = string.Format("Ocurrion un error al eliminar el cliente detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        public ActionResult UpdateClient(Client client)
        {
            try
            {
                client.UpdDate = DateTime.Now.ToLocal();
                client.UpdUser = User.Identity.Name;

                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al modificar el cliente",
                    Body = string.Format("Ocurrion un error al guardar los cambios del cliente {0}  detalle del error {1}",
                                        client.Name, ex.Message),
                });
            }
            return Json(new JResponse
            {
                Result = JResponse.Success,
                Header = "Datos del cliente guardados",
                Body = string.Format("Los datos del cliente {0} fueron modificados", client.Name),
                Id = client.ClientId
            });
        }


        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            Client client = new Client();
            try
            {
                client = db.Clients.FirstOrDefault(c=> c.ClientId == id && c.IsActive);

                if(client!=null)
                {
                    client.UpdUser = User.Identity.Name;
                    client.UpdDate = DateTime.Now.ToLocal();
                    client.IsActive = false;

                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = JResponse.Warning,
                        Header = "No existe el client a eliminar!",
                        Body = string.Format("Este cliente ya no esta activo en el catálgo"),
                        Id = client.ClientId
                    });
                }
              
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al eliminar el cliente",
                    Body = string.Format("Ocurrion un error al eliminar el cliente detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = JResponse.Success,
                Header = "Cliente eliminado!",
                Body = string.Format("El cliente {0} fue eliminado del catálogo", client.Name),
                Id = client.ClientId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleateClient(string filter)
        {
            var clients = db.Clients.Where(c => c.Name.Contains(filter)).OrderBy(c => c.Name).Take(20).
                Select(c => new { Label = c.Name, Id = c.ClientId, Value = c.FTR });

            return Json(clients);
        }

        #endregion

        #region Employee Methods
        public ActionResult Employees()
        {
            var model = new List<Employee>();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();
            ViewBag.JobPositions = db.JobPositions.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchEmployees(string ftr, string name, int? stateId, int? cityId, int? id)
        {
            var model = db.Employees.Where(e => (ftr == string.Empty || ftr == null || e.FTR == ftr)
            && (id == null || e.EmployeeId == id)
            && (name == string.Empty || name == null || e.Name.Contains(name))
            && (cityId == null || e.CityId == cityId) && (stateId == null || e.City.StateId == stateId)
            && e.IsActive).Include(c => c.City).Include(e=> e.EmployeeUsers).ToList();

            return PartialView("_EmployeeList", model);
        }

        [HttpPost]
        public ActionResult BeginAddEmployee()
        {
            var model = new Employee();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();
            ViewBag.JobPositions = db.JobPositions.ToSelectList();

            return PartialView("_EmployeeEdit", model);
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                employee.InsDate = DateTime.Now.ToLocal();
                employee.UpdDate = DateTime.Now.ToLocal();
                employee.UpdUser = User.Identity.Name;
                employee.InsUser = User.Identity.Name;
                employee.IsActive = true;

                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al guardar el empleado",
                    Body = "Ocurrion un error al agregar el empleado " + ex.Message
                });
            }
            return Json(new JResponse
            {
                Result = JResponse.Success,
                Header = "Datos del empleado guardados",
                Body = string.Format("El empleado {0} fue agregado al catálogo", employee.Name),
                Id = employee.EmployeeId
            });
        }
        [HttpPost]
        public ActionResult BeginUpdateEmployee(int id)
        {
            try
            {
                var model = db.Employees.Include(e => e.City).Include(e=>e.JobPosition).
                    FirstOrDefault(e => e.EmployeeId == id && e.IsActive);

                if (model != null)
                {                    
                    ViewBag.States       = new SelectList(db.States, nameof(State.StateId), nameof(State.Name), model.City.StateId);
                    ViewBag.Cities       = db.Cities.Where(c => c.StateId == model.City.StateId).ToSelectList();
                    ViewBag.JobPositions = db.JobPositions.ToSelectList();

                    return PartialView("_EmployeeEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = JResponse.Warning,
                        Header = "Empleado inexistente!",
                        Body = string.Format("Este empleado ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al obtener el empleado",
                    Body = string.Format("Ocurrion un error al ontener los datos del empleado detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                employee.UpdDate = DateTime.Now.ToLocal();
                employee.UpdUser = User.Identity.Name;

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al modificar el empleado",
                    Body = string.Format("Ocurrion un error al guardar los cambios del empleado {0}  detalle del error {1}",
                                        employee.Name, ex.Message),
                });
            }
            return Json(new JResponse
            {
                Result = JResponse.Success,
                Header = "Datos del empleado guardados",
                Body = string.Format("Los datos del empleado {0} fueron modificados", employee.Name),
                Id = employee.EmployeeId
            });
        }


        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            Employee employee = new Employee();
            try
            {
                employee = db.Employees.FirstOrDefault(e => e.EmployeeId == id && e.IsActive);

                if (employee != null)
                {
                    employee.UpdUser = User.Identity.Name;
                    employee.UpdDate = DateTime.Now.ToLocal();
                    employee.IsActive = false;

                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = JResponse.Warning,
                        Header = "No existe el empleado a eliminar!",
                        Body = string.Format("Este empleado ya no esta activo en el catálgo"),
                        Id = employee.EmployeeId
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
                    Header = "Error al eliminar el Employee",
                    Body = string.Format("Ocurrion un error al eliminar el Employee detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = JResponse.Success,
                Header = "Employee eliminado!",
                Body = string.Format("El Employee {0} fue eliminado del catálogo", employee.Name),
                Id = employee.EmployeeId
            });
        }
        #endregion



        [HttpPost]
        public ActionResult BeginCreateUser(int id,bool isEmployee)
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
                        UserName = employee.Email != null? employee.Email.Split('@').First(): employee.Name
                    };
                }
                else
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

                return PartialView("_RegistUser", vm);
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = JResponse.Warning,
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