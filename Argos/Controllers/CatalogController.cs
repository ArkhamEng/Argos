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
using Argos.ViewModels.Generic;
using Argos.Models.Operative;
using Argos.Models.BusinessEntity;

namespace Argos.Controllers
{
    [CustomAuthorize]
    public partial class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Client Methods
        public ActionResult Clients()
        {
            var model = new PersonFilterViewModel<Client>();
            model.States = db.States.ToSelectList();
           
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchClients(string ftr, string name, string stateId, string townId,int? id)
        {
            var model = db.Persons.OfType<Client>().Where(c=> (ftr == string.Empty || ftr == null || c.FTR == ftr) 
            && (id==null || c.PersonId == id)
            && (name == string.Empty || name == null || c.Name.Contains(name))
            && (townId == null || townId == string.Empty || c.TownId == townId)
            && (stateId == null || stateId == string.Empty || c.Town.StateId == stateId)
            && c.IsActive).Include(c => c.Town).ToList();

            return PartialView("_ClientList",model);
        }

        [HttpPost]
        public ActionResult BeginAddClient()
        {
            var model = new PersonViewModel<Client>();

            model.States = db.States.ToSelectList();
            model.Cities = new List<Town>().ToSelectList();

            return PartialView("_ClientEdit",model);
        }

        [HttpPost]
        public ActionResult AddClient(Client client)
        {
            try
            {
                db.Persons.Add(client);
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
                Id = client.PersonId
            });
        }

        [HttpPost]
        public ActionResult BeginUpdateClient(int id)
        {
            try
            {
                var model = new PersonViewModel<Client>();
               
                model.Entity = db.Persons.OfType<Client>().Include(c=> c.Town).
                    FirstOrDefault(c => c.PersonId == id && c.IsActive);

                if (model != null)
                {
                    var json = EvalLock(model.Entity);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model.Entity).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model.Entity).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();

                    model.States = new SelectList(db.States,nameof(State.StateId),nameof(State.Name), model.Entity.Town.StateId);
                    model.Cities = db.Towns.Where(c => c.StateId == model.Entity.Town.StateId).ToSelectList();

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
                Id = client.PersonId
            });
        }

        [HttpPost]
        public ActionResult UnLockPerson(int id)
        {
            try
            {
                var person = db.Persons.Find(id);

                if(!person.IsLocked)
                {
                    person.LockUser = null;
                    person.LockEndDate = null;
                    db.Entry(person).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(person).Property(c => c.LockUser).IsModified = true;

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
        public ActionResult DeletePerson(int id)
        {
            Person person = null;
            try
            {
                person = db.Persons.FirstOrDefault(c=> c.PersonId == id && c.IsActive);

                if(person!=null)
                {
                    person.UnLock();
                    person.IsActive = false;

                    db.Entry(person).Property(c => c.UpdUser).IsModified = true;
                    db.Entry(person).Property(c => c.UpdDate).IsModified = true;
                    db.Entry(person).Property(c => c.IsActive).IsModified = true;

                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Cons.ResponseWarning,
                        Header = "No existe el registro a eliminar!",
                        Body = string.Format("Este registro ya no esta activo en el catálgo")                        
                    });
                }
              
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error al eliminar el registro",
                    Body = string.Format("Ocurrio un error al eliminar el registro detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = Cons.ResponseSuccess,
                Header = "Registro eliminado!",
                Body = string.Format("El registro {0} fue eliminado del catálogo", person.Name),
                Id = person.PersonId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleatePerson(string filter)
        {
            var clients = db.Persons.Where(c => c.Name.Contains(filter)).OrderBy(c => c.Name).Take(Cons.AutoCompleateRows).
                Select(c => new { Label = c.Name, Id = c.PersonId, Value = c.FTR });

            return Json(clients);
        }

        #endregion

        #region Employee Methods
        public ActionResult Employees()
        {
            var model = new PersonFilterViewModel<Employee>();
            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchEmployees(string ftr, string name, string stateId,string townId, int? id)
        {
            var model = db.Persons.OfType<Employee>().Include(e=> e.JobPosition).Where(e => (ftr == string.Empty || ftr == null || e.FTR == ftr)
            && (id == null || e.PersonId == id)
            && (name == string.Empty || name == null || e.Name.Contains(name))
            && (townId == null || townId == string.Empty || e.TownId == townId)
            && (stateId == null || stateId == string.Empty || e.Town.StateId == stateId)
            && e.IsActive).Include(c => c.Town).Include(e=> e.SystemUser).ToList();

            return PartialView("_EmployeeList", model);
        }

        [HttpPost]
        public ActionResult BeginAddEmployee()
        {
            var model = new PersonViewModel<Employee>();
            model.Entity = new Employee { Gender = Cons.MaleChar, HireDate=DateTime.Now.TodayLocal() };

            model.States = db.States.ToSelectList();
            model.JobPositions = db.JobPositions.ToSelectList();

            return PartialView("_EmployeeEdit", model);
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                db.Persons.Add(employee);
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
                Id = employee.PersonId
            });
        }
        [HttpPost]
        public ActionResult BeginUpdateEmployee(int id)
        {
            try
            {
                var model = new PersonViewModel<Employee>();

                model.Entity = db.Persons.OfType<Employee>().Include(e => e.Town).Include(e=>e.JobPosition).
                                            FirstOrDefault(e => e.PersonId == id && e.IsActive);

                if (model != null)
                {
                    var json = EvalLock(model.Entity);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model.Entity).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model.Entity).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();
                
                    model.States    = new SelectList(db.States, nameof(State.StateId), nameof(State.Name), model.Entity.Town.StateId);
                    model.Cities    = db.Towns.Where(c => c.StateId == model.Entity.Town.StateId).ToSelectList();
                    model.JobPositions = db.JobPositions.ToSelectList();

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
                Id = employee.PersonId
            });
        }

     
        #endregion

        #region Supplier Methods
        public ActionResult Suppliers()
        {
            var model = new PersonFilterViewModel<Supplier>();
            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchSuppliers(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = db.Persons.OfType<Supplier>().Where(c => (ftr == string.Empty || ftr == null || c.FTR == ftr)
            && (id == null || c.PersonId == id)
            && (name == string.Empty || name == null || c.Name.Contains(name))
            && (townId == null || townId == string.Empty || c.TownId == townId) 
            && (stateId == null || stateId == string.Empty || c.Town.StateId == stateId)
            && c.IsActive).Include(c => c.Town).ToList();

            return PartialView("_SupplierList", model);
        }

        [HttpPost]
        public ActionResult BeginAddSupplier()
        {
            var model = new PersonViewModel<Supplier>();

            model.States = db.States.ToSelectList();

            return PartialView("_SupplierEdit", model);
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {
            try
            {
                db.Persons.Add(supplier);
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
                Id = supplier.PersonId
            });
        }


        [HttpPost]
        public ActionResult BeginUpdateSupplier(int id)
        {
            try
            {
                var model = new PersonViewModel<Supplier>();
                    
                 model.Entity =  db.Persons.OfType<Supplier>().Include(s => s.Town).FirstOrDefault(s => s.PersonId == id && s.IsActive);

                if (model != null)
                {
                    var json = EvalLock(model.Entity);

                    if (json != null)
                        return json;

                    //bloqueo el registro
                    db.Entry(model.Entity).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(model.Entity).Property(c => c.LockUser).IsModified = true;

                    db.SaveChanges();

                    model.States = new SelectList(db.States, nameof(State.StateId), nameof(State.Name), model.Entity.Town.StateId);
                    model.Cities = db.Towns.Where(c => c.StateId == model.Entity.Town.StateId).ToSelectList();

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
        public ActionResult UpdateSupplier(Supplier supplier)
        {
            try
            {
                supplier.UnLock();

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
                Id = supplier.PersonId
            });
        }


        [HttpPost]
        public ActionResult DeleteSupplier(int id)
        {
            Supplier provider = new Supplier();
            try
            {
                provider = db.Persons.OfType<Supplier>().FirstOrDefault(c => c.PersonId == id && c.IsActive);

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
                Id = provider.PersonId
            });
        }


        #endregion


        private JsonResult EvalLock(AuditableCatalog model)
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
                model.Lock();
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