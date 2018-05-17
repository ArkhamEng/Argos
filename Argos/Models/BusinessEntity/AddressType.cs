using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BusinessEntity
{
    [Table("AddressType", Schema = "BusinessEntity")]
    public class AddressType:ISelectable
    {
        public AddressTypes AddressTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public string Value { get { return ((int)this.AddressTypeId).ToString(); } }
       

        public string Text { get { return this.Name; } }
       
    }
}