using Argos.Common;
using Argos.Common.Constants;
using Argos.Common.Enums;
using Argos.Common.Interfaces;
using Argos.Models;
using Argos.Models.Business;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Argos.ViewModels.Generic
{
    public class PersonViewModel<T> where T:Person
    {
        public T Person { get; set; }

        public SelectList JobPositions { get; set; }

        public SelectList CreditStatus { get; set; }


        public bool  AllowEdit { get; set; }

        public virtual string EditButton
        {
            get
            {
                if (true || (HttpContext.Current.User.IsInRole("Capturista") && (this.Person != null && this.Person.IsActive)))
                    return Styles.Buttons.Warning;
                else
                    return Styles.Buttons.Disabled.Warning;
            }
        }

        public virtual string DeleteButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Capturista") && (this.Person != null && this.Person.IsActive)))
                    return Styles.Buttons.Danger;
                else
                    return Styles.Buttons.Disabled.Danger;
            }
        }

        public virtual string DropImageButton
        {
            get
            {
                return (this.Person.ImagePath != null && this.Person.ImagePath != string.Empty) ?
                        Styles.Buttons.Danger : Styles.Buttons.Disabled.Danger;
            }
        }

        public string ItemState
        {
            get
            {
                if (!this.Person.IsActive)
                    return Responses.Danger;

                return string.Empty;
            }
        }


        public bool DropImage { get; set; }

        public List<string> DroppedPhones { get; set; }

        public List<AddressTypes> DroppedAddress { get; set; }

        public List<string> DroppedMails { get; set; }

        public List<HttpPostedFileBase> NewImages { get; set; }

        public string Image
        {
            get
            {
                return (this.Person.ImagePath != null && this.Person.ImagePath != string.Empty) ? this.Person.ImagePath : URis.NoImage;
            }
        }

        public PersonViewModel()
        {
            this.Person         = (T)Activator.CreateInstance(typeof(T));
            this.NewImages      = new List<HttpPostedFileBase>();
            this.DroppedMails   = new List<string>();
            this.DroppedPhones  = new List<string>();
            this.DroppedAddress = new List<AddressTypes>();
        }

        public PersonViewModel(IAppCache cache)
        {
            this.Person     = (T)Activator.CreateInstance(typeof(T));
            this.NewImages  = new List<HttpPostedFileBase>();
            this.DroppedMails = new List<string>();
            this.DroppedPhones = new List<string>();
            this.DroppedAddress = new List<AddressTypes>();
           this.CreditStatus    = cache.CreditStatus.ToSelectList();
        }

    }
}