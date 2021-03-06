﻿using Argos.Common.Constants;
using Argos.Data.Context;
using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using Argos.Support;
using Argos.ViewModels.Generic;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Argos.Web.Controllers
{
    [CustomAuthorize]
    public partial class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Client Methods
        public ActionResult Clients()
        {
            var model = GetPersons<Client>();
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchClients(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = SearchPersons<Client>(ftr, name, stateId, townId, id);
            return PartialView("_ClientList", model);
        }


        [HttpPost]
        public ActionResult BeginAddClient()
        {
            var model = new PersonViewModel<Client>(AppCache.Instance);
            return PartialView("_ClientEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateClient(int id)
        {
            try
            {
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
                        Code = Responses.Codes.RecordNotFound,
                        Result = Responses.Warning,
                        Header = "Cliente inexistente!",
                        Body = string.Format("Este cliente ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Code = Responses.Codes.ServerError,
                    Result = Responses.Danger,
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
                if (clientVm.Person.EntityId > Numbers.Zero)
                {
                    UpdatePerson(clientVm);

                    return Json(new JResponse
                    {
                        Result = Responses.Success,
                        Header = "Cliente Modificado",
                        Code = Responses.Codes.Success,
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
                        Result = Responses.Success,
                        Header = "Cliente Agregado",
                        Body = "Se agrego el cliente " + clientVm.Person.Name,
                        Id = clientVm.Person.EntityId,
                        Code = Responses.Codes.Success
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Responses.Danger,
                    Header = "Error Al guardar",
                    Body = "Ocurrio un error al guardar los datos del cliente " + ex.Message,
                    Code = Responses.Codes.ServerError
                });
            }
        }

        #endregion



        #region Employee Methods
        public ActionResult Employees()
        {
            var model = GetPersons<Employee>();
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchEmployees(string ftr, string name, string stateId, string townId, int? id)
        {
            var model = SearchPersons<Employee>(ftr, name, stateId, townId, id);
            return PartialView("_EmployeeList", model);
        }

        [HttpPost]
        public ActionResult BeginAddEmployee()
        {
            var model = new PersonViewModel<Employee>(AppCache.Instance);
            model.Person = new Employee { Gender = Labels.MaleChar, HireDate = DateTime.Now.TodayLocal() };
            model.JobPositions = db.JobPositions.ToSelectList();

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
                        Code = Responses.Codes.RecordNotFound,
                        Result = Responses.Warning,
                        Header = "Empleado inexistente!",
                        Body = string.Format("Este empleado ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Code = Responses.Codes.ServerError,
                    Result = Responses.Danger,
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
                if (model.Person.EntityId > Numbers.Zero)
                {
                    UpdatePerson(model);

                    return Json(new JResponse
                    {
                        Result = Responses.Success,
                        Header = "Empleado Modificado",
                        Code = Responses.Codes.Success,
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
                        Result = Responses.Success,
                        Header = "Empleado Agregado",
                        Code = Responses.Codes.Success,
                        Body = "Se agrego el empleado " + model.Person.Name,
                        Id = model.Person.EntityId
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Responses.Danger,
                    Header = "Error Al guardar",
                    Body = "Ocurrio un error al guardar os datos del cliente " + ex.Message,
                    Code = Responses.Codes.ServerError
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
            var model = SearchPersons<Supplier>(ftr, name, stateId, townId, id);
            return PartialView("_SupplierList", model);
        }


        [HttpPost]
        public ActionResult BeginAddSupplier()
        {
            var model = new PersonViewModel<Supplier>(AppCache.Instance);

            return PartialView("_SupplierEdit", model);
        }

        [HttpPost]
        public ActionResult BeginUpdateSupplier(int id)
        {
            try
            {
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
                        Code = Responses.Codes.RecordNotFound,
                        Result = Responses.Warning,
                        Header = "Cliente inexistente!",
                        Body = string.Format("Este cliente ya no esta activo en el catálgo"),
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Code = Responses.Codes.ServerError,
                    Result = Responses.Danger,
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
                if (supplierVm.Person.EntityId > Numbers.Zero)
                {
                    UpdatePerson(supplierVm);

                    return Json(new JResponse
                    {
                        Result = Responses.Success,
                        Header = "Proveedor Modificado",
                        Code = Responses.Codes.Success,
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
                        Result = Responses.Success,
                        Header = "Proveedor Agregado",
                        Body = "Se agrego el proveedor " + supplierVm.Person.Name,
                        Id = supplierVm.Person.EntityId,
                        Code = Responses.Codes.Success
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Responses.Danger,
                    Header = "Error Al guardar",
                    Body = "Ocurrio un error al guardar os datos del proveedor " + ex.Message,
                    Code = Responses.Codes.ServerError
                });
            }
        }


        [HttpPost]
        public ActionResult GetSuppliers()
        {
            var model = db.Persons.OfType<Supplier>().ToList();
            return PartialView("_AssingSupplier", model);
        }

        [HttpPost]
        public ActionResult SearchClient(string filter, int? id)
        {
            try
            {
                var model = (from c in db.Persons.OfType<Client>()
                             where (string.IsNullOrEmpty(filter) || c.Name.Contains(filter)) &&
                                   (id == null || c.EntityId == id)
                             select c).Take(Common.Constants.Numbers.Config.AutoCompleateRows).ToList();

                if (model.Count == Common.Constants.Numbers.One)
                    return Json(model.Select(c=> new { Name = c.Name, EntityId = c.EntityId, FTR = c.FTR, c.UserName }).FirstOrDefault());
                else
                    return PartialView("_ClientQuickSearch", model);
            }
            catch (Exception ex)
            {

                throw;
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