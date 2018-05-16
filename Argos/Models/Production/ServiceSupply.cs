using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Supply", Schema = "Operative")]
    public class Supply:AuditableEntity
    {
        public int SupplyId { get; set; }

        public int ServiceId { get; set; }

        public double Quantity { get; set; }

        #region Navigation Properties
        public virtual Service Service { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }
}