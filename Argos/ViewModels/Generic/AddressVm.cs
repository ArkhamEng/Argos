using Argos.Models.BusinessEntity;
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
}