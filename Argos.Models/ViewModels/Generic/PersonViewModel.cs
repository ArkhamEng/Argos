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
    public class PersonViewModel<T> :CatalogViewModel<T> where T:Person
    {
        public T Person
        {
            get { return (T)this.catalog; }
            set { this.catalog = value; }
        }

        public SelectList JobPositions { get; set; }

        public SelectList CreditStatus { get; set; }


        public bool DropImage { get; set; }

        public List<int> DroppedPhones { get; set; }

        public List<int> DroppedAddress { get; set; }

        public List<int> DroppedMails { get; set; }

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
            this.DroppedMails   = new List<int>();
            this.DroppedPhones  = new List<int>();
            this.DroppedAddress = new List<int>();
        }

        public PersonViewModel(IAppCache cache)
        {
            this.Person     = (T)Activator.CreateInstance(typeof(T));
            this.NewImages  = new List<HttpPostedFileBase>();
            this.DroppedMails = new List<int>();
            this.DroppedPhones = new List<int>();
            this.DroppedAddress = new List<int>();
           this.CreditStatus    = cache.CreditStatus.ToSelectList();
        }

    }
}