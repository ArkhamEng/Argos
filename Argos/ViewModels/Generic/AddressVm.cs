using Argos.Models.Business;
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
   
    public class AddressVm
    {
        [Display(Name ="Estado")]
        public string SelectedStateId { get; set; }

        public SelectList States { get; set; }

        public SelectList Types { get; set; }

        public SelectList Towns { get; set; }

        public Address Address { get; set; }

        public string AddButton { get; set; }

        public string RemoveButton { get; set; }
    }

   
    public class PhoneVm
    {
        public SelectList PhoneTypes { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public string Style
        {
            get { return PhoneNumber.PhoneNumberId == Cons.Zero ? Styles.Hidden : string.Empty; }
        }

    }

    public class EmailVm
    {
        public EmailAddress Email { get; set; }

        public string Style
        {
            get { return Email.EmailAddressId == Cons.Zero ? Styles.Hidden : string.Empty; }
        }

    }
}