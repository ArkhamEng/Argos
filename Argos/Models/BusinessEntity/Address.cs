using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BusinessEntity
{
    [Table("Address", Schema = "BusinessEntity")]
    public class Address:LocatableEntity
    {
        public int AddressId { get; set; }

        public AddressTypes AddressTypeId { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public virtual AddressType AddressType { get; set; }
    }
}