using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("ServiceAddress", Schema = "Operative")]
    public class ServiceAddress: LocatableEntity
    {
        [ForeignKey("Service")]
        public int ServiceAddressId { get; set; }

        #region Navigation Properties
        public virtual Service Service { get; set; }
        #endregion
    }
}