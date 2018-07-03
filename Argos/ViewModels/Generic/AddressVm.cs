using Argos.Models.Business;
using Argos.Models.Config;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
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
            this.Towns = new List<Town>().ToSelectList();
        }
    }

   
    public class PhoneVm
    {
        public SelectList PhoneTypes { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public string Style
        {
            get { return (PhoneNumber.Phone == null) || (PhoneNumber.Phone == string.Empty) ? Styles.Hidden : string.Empty; }
        }

    }

    public class EmailVm
    {
        public EmailAddress Email { get; set; }

        public string Style
        {
            get { return Email.Email == null || Email.Email == string.Empty ? Styles.Hidden : string.Empty; }
        }

    }
}