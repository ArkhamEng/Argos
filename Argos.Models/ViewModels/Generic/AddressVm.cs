using Argos.Common.Interfaces;
using Argos.Models;
using Argos.Models.Business;
using Argos.Models.Config;
using Argos.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Argos.ViewModels.Generic
{
    [NotMapped]
    public class AddressVm:Address
    {
        [Display(Name ="Estado")]
        public string SelectedStateId { get; set; }

        public SelectList States { get; set; }

        public SelectList Types { get; set; }

        public SelectList Towns { get; set; }

        public List<Address> Addresses { get; set; }

        public string AddButton { get; set; }

        public string RemoveButton { get; set; }

        public AddressVm()
        {           
            this.Addresses = new List<Address>();
            this.Towns     = new List<Town>().ToSelectList();
        }

        public AddressVm(IAppCache cache)
        {
            this.States    = cache.States.ToSelectList();
            this.Types     = cache.AddressTypes.ToSelectList();
            this.Addresses = new List<Address>();
            this.Towns = new List<Town>().ToSelectList();
        }
    }

   [NotMapped]
    public class PhoneVm:PhoneNumber
    {
        public SelectList PhoneTypes { get; set; }

        public PhoneVm()
        {
            this.PhoneTypes = new List<ISelectable>().ToSelectList();
        }

        public PhoneVm(IAppCache chache)
        {
            this.PhoneTypes = chache.PhoneTypes.ToSelectList();
        }

    }

    [NotMapped]
    public class EmailVm:EmailAddress
    {
        public SelectList EmailTypes { get; set; }

        public EmailVm()
        {
            this.EmailTypes = new List<ISelectable>().ToSelectList();
        }

        public EmailVm(IAppCache chache)
        {
           this.EmailTypes = chache.EmailTypes.ToSelectList();
        }
    }
}