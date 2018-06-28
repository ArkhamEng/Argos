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
using Argos.Models.Enums;

namespace Argos.Controllers
{
    [CustomAuthorize]
    public partial class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddAddress()
        {
            var model = new AddressVm();
            model.Address = new Address();

            model.States = db.States.OrderBy(s => s.Name).ToSelectList();
            model.Types = db.AddressTypes.OrderBy(t => t.Name).ToSelectList();
            model.Towns = new List<Town>().ToSelectList();

            return PartialView("_Address", model);
        }

        #region Client Methods
        public ActionResult Clients()
        {
            var model = new PersonFilterViewModel<ClientVm>();
            model.Entities = db.Persons.OfType<Client>().OrderBy(c => c.Name).
                                Select(c => new ClientVm { Client = c }).ToList();

            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchClients(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = (from c in db.Persons.OfType<Client>()
                         where
                         (ftr == string.Empty || ftr == null || c.FTR == ftr)
                         && (id == null || c.PersonId == id)
                         && (name == string.Empty || name == null || c.Name.Contains(name))
                         && c.IsActive
                         select c).OrderBy(c => c.Name).Select(c => new ClientVm { Client = c }).ToList();

            return PartialView("_ClientList", model);
        }


        [HttpPost]
        public ActionResult BeginAddClient()
        {
            var model = new ClientVm();
            BeginAddPerson(model);
            return PartialView("_ClientEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateClient(int id)
        {
            try
            {
                var model = new ClientVm();

                model.Client = db.Persons.OfType<Client>().Include(c => c.Addresses).Include(c => c.Addresses.Select(a => a.Town.State)).
                    FirstOrDefault(c => c.PersonId == id && c.IsActive);

                BeginUpdatePerson(model);

                if (model != null)
                {
                    LockPerson(model);
                    return PartialView("_ClientEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Code = Codes.RecordNotFound,
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
                    Code = Codes.ServerError,
                    Result = Cons.ResponseDanger,
                    Header = "Error al obtener datos",
                    Body = string.Format("Ocurrio un error obtener los datos del cliente, detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveClient(ClientVm clientVm)
        {
            try
            {
                //update
                if (clientVm.Client.PersonId > Cons.Zero)
                {
                    UpdatePerson(clientVm);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Cliente Modificado",
                        Code = Codes.Success,
                        Body = "Se actualizaron los datos del cliente " + clientVm.Client.Name + " y se removio el bloqueo",
                        Id = clientVm.Client.PersonId
                    });
                }
                //insert
                else
                {
                    InsertPerson(clientVm);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Cliente Agregado",
                        Body = "Se agrego el cliente " + clientVm.Client.Name,
                        Id = clientVm.Client.PersonId,
                        Code = Codes.Success
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error Al guardar",
                    Body = "Ocurrio un error al guardar os datos del cliente " + ex.Message,
                    Code = Codes.ServerError
                });
            }
        }

        #endregion

        #region Person Methods

        private void BeginAddPerson(PersonVM model)
        {
            model.Addresses.Add(new AddressVm
            {
                Address = new Address(),
                States = db.States.ToSelectList(AddressTypes.Home),
                Types = db.AddressTypes.ToSelectList(),
                Towns = new SelectList(new List<Town>()),
                AddButton = Styles.BtnEdit,
                RemoveButton = Styles.BtnDeletetHidden
            });
        }

        private bool InsertPerson(PersonVM personVm)
        {
            try
            {
                db.Persons.Add(personVm.Person);
                db.SaveChanges();

                //si hay imagen, la guardo y guardo la ruta
                if (personVm.NewImages.Count > Cons.Zero)
                {
                    var file = personVm.NewImages.FirstOrDefault();
                    var path = Support.FileManager.SaveFile(file, personVm.Person.PersonId.ToString(), FileType.PersonImage);
                    personVm.Person.ImagePath = path;

                    db.Entry(personVm.Person).Property(c => c.ImagePath).IsModified = true;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdatePerson(PersonVM personVm)
        {
            try
            {
                foreach (var address in personVm.Person.Addresses)
                {
                    //si es nuevo registro lo agrego
                    if (address.AddressId == Cons.Zero)
                        db.Addresses.Add(address);
                    else
                    {
                        //de lo contrario actualizo
                        address.UnLock();
                        db.Entry(address).State = EntityState.Modified;
                        db.Entry(address).Property(c => c.InsDate).IsModified = false;
                        db.Entry(address).Property(c => c.InsUser).IsModified = false;
                    }
                }

                bool modifyImage = false;

                //si la imagen fue eliminada, la borro del disco
                if (personVm.DropImage && personVm.Person.ImagePath!= string.Empty)
                {
                    FileManager.DeleteImage(personVm.Person.ImagePath);
                    personVm.Person.ImagePath = null;
                    modifyImage = true;
                }


                //si hay imagen, la guardo y guardo la ruta
                if (personVm.NewImages.Count > Cons.Zero && personVm.NewImages.First()!=null)
                {
                    var file = personVm.NewImages.FirstOrDefault();
                    var path = Support.FileManager.SaveFile(file, personVm.Person.PersonId.ToString(), FileType.PersonImage);
                    personVm.Person.ImagePath = path;
                    modifyImage = true;
                }

                personVm.Person.UnLock();
                db.Entry(personVm.Person).State = EntityState.Modified;
                db.Entry(personVm.Person).Property(c => c.InsDate).IsModified = false;
                db.Entry(personVm.Person).Property(c => c.InsUser).IsModified = false;
                db.Entry(personVm.Person).Property(c => c.ImagePath).IsModified = modifyImage;


                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private JsonResult LockPerson(PersonVM model)
        {
            try
            {
                if (model.Person.IsLocked)
                {
                    var time = (model.Person.LockEndDate.Value - DateTime.Now.ToLocal()).ToString("mm:ss");
                    return Json(new JResponse
                    {
                        Code = Codes.RecordLocked,
                        Result = Cons.ResponseWarning,
                        Header = "Resistro bloqueado!",
                        Body = string.Format("Este registro ha sido bloqueado por  el usuario {0}, tiempo restante del bloqueo {1}", model.Person.LockUser, time),
                    });
                }

                //bloqueo el registro
                db.Entry(model.Person).Property(c => c.LockEndDate).IsModified = true;
                db.Entry(model.Person).Property(c => c.LockUser).IsModified = true;

                model.Addresses.ForEach(av =>
                {
                    av.Address.Lock();
                    db.Entry(av.Address).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(av.Address).Property(c => c.LockUser).IsModified = true;
                });

                db.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool BeginUpdatePerson(PersonVM model)
        {
            try
            {
                var states = db.States.ToSelectList();
                var types = db.AddressTypes.ToSelectList();

                int index = 0;

                foreach (var a in model.Person.Addresses)
                {
                    AddressVm avm = new AddressVm { Address = a };

                    if (index == Cons.Zero)
                    {
                        avm.AddButton = Styles.BtnEdit;
                        avm.RemoveButton = Styles.BtnDeletetHidden;
                    }
                    else
                    {
                        avm.AddButton = Styles.BtnEditHidden;
                        avm.RemoveButton = Styles.BtnDelete;
                    }

                    avm.SelectedStateId = a.Town.StateId;
                    avm.States = states.ToSelectList(avm.SelectedStateId);
                    avm.Towns = db.Towns.Where(t => t.StateId == avm.SelectedStateId).ToSelectList(a.TownId);
                    avm.Types = types.ToSelectList((int)a.AddressTypeId);

                    model.Addresses.Add(avm);
                    index++;
                }

                
             
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UnLockPerson(int id)
        {
            try
            {
                var person = db.Persons.Include(p => p.Addresses).FirstOrDefault(p => p.PersonId == id);

                if (!person.IsLocked)
                {
                    person.UnLock();
                    db.Entry(person).Property(c => c.LockEndDate).IsModified = true;
                    db.Entry(person).Property(c => c.LockUser).IsModified = true;

                    //desbloqueo las direcciones
                    foreach (var address in person.Addresses)
                    {
                        address.UnLock();
                        db.Entry(address).Property(c => c.LockEndDate).IsModified = true;
                        db.Entry(address).Property(c => c.LockUser).IsModified = true;
                    }

                    db.SaveChanges();
                }

                return Json(new JResponse
                {
                    Code = Codes.Success,
                    Result = Cons.ResponseInfo,
                    Header = "Registro desbloqueado",
                    Body = string.Format("El registro ha sido desbloqueado")
                });

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Code = Codes.ServerError,
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
                person = db.Persons.FirstOrDefault(c => c.PersonId == id && c.IsActive);

                if (person != null)
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
            var model = new PersonFilterViewModel<EmployeeVm>();

            model.Entities = db.Persons.OfType<Employee>().OrderBy(e => e.Name).
                                Select(c => new EmployeeVm { Employee = c }).ToList();

            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchEmployees(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = (from e in db.Persons.OfType<Employee>().Include(e => e.JobPosition).
                         Include(e => e.SystemUser).Include(e => e.Addresses.Select(a => a.Town))

                         where (ftr == string.Empty || ftr == null || e.FTR == ftr)
                         &&    (id == null || e.PersonId == id)
                         &&    (name == string.Empty || name == null || e.Name.Contains(name))
                         &&    (stateId == null || stateId == string.Empty || e.Addresses.Any(a=> a.Town.StateId == stateId))

                         &&    e.IsActive select e ).OrderBy(e=> e.Name).
                         Select(e=> new EmployeeVm { Employee = e }).ToList();

            return PartialView("_EmployeeList", model);
        }

        [HttpPost]
        public ActionResult BeginAddEmployee()
        {
            var model = new EmployeeVm();
            model.Employee = new Employee { Gender = Cons.MaleChar, HireDate = DateTime.Now.TodayLocal() };
            model.JobPositions = db.JobPositions.ToSelectList();

            BeginAddPerson(model);

            return PartialView("_EmployeeEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateEmployee(int id)
        {
            try
            {
                var model = new EmployeeVm();
                model.JobPositions = db.JobPositions.ToSelectList();

                model.Employee = db.Persons.OfType<Employee>().Include(c => c.Addresses).Include(c => c.Addresses.Select(a => a.Town.State)).
                    FirstOrDefault(c => c.PersonId == id && c.IsActive);

                BeginUpdatePerson(model);

                if (model != null)
                {
                    LockPerson(model);
                    return PartialView("_EmployeeEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Code = Codes.RecordNotFound,
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
                    Code = Codes.ServerError,
                    Result = Cons.ResponseDanger,
                    Header = "Error al obtener datos",
                    Body = string.Format("Ocurrio un error obtener los datos del empleado, detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployee(EmployeeVm model)
        {
            try
            {
                //update
                if (model.Employee.PersonId > Cons.Zero)
                {
                    UpdatePerson(model);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Empleado Modificado",
                        Code = Codes.Success,
                        Body = "Se actualizaron los datos del empleado " + model.Employee.Name + " y se removio el bloqueo",
                        Id = model.Employee.PersonId
                    });
                }
                //insert
                else
                {
                    InsertPerson(model);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Empleado Agregado",
                        Code = Codes.Success,
                        Body = "Se agrego el empleado " + model.Employee.Name,
                        Id = model.Employee.PersonId
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error Al guardar",
                    Body = "Ocurrio un error al guardar os datos del cliente " + ex.Message,
                    Code = Codes.ServerError
                });
            }
        }
        #endregion

        #region Supplier Methods
        public ActionResult Suppliers()
        {
            var model = new PersonFilterViewModel<SupplierVm>();
            model.Entities = db.Persons.OfType<Supplier>().OrderBy(s => s.Name).
                                Select(s => new SupplierVm { Supplier = s }).ToList();

            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchSuppliers(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = (from s in db.Persons.OfType<Supplier>().Include(s => s.SystemUser).
                         Include(s => s.Addresses.Select(a => a.Town))

                         where (ftr == string.Empty || ftr == null || s.FTR == ftr)
                         && (id == null || s.PersonId == id)
                         && (name == string.Empty || name == null || s.Name.Contains(name))
                         && (stateId == null || stateId == string.Empty || s.Addresses.Any(a => a.Town.StateId == stateId))

                         && s.IsActive
                         select s).OrderBy(e => e.Name).
                        Select(e => new SupplierVm { Supplier = e }).ToList();

            return PartialView("_SupplierList", model);
        }


        [HttpPost]
        public ActionResult BeginAddSupplier()
        {
            var model = new SupplierVm();
            model.Supplier = new Supplier();
            BeginAddPerson(model);
            return PartialView("_SupplierEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateSupplier(int id)
        {
            try
            {
                var model = new SupplierVm();

                model.Supplier = db.Persons.OfType<Supplier>().Include(c => c.Addresses).
                    Include(c => c.Addresses.Select(a => a.Town.State)).FirstOrDefault(c => c.PersonId == id && c.IsActive);

                BeginUpdatePerson(model);

                if (model != null)
                {
                    LockPerson(model);
                    return PartialView("_SupplierEdit", model);
                }
                else
                {
                    return Json(new JResponse
                    {
                        Code = Codes.RecordNotFound,
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
                    Code = Codes.ServerError,
                    Result = Cons.ResponseDanger,
                    Header = "Error al obtener datos",
                    Body = string.Format("Ocurrio un error obtener los datos del cliente, detalle del error:{0}", ex.Message),
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSupplier(SupplierVm supplierVm)
        {
            try
            {
                //update
                if (supplierVm.Supplier.PersonId > Cons.Zero)
                {
                    UpdatePerson(supplierVm);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Proveedor Modificado",
                        Code = Codes.Success,
                        Body = "Se actualizaron los datos del proveedor " + supplierVm.Supplier.Name + " y se removio el bloqueo",
                        Id = supplierVm.Supplier.PersonId
                    });
                }
                //insert
                else
                {
                    InsertPerson(supplierVm);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Proveedor Agregado",
                        Body = "Se agrego el proveedor " + supplierVm.Supplier.Name,
                        Id = supplierVm.Supplier.PersonId,
                        Code = Codes.Success
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Cons.ResponseDanger,
                    Header = "Error Al guardar",
                    Body = "Ocurrio un error al guardar os datos del proveedor " + ex.Message,
                    Code = Codes.ServerError
                });
            }
        }

        #endregion


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