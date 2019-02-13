using Argos.Models.Business;
using Argos.Support;
using Argos.ViewModels.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using Argos.Models;
using Argos.Common.Constants;


namespace Argos.Web.Controllers
{
    public partial class CatalogController : Controller
    {
        public PersonSearchVieModel<T> GetPersons<T>() where T : Person
        {
            var model = new PersonSearchVieModel<T>();

            model.Entities = SearchPersons<T>(null, null, null, null, null);

            model.States = AppCache.Instance.States.ToSelectList();

            return model;
        }

        public ICollection<PersonViewModel<T>> SearchPersons<T>(string ftr, string name, string stateId, string townId, int? id) where T : Person
        {
            var model = (from p in db.Persons.OfType<T>().Include(p => p.SystemUser).Include(p => p.Addresses.Select(a => a.Town)).
                        Include(s => s.PhoneNumbers).Include(s => s.EmailAddresses)

                         where (ftr == string.Empty || ftr == null || p.FTR == ftr)
                         && (id == null || p.EntityId == id)
                         && (name == string.Empty || name == null || p.Name.Contains(name))
                         && (stateId == null || stateId == string.Empty || p.Addresses.Any(a => a.Town.StateId == stateId))
                         && (townId == null || townId == string.Empty || p.Addresses.Any(a => a.TownId == townId))
                         && p.IsActive
                         select p).OrderBy(e => e.Name).
                       Select(e => new PersonViewModel<T> { Person = e }).ToList();

            return model;
        }

        [HttpPost]
        public ActionResult AddAddress()
        {
            var model = new AddressVm(AppCache.Instance);
            return PartialView("_Address", model);
        }

        [HttpPost]
        public ActionResult AddPhone()
        {
            var model = new PhoneVm(AppCache.Instance);
            return PartialView("_Phone", model);
        }

        [HttpPost]
        public ActionResult AddEmail()
        {
            var model = new EmailVm(AppCache.Instance);
            return PartialView("_Email", model);
        }

        [HttpPost]
        public ActionResult UnLockPerson(int id)
        {
            try
            {
                var person = db.Entities.Include(p => p.Addresses).FirstOrDefault(p => p.EntityId == id);

                if (person.IsLocked)
                {
                    person.LockEndDate = null;
                    person.LockUser = null;

                    db.Entry(person);
                    db.SaveChanges();
                }

                return Json(new JResponse
                {
                    Code = Responses.Codes.Success,
                    Result = Responses.Info,
                    Header = "Registro desbloqueado",
                    Body = string.Format("El registro ha sido desbloqueado")
                });

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Code = Responses.Codes.ServerError,
                    Result = Responses.Danger,
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
                person = db.Entities.OfType<Person>().FirstOrDefault(c => c.EntityId == id && c.IsActive);

                if (person != null)
                {
                    person.LockEndDate = null;
                    person.LockUser = string.Empty;
                    person.IsActive = false;

                    db.Entry(person);

                    db.SaveChanges();
                }
                else
                {
                    return Json(new JResponse
                    {
                        Result = Responses.Warning,
                        Header = "No existe el registro a eliminar!",
                        Body = string.Format("Este registro ya no esta activo en el catálgo")
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new JResponse
                {
                    Result = Responses.Danger,
                    Header = "Error al eliminar el registro",
                    Body = string.Format("Ocurrio un error al eliminar el registro detalle del error:{0}", ex.Message)
                });
            }
            return Json(new JResponse
            {
                Result = Responses.Success,
                Header = "Registro eliminado!",
                Body = string.Format("El registro {0} fue eliminado del catálogo", person.Name),
                Id = person.EntityId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleatePerson(string filter)
        {
            var clients = db.Entities.OfType<Person>().Where(c => c.Name.Contains(filter)).
                          OrderBy(c => c.Name).Take(Numbers.Config.AutoCompleateRows).
                          Select(c => new { Label = c.Name, Id = c.EntityId, Value = c.FTR });

            return Json(clients);
        }


        private PersonViewModel<T> BeginUpdatePerson<T>(int id) where T : Person
        {
            try
            {
                var model = new PersonViewModel<T>(AppCache.Instance);

                model.Person = db.Entities.OfType<T>().Include(c => c.Addresses).Include(c => c.PhoneNumbers).Include(c => c.EmailAddresses).
                    Include(c => c.Addresses.Select(a => a.Town.State)).FirstOrDefault(c => c.EntityId == id && c.IsActive);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool InsertPerson<T>(PersonViewModel<T> personVm) where T : Person
        {
            try
            {
               
                db.Entities.Add(personVm.Person);
                db.SaveChanges();

                //si hay imagen, la guardo y guardo la ruta
                if (personVm.NewImages.Count > Numbers.Zero)
                {
                    var file = personVm.NewImages.FirstOrDefault();

                    var fileT = GetFileType(personVm.Person);

                    var path = Argos.Support.FileManager.SaveFile(file, personVm.Person.EntityId.ToString(), fileT);
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

        private bool UpdatePerson<T>(PersonViewModel<T> personVm) where T : Person
        {
            try
            {
                //elimino direcciones
                if (personVm.DroppedAddress.Count > Numbers.Zero)
                {
                    var addresses = db.Addresses.
                        Where(a => a.EntityId == personVm.Person.EntityId && personVm.DroppedAddress.Contains(a.AddressId));

                    foreach (var addr in addresses)
                    {
                        addr.IsActive = false;
                        addr.UpdDate = DateTime.Now.ToLocal();
                        addr.UpdUser = HttpContext.User.Identity.Name;

                        db.Entry(addr);
                    }
                }

                //agrego las nuevas direcciones
                foreach (var address in personVm.Person.Addresses)
                {
                    address.EntityId = personVm.Person.EntityId;                    
                    db.Addresses.Add(address);
                }

                //elimino telefonos
                if (personVm.DroppedPhones.Count > Numbers.Zero)
                {
                    var phones = db.PhoneNumbers.Where(p => p.EntityId == personVm.Person.EntityId && personVm.DroppedPhones.Contains(p.PhoneNumberId));

                    foreach(var phone in phones)
                    {
                        phone.IsActive = false;
                        phone.UpdDate = DateTime.Now.ToLocal();
                        phone.UpdUser = HttpContext.User.Identity.Name;

                        db.Entry(phone);
                    }
                }

                //agrego los nuevos teléfonos
                foreach (var phoneNumber in personVm.Person.PhoneNumbers)
                {
                    phoneNumber.EntityId = personVm.Person.EntityId;
                    db.PhoneNumbers.Add(phoneNumber);
                }


                //elimino direcciones de correo
                if (personVm.DroppedMails.Count > Numbers.Zero)
                {
                    var emails = db.EmailAddresses.Where(e => e.EntityId == personVm.Person.EntityId && personVm.DroppedMails.Contains(e.EmailAddressId));

                    foreach (var mail in emails)
                    {
                        mail.IsActive = false;
                        mail.UpdDate = DateTime.Now.ToLocal();
                        mail.UpdUser = HttpContext.User.Identity.Name;

                        db.Entry(mail);
                    }
                }

                //agrego las nuevas direcciones de correo
                foreach (var emailAddress in personVm.Person.EmailAddresses)
                {
                    emailAddress.EntityId = personVm.Person.EntityId;
                    db.EmailAddresses.Add(emailAddress);
                }

                bool modifyImage = false;

                //si la imagen fue eliminada, la borro del disco
                if (personVm.DropImage && personVm.Person.ImagePath != string.Empty)
                {
                    FileManager.DeleteImage(personVm.Person.ImagePath);
                    personVm.Person.ImagePath = null;
                    modifyImage = true;
                }

                //si hay imagen, la guardo y guardo la ruta
                if (personVm.NewImages.Count > Numbers.Zero && personVm.NewImages.First() != null)
                {
                    var file = personVm.NewImages.FirstOrDefault();

                    var fileT = GetFileType(personVm.Person);

                    var path = Argos.Support.FileManager.SaveFile(file, personVm.Person.EntityId.ToString(), fileT);
                    personVm.Person.ImagePath = path;
                    modifyImage = true;
                }

                personVm.Person.LockEndDate = null;
                personVm.Person.LockUser    = null;
              
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

        private FileType GetFileType(object person)
        {
            var t = person.GetType();
            var fileT = FileType.ClientImage;

            if (t == typeof(Argos.Models.HumanResources.Employee))
                fileT = FileType.EmployeeImage;
            if (t == typeof(Argos.Models.Purchasing.Supplier))
                fileT = FileType.SupplierImage;

            return fileT;
        }

        private JsonResult LockPerson<T>(PersonViewModel<T> model) where T : Person
        {
            try
            {
                if (model.Person.IsLocked)
                {
                    var t = (model.Person.LockEndDate.Value - DateTime.Now.ToLocal());
                    var time = t.Minutes.ToString("00") + ":" + t.Seconds.ToString("00");
                    return Json(new JResponse
                    {
                        Code = Responses.Codes.RecordLocked,
                        Result = Responses.Warning,
                        Header = "Resistro bloqueado!",
                        Body = string.Format("Este registro ha sido bloqueado por  el usuario {0}, tiempo restante del bloqueo {1}", model.Person.LockUser, time),
                    });
                }

                //bloqueo el registro
                model.Person.LockEndDate = DateTime.Now.ToLocal().AddMinutes(Numbers.Config.LockDuration);
                model.Person.LockUser    = HttpContext.User.Identity.Name;

                db.Entry(model.Person).Property(c => c.LockEndDate).IsModified = true;
                db.Entry(model.Person).Property(c => c.LockUser).IsModified = true;


                db.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}