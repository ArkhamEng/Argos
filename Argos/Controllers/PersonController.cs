using Argos.Models.Business;
using Argos.Support;
using Argos.ViewModels.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using Argos.Models.Config;
using Argos.Models.Operative;

namespace Argos.Controllers
{
    public partial class CatalogController : Controller
    {
        public PersonSearchVieModel<T> GetPersons<T>() where T : Person
        {
            var model = new PersonSearchVieModel<T>();

            model.Entities = (from s in db.Persons.OfType<T>().Include(s => s.PhoneNumbers).Include(s => s.EmailAddresses).ToList()
                              select new PersonViewModel<T> { Person = s }).OrderBy(s => s.Person.Name).ToList();

            model.States = db.States.ToSelectList();

            return model;
        }

        public ICollection<PersonViewModel<T>> SearchPersons<T>(string ftr, string name, string stateId, string townId, int? id) where T : Person
        {
            var model = (from p in db.Persons.OfType<T>().Include(p => p.SystemUser).Include(p=> p.Addresses.Select(a=> a.Town)).
                        Include(s => s.PhoneNumbers).Include(s => s.EmailAddresses).ToList()

                         where (ftr == string.Empty || ftr == null || p.FTR == ftr)
                         && (id == null || p.EntityId == id)
                         && (name == string.Empty || name == null || p.Name.Contains(name))
                         && (stateId == null || stateId == string.Empty || p.Addresses.Any(a => a.Town.StateId == stateId))
                         && (townId == null || townId == string.Empty || p.Addresses.Any(a=> a.TownId == townId))
                         && p.IsActive
                         select p).OrderBy(e => e.Name).
                       Select(e => new PersonViewModel<T> { Person = e }).ToList();

            return model;
        }

        [HttpPost]
        public ActionResult UnLockPerson(int id)
        {
            try
            {
                var person = db.Entities.Include(p => p.Addresses).FirstOrDefault(p => p.EntityId == id);

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
                person = db.Entities.OfType<Person>().FirstOrDefault(c => c.EntityId == id && c.IsActive);

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
                Id = person.EntityId
            });
        }

        [HttpPost]
        public ActionResult AutoCompleatePerson(string filter)
        {
            var clients = db.Entities.OfType<Person>().Where(c => c.Name.Contains(filter)).
                          OrderBy(c => c.Name).Take(Cons.AutoCompleateRows).
                          Select(c => new { Label = c.Name, Id = c.EntityId, Value = c.FTR });

            return Json(clients);
        }

        private void BeginAddPerson<T>(PersonViewModel<T> model) where T : Person
        {
            model.AddressViewModel.States = db.States.OrderBy(s => s.Name).ToSelectList();
            model.AddressViewModel.Types  = db.AddressTypes.ToSelectList();
            model.AddressViewModel.Towns  = new SelectList(new List<Town>());
         
            model.Emails.Add(new EmailVm { Email = new EmailAddress() });

            model.Phones.Add(new PhoneVm
            {
                PhoneNumber = new PhoneNumber(),
                PhoneTypes = db.PhoneTypes.OrderBy(pt => pt.Name).ToSelectList()
            });

        }

        private PersonViewModel<T> BeginUpdatePerson<T>(int id) where T : Person
        {
            try
            {
                var model = new PersonViewModel<T>();

                model.Person = db.Entities.OfType<T>().Include(c => c.Addresses).Include(c => c.PhoneNumbers).Include(c => c.EmailAddresses).
                    Include(c => c.Addresses.Select(a => a.Town.State)).FirstOrDefault(c => c.EntityId == id && c.IsActive);

                var phoneTypes = db.PhoneTypes.OrderBy(pt => pt.Name).ToSelectList();

                model.AddressViewModel.States = db.States.OrderBy(s => s.Name).ToSelectList();
                model.AddressViewModel.Types  = db.AddressTypes.OrderBy(at => at.Name).ToSelectList();

                foreach (var a in model.Person.Addresses)
                {
                    model.AddressViewModel.Addresses.Add(a);
                }

                foreach (var p in model.Person.PhoneNumbers)
                {
                    PhoneVm pvm = new PhoneVm { PhoneNumber = p };
                    pvm.PhoneTypes = phoneTypes.ToSelectList((int)p.PhoneTypeId);
                    model.Phones.Add(pvm);
                }


                PhoneVm ph    = new PhoneVm { PhoneNumber = new PhoneNumber() };
                ph.PhoneTypes = phoneTypes;
                model.Phones.Insert(Cons.Zero, ph);


                foreach (var e in model.Person.EmailAddresses)
                {
                    EmailVm evm = new EmailVm { Email = e };
                    model.Emails.Add(evm);
                }

                EmailVm em = new EmailVm { Email = new EmailAddress() };
                model.Emails.Insert(Cons.Zero, em);

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
                if (personVm.NewImages.Count > Cons.Zero)
                {
                    var file = personVm.NewImages.FirstOrDefault();
                    var path = Support.FileManager.SaveFile(file, personVm.Person.EntityId.ToString(), FileType.PersonImage);
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
                foreach(var dPhone in personVm.DroppedPhones)
                {
                    var phone = new PhoneNumber { EntityId = personVm.Person.EntityId, Phone = dPhone };
                    db.Entry(phone).State = EntityState.Deleted;
                }

                foreach (var dMail in personVm.DroppedMails)
                {
                    var email = new EmailAddress { EntityId = personVm.Person.EntityId, Email = dMail };
                    db.Entry(email).State = EntityState.Deleted;
                }

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

                foreach (var phoneNumber in personVm.Person.PhoneNumbers)
                {
                    //si es nuevo registro lo agrego
                    if (phoneNumber.EntityId == Cons.Zero)
                    {
                        phoneNumber.EntityId = personVm.Person.EntityId;
                        db.PhoneNumbers.Add(phoneNumber);
                    }
                    else
                    {
                        //de lo contrario actualizo
                        db.Entry(phoneNumber).State = EntityState.Modified;
                        db.Entry(phoneNumber).Property(c => c.InsDate).IsModified = false;
                        db.Entry(phoneNumber).Property(c => c.InsUser).IsModified = false;
                    }
                }

                foreach (var mail in personVm.Person.EmailAddresses)
                {
                    //si es nuevo registro lo agrego
                    if (mail.EntityId == Cons.Zero)
                    {
                        mail.EntityId = personVm.Person.EntityId;
                        db.EmailAddresses.Add(mail);
                    }                
                    else
                    {
                        //de lo contrario actualizo
                        db.Entry(mail).State = EntityState.Modified;
                        db.Entry(mail).Property(c => c.InsDate).IsModified = false;
                        db.Entry(mail).Property(c => c.InsUser).IsModified = false;
                    }
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
                if (personVm.NewImages.Count > Cons.Zero && personVm.NewImages.First() != null)
                {
                    var file = personVm.NewImages.FirstOrDefault();
                    var path = Support.FileManager.SaveFile(file, personVm.Person.EntityId.ToString(), FileType.PersonImage);
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

        private JsonResult LockPerson<T>(PersonViewModel<T> model) where T : Person
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
                model.Person.Lock();

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