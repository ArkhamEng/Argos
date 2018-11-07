using Argos.Common.Enums;
using Argos.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("AddressType", Schema = "Business")]
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