using System.Linq;
using Argos.Models;
using System.Web.Mvc;
using Argos.Models.HumanResources;
using Argos.Support;
using Argos.Models.Config;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using Argos.Models.BaseTypes;
using Argos.Models.Transaction;

namespace Argos.Controllers
{
    [Authorize]
    public partial class CatalogController : Controller
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
                db.Clients.Add(client);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse { Result = Cons.ResponseDanger, Header = "Error al guardar el cliente",
                    Body = "Ocurrio un error al agregar el cliente " + ex.Message });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
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
                    var json = EvalLock(model);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();

                    ViewBag.States = new SelectList(db.States,nameof(State.StateId),nameof(State.Name), model.City.StateId);
                    ViewBag.Cities = db.Cities.Where(c => c.StateId == model.City.StateId).ToSelectList();

                    return PartialView("_ClientEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "Cliente inexistente!",
                        Body = string.Format("Este cliente ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el cliente",
                    Body = string.Format("Ocurrio un error al eliminar el cliente detalle del error:{0}", ex.Message),
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
                client.LockEndDate  = null;
                client.LockUser     = null;

                db.Entry(client).State = EntityState.Modified;
                db.Entry(client).Property(c => c.InsDate).IsModified = false;
                db.Entry(client).Property(c => c.InsUser).IsModified = false;
                db.Entry(client).Property(c => c.IsActive).IsModified = false;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al modificar el cliente",
                    Body = string.Format("Ocurrio un error al guardar los cambios del cliente {0},  detalle del error {1}",
                                        client.Name, ex.Message),
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Datos del cliente guardados",
                Body = string.Format("Los datos del cliente {0} fueron modificados", client.Name),
                Id = client.ClientId
            });
        }

        [HttpPost]
        public ActionResult UnLockClient(int id, string entity)
        {
            try
            {
                var model = db.Clients.Find(id);

                if(!model.IsLocked)
                {
                    model.LockUser = null;
                    model.LockEndDate = null;
                    db.Entry(model).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();
                }

                return Json(new JResponse
                {
                    Result = Cons.ResponseSuccess,
                    Header = "Registro desbloqueado",
                    Body = string.Format("El registro ha sido desbloqueado")
                });

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al desbloquear el resgistro",
                    Body = string.Format("Ocurrio un error al eliminar el cliente detalle del error:{0}", ex.Message)
                });
            }
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

                    db.Entry(client).Property(c => c.InsUser).IsModified = false;
                    db.Entry(client).Property(c => c.InsDate).IsModified = false;

                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "No existe el client a eliminar!",
                        Body = string.Format("Este cliente ya no esta activo en el catálgo")                        
                    });
                }
              
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el cliente",
                    Body = string.Format("Ocurrio un error al eliminar el cliente detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Cliente eliminado!",
                Body = string.Format("El cliente {0} fue eliminado del catálogo", client.Name),
                Id = client.ClientId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleateClient(string filter)
        {
            var clients = db.Clients.Where(c => c.Name.Contains(filter)).OrderBy(c => c.Name).Take(Cons.AutoCompleateRows).
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
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al guardar el empleado",
                    Body = "Ocurrio un error al agregar el empleado " + ex.Message
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
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
                    var json = EvalLock(model);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();
                
                    ViewBag.States       = new SelectList(db.States, nameof(State.StateId), nameof(State.Name), model.City.StateId);
                    ViewBag.Cities       = db.Cities.Where(c => c.StateId == model.City.StateId).ToSelectList();
                    ViewBag.JobPositions = db.JobPositions.ToSelectList();

                    return PartialView("_EmployeeEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "Empleado inexistente!",
                        Body = string.Format("Este empleado ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al obtener el empleado",
                    Body = string.Format("Ocurrio un error al ontener los datos del empleado, detalle del error:{0}", ex.Message),
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
                db.Entry(employee).Property(c => c.InsUser).IsModified = false;
                db.Entry(employee).Property(c => c.InsDate).IsModified = false;
                db.Entry(employee).Property(c => c.IsActive).IsModified = false;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al modificar el empleado",
                    Body = string.Format("Ocurrio un error al guardar los cambios del empleado {0}  detalle del error {1}",
                                        employee.Name, ex.Message),
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Datos del empleado guardados",
                Body = string.Format("Los datos del empleado {0} fueron modificados", employee.Name),
                Id = employee.EmployeeId
            });
        }

        [HttpPost]
        public ActionResult UnLockEmployee(int id)
        {
            try
            {
                var model = db.Employees.Find(id);

                if (!model.IsLocked)
                {
                    model.LockUser = null;
                    model.LockEndDate = null;
                    db.Entry(model).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();
                }

                return Json(new JResponse
                {
                    Result = Cons.ResponseSuccess,
                    Header = "Registro desbloqueado",
                    Body = string.Format("El registro ha sido desbloqueado")
                });

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al desbloquear el resgistro",
                    Body = string.Format("Ocurrio un error al eliminar el cliente detalle del error:{0}", ex.Message)
                });
            }
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

                    db.Entry(employee).Property(e => e.UpdUser).IsModified  = true;
                    db.Entry(employee).Property(e => e.UpdDate).IsModified  = true;
                    db.Entry(employee).Property(e => e.IsActive).IsModified = true;

                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "No existe el empleado a eliminar!",
                        Body = string.Format("Este empleado ya no esta activo en el catálgo")                        
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el Employee",
                    Body = string.Format("Ocurrio un error al eliminar el Employee detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Employee eliminado!",
                Body = string.Format("El Employee {0} fue eliminado del catálogo", employee.Name),
                Id = employee.EmployeeId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleatEmployee(string filter)
        {
            var employees = db.Employees.Where(c => c.Name.Contains(filter)).OrderBy(c => c.Name).Take(Cons.AutoCompleateRows).
                Select(c => new { Label = c.Name, Id = c.EmployeeId, Value = c.FTR });

            return Json(employees);
        }
        #endregion

        #region Supplier Methods
        public ActionResult Suppliers()
        {
            var model = new List<Supplier>();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchSuppliers(string ftr, string name, int? stateId, int? cityId, int? id)
        {
            var model = db.Suppliers.Where(c => (ftr == string.Empty || ftr == null || c.FTR == ftr)
            && (id == null || c.SupplierId == id)
            && (name == string.Empty || name == null || c.Name.Contains(name))
            && (cityId == null || c.CityId == cityId) && (stateId == null || c.City.StateId == stateId)
            && c.IsActive).Include(c => c.City).ToList();

            return PartialView("_SupplierList", model);
        }

        [HttpPost]
        public ActionResult BeginAddSupplier()
        {
            var model = new Supplier();

            ViewBag.States = db.States.ToSelectList();
            ViewBag.Cities = new List<City>().ToSelectList();

            return PartialView("_SupplierEdit", model);
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {
            try
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al guardar el proveedor",
                    Body = "Ocurrio un error al agregar el proveedor " + ex.Message
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Datos del proveedor guardados",
                Body = string.Format("El proveedor {0} fue agregado al catálogo", supplier.Name),
                Id = supplier.SupplierId
            });
        }


        [HttpPost]
        public ActionResult BeginUpdateSupplier(int id)
        {
            try
            {
                var model = db.Suppliers.Include(c => c.City).
                    FirstOrDefault(c => c.SupplierId == id && c.IsActive);

                if (model != null)
                {
                    var json = EvalLock(model);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();

                    ViewBag.States = new SelectList(db.States, nameof(State.StateId), nameof(State.Name), model.City.StateId);
                    ViewBag.Cities = db.Cities.Where(c => c.StateId == model.City.StateId).ToSelectList();

                    return PartialView("_SupplierEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "Proveedor inexistente!",
                        Body = string.Format("Este proveedor ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el proveedor",
                    Body = string.Format("Ocurrio un error al eliminar el proveedor detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        public ActionResult UpdateProvider(Supplier supplier)
        {
            try
            {
                supplier.UpdDate = DateTime.Now.ToLocal();
                supplier.UpdUser = User.Identity.Name;

                db.Entry(supplier).State = EntityState.Modified;
                db.Entry(supplier).Property(c => c.InsDate).IsModified = false;
                db.Entry(supplier).Property(c => c.InsUser).IsModified = false;
                db.Entry(supplier).Property(c => c.IsActive).IsModified = false;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al modificar el proveedor",
                    Body = string.Format("Ocurrio un error al guardar los cambios del proveedor {0}  detalle del error {1}",
                                        supplier.Name, ex.Message),
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Datos del proveedor guardados",
                Body = string.Format("Los datos del proveedor {0} fueron modificados", supplier.Name),
                Id = supplier.SupplierId
            });
        }

        [HttpPost]
        public ActionResult UnLockSupplier(int id)
        {
            try
            {
                var model = db.Suppliers.Find(id);

                if (!model.IsLocked)
                {
                    model.LockUser = null;
                    model.LockEndDate = null;
                    db.Entry(model).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();
                }

                return Json(new JResponse
                {
                    Result = Cons.ResponseSuccess,
                    Header = "Registro desbloqueado",
                    Body = string.Format("El registro ha sido desbloqueado")
                });

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al desbloquear el resgistro",
                    Body = string.Format("Ocurrio un error al eliminar el cliente detalle del error:{0}", ex.Message)
                });
            }
        }

        [HttpPost]
        public ActionResult DeleteSupplier(int id)
        {
            Supplier provider = new Supplier();
            try
            {
                provider = db.Suppliers.FirstOrDefault(c => c.SupplierId == id && c.IsActive);

                if (provider != null)
                {
                    provider.UpdUser = User.Identity.Name;
                    provider.UpdDate = DateTime.Now.ToLocal();
                    provider.IsActive = false;

                    db.Entry(provider).Property(p => p.UpdDate).IsModified = true;
                    db.Entry(provider).Property(p => p.UpdUser).IsModified = true;
                    db.Entry(provider).Property(p => p.IsActive).IsModified = true;
                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "No existe el proveedor a eliminar!",
                        Body = string.Format("Este proveedor ya no esta activo en el catálgo")
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el proveedor",
                    Body = string.Format("Ocurrio un error al eliminar el proveedor detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Proveedor eliminado!",
                Body = string.Format("El proveedor {0} fue eliminado del catálogo", provider.Name),
                Id = provider.SupplierId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleatSupplier(string filter)
        {
            var providers = db.Suppliers.Where(c => c.Name.Contains(filter)).OrderBy(c => c.Name).Take(Cons.AutoCompleateRows).
                Select(c => new { Label = c.Name, Id = c.SupplierId, Value = c.FTR });

            return Json(providers);
        }

        #endregion


        private JsonResult EvalLock(AuditableEntity model)
        {
            if (model.IsLocked)
            {
                var time = (model.LockEndDate.Value - DateTime.Now.ToLocal()).ToString("mm:ss");
                return Json(new JResponse
                {
                    Result = Cons.ResponseWarning,
                    Header = "Resistro bloqueado!",
                    Body = string.Format("Este registro ha sido bloqueado por  el usuario {0}, tiempo restante del bloqueo {1}", model.LockUser, time),
                });
            }
            else
            {
                model.LockUser    = User.Identity.Name;
                model.LockEndDate = DateTime.Now.ToLocal().AddMinutes(Cons.LockDuration);
                return null;
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