using Argos.Models;
using Argos.Models.BaseTypes;
using Argos.Models.BusinessEntity;
using Argos.Models.HumanResources;
using Argos.Models.Operative;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.ViewModels.Generic
{
    public class ClientVm : PersonVM
    {
        public Client Client
        {
            get { return (Client)Person; }
            set { Person = value; }
        }

        public ClientVm() : base()
        {
            this.Client = new Client();
        }
    }

    public class SupplierVm : PersonVM
    {
        public Supplier Supplier
        {
            get { return (Supplier)Person; }
            set { Person = value; }
        }

        public SupplierVm() : base()
        {
            this.Supplier = new Supplier();
        }
    }

    public class EmployeeVm : PersonVM
    {
        public SelectList JobPositions { get; set; }

        public Employee Employee
        {
            get { return (Employee)Person; }
            set { Person = value; }
        }

        public EmployeeVm() : base()
        {
            this.Employee = new Employee();
        }
    }

    public class PersonVM
    {
        public Person Person;

        public List<AddressVm> Addresses { get; set; }

        public virtual string EditButton
        {
            get
            {
                if (true || (HttpContext.Current.User.IsInRole("Capturista") && (this.Person != null && this.Person.IsActive)))
                    return Styles.BtnEdit;
                else
                    return Styles.BtnEditDisabled;
            }
        }

        public virtual string DeleteButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Capturista") && (this.Person != null && this.Person.IsActive)))
                    return Styles.BtnDelete;
                else
                    return Styles.BtnDeletetDisabled;
            }
        }

        public virtual string DropImageButton
        {
            get
            {
                return (this.Person.ImagePath != null && this.Person.ImagePath != string.Empty) ?
                        Styles.BtnDropImage : Styles.BtnDropImageDisabled;
            }
        }

        public string ItemState
        {
            get
            {
                if (!this.Person.IsActive)
                    return Cons.ResponseDanger;

                return string.Empty;
            }
        }

        
        public bool DropImage { get; set; }

        public List<HttpPostedFileBase> NewImages { get; set; }

        public string Image
        {
            get
            {
                return (this.Person.ImagePath != null && this.Person.ImagePath != string.Empty) ? this.Person.ImagePath : Cons.NoImage;
            }
        }

        public PersonVM()
        {
            this.Addresses = new List<AddressVm>();
            this.NewImages = new List<HttpPostedFileBase>();
        }

    }

}