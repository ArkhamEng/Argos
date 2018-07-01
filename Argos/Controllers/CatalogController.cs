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
using Argos.Models.Business;
using Argos.Models.Enums;


namespace Argos.Controllers
{
    [CustomAuthorize]
    public partial class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public ActionResult AddAddress()
        //{
        //    var model = new AddressVm();
        //    model.Address = new Address();

        //    model.States = db.States.OrderBy(s => s.Name).ToSelectList();
        //    model.Types = db.AddressTypes.OrderBy(t => t.Name).ToSelectList();
        //    model.Towns = new List<Town>().ToSelectList();

        //    return PartialView("_Address", model);
        //}

        #region Client Methods
        public ActionResult Clients()
        {
            var model = new PersonSearchVieModel<Client>();
            model.Entities = db.Entities.OfType<Client>().OrderBy(c => c.Name).
                                Select(c => new PersonViewModel<Client> { Person = c }).ToList();

            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchClients(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = (from c in db.Entities.OfType<Client>().Include(c => c.SystemUser).
                         Include(c => c.Addresses.Select(a => a.Town))

                         where (ftr == string.Empty || ftr == null || c.FTR == ftr)
                        && (id == null || c.EntityId == id)
                        && (name == string.Empty || name == null || c.Name.Contains(name))
                        && (stateId == null || stateId == string.Empty || c.Addresses.Any(a => a.Town.StateId == stateId))
                        && c.IsActive
                        select c).OrderBy(c => c.Name).Select(c => new PersonViewModel<Client> { Person = c }).ToList();

            return PartialView("_ClientList", model);
        }


        [HttpPost]
        public ActionResult BeginAddClient()
        {
            var model = new PersonViewModel<Client>();
            BeginAddPerson(model);

            return PartialView("_ClientEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateClient(int id)
        {
            try
            {
                //var model = new PersonViewModel<Client>();

                //model.Person = db.Entities.OfType<Client>().Include(c => c.Addresses).Include(c => c.Addresses.Select(a => a.Town.State)).
                //    FirstOrDefault(c => c.EntityId == id && c.IsActive);

                var model = BeginUpdatePerson<Client>(id);

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
        public ActionResult SaveClient(PersonViewModel<Client> clientVm)
        {
            try
            {
                //update
                if (clientVm.Person.EntityId > Cons.Zero)
                {
                    UpdatePerson(clientVm);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Cliente Modificado",
                        Code = Codes.Success,
                        Body = "Se actualizaron los datos del cliente " + clientVm.Person.Name + " y se removio el bloqueo",
                        Id = clientVm.Person.EntityId
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
                        Body = "Se agrego el cliente " + clientVm.Person.Name,
                        Id = clientVm.Person.EntityId,
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

      

        #region Employee Methods
        public ActionResult Employees()
        {
            var model = new PersonSearchVieModel<Employee>();

            model.Entities = db.Entities.OfType<Employee>().OrderBy(e => e.Name).
                                Select(c => new PersonViewModel<Employee> { Person = c }).ToList();

            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchEmployees(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = (from e in db.Entities.OfType<Employee>().Include(e => e.JobPosition).
                         Include(e => e.SystemUser).Include(e => e.Addresses.Select(a => a.Town))

                         where (ftr == string.Empty || ftr == null || e.FTR == ftr)
                         && (id == null || e.EntityId == id)
                         && (name == string.Empty || name == null || e.Name.Contains(name))
                         && (stateId == null || stateId == string.Empty || e.Addresses.Any(a => a.Town.StateId == stateId))

                         && e.IsActive
                         select e).OrderBy(e => e.Name).
                         Select(e => new PersonViewModel<Employee> { Person = e }).ToList();

            return PartialView("_EmployeeList", model);
        }

        [HttpPost]
        public ActionResult BeginAddEmployee()
        {
            var model = new PersonViewModel<Employee>();
            model.Person = new Employee { Gender = Cons.MaleChar, HireDate = DateTime.Now.TodayLocal() };
            model.JobPositions = db.JobPositions.ToSelectList();

            BeginAddPerson(model);

            return PartialView("_EmployeeEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateEmployee(int id)
        {
            try
            {
                var model = BeginUpdatePerson<Employee>(id);
                model.JobPositions = db.JobPositions.ToSelectList();

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
        public ActionResult SaveEmployee(PersonViewModel<Employee> model)
        {
            try
            {
                //update
                if (model.Person.EntityId > Cons.Zero)
                {
                    UpdatePerson(model);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Empleado Modificado",
                        Code = Codes.Success,
                        Body = "Se actualizaron los datos del empleado " + model.Person.Name + " y se removio el bloqueo",
                        Id = model.Person.EntityId
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
                        Body = "Se agrego el empleado " + model.Person.Name,
                        Id = model.Person.EntityId
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
            var model = new PersonSearchVieModel<Supplier>();

            model.Entities = (from s in db.Entities.Include(s => s.PhoneNumbers).Include(s => s.EmailAddresses).ToList().OfType<Supplier>()
                              select new PersonViewModel<Supplier> { Person = s }).OrderBy(s => s.Person.Name).ToList();

            model.States = db.States.ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchSuppliers(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = (from s in db.Entities.OfType<Supplier>().Include(s => s.SystemUser).
                         Include(s => s.Addresses.Select(a => a.Town))

                         where (ftr == string.Empty || ftr == null || s.FTR == ftr)
                         && (id == null || s.EntityId == id)
                         && (name == string.Empty || name == null || s.Name.Contains(name))
                         && (stateId == null || stateId == string.Empty || s.Addresses.Any(a => a.Town.StateId == stateId))

                         && s.IsActive
                         select s).OrderBy(e => e.Name).
                        Select(e => new PersonViewModel<Supplier> { Person = e }).ToList();

            return PartialView("_SupplierList", model);
        }


        [HttpPost]
        public ActionResult BeginAddSupplier()
        {
            var model = new PersonViewModel<Supplier>();
            
            BeginAddPerson(model);
            return PartialView("_SupplierEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateSupplier(int id)
        {
            try
            {
                //var model = new PersonViewModel<Supplier>();

                //model.Person = db.Entities.OfType<Supplier>().Include(c => c.Addresses).Include(c => c.PhoneNumbers).Include(c => c.EmailAddresses).
                //    Include(c => c.Addresses.Select(a => a.Town.State)).FirstOrDefault(c => c.EntityId == id && c.IsActive);

               var model = BeginUpdatePerson<Supplier>(id);

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
        public ActionResult SaveSupplier(PersonViewModel<Supplier> supplierVm)
        {
            try
            {
                //update
                if (supplierVm.Person.EntityId > Cons.Zero)
                {
                    UpdatePerson(supplierVm);

                    return Json(new JResponse
                    {
                        Result = Cons.ResponseSuccess,
                        Header = "Proveedor Modificado",
                        Code = Codes.Success,
                        Body = "Se actualizaron los datos del proveedor " + supplierVm.Person.Name + " y se removio el bloqueo",
                        Id = supplierVm.Person.EntityId
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
                        Body = "Se agrego el proveedor " + supplierVm.Person.Name,
                        Id = supplierVm.Person.EntityId,
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