using Argos.Models.Business;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.ViewModels.Generic
{
    public class PersonViewModel<T> where T:Person
    {
        public T Person { get; set; }

        public SelectList JobPositions { get; set; }

        public List<PhoneVm> Phones { get; set; }

        public List<EmailVm> Emails { get; set; }

        public List<AddressVm> Addresses { get; set; }

        public virtual string EditButton
        {
            get
            {
                if (true || (HttpContext.Current.User.IsInRole("Capturista") && (this.Person != null && this.Person.IsActive)))
                    return Styles.btnWarning;
                else
                    return Styles.btnWarningDisable;
            }
        }

        public virtual string DeleteButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Capturista") && (this.Person != null && this.Person.IsActive)))
                    return Styles.btnDanger;
                else
                    return Styles.btnDangerDisable;
            }
        }

        public virtual string DropImageButton
        {
            get
            {
                return (this.Person.ImagePath != null && this.Person.ImagePath != string.Empty) ?
                        Styles.btnDanger : Styles.btnDangerDisable;
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

        public PersonViewModel()
        {
            this.Person     = (T)Activator.CreateInstance(typeof(T));
            this.Addresses  = new List<AddressVm>();
            this.NewImages  = new List<HttpPostedFileBase>();
            this.Phones     = new List<PhoneVm>();
            this.Emails     = new List<EmailVm>();
        }

    }
}