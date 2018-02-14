using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("AccountAddress", Schema = "Operative")]
    public class AccountAddress: LocatableEntity
    {
        [ForeignKey("ServiceAccount")]
        public int AccountAddressId { get; set; }

        #region Navigation Properties
        public virtual ServiceAccount ServiceAccount { get; set; }
        #endregion
    }
}