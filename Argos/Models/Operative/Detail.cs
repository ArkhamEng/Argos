using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Detail", Schema = "Operative")]
    public abstract class Detail:AuditableEntity
    {
        public int DetailId { get; set; }

        public int ProductId { get; set; }

     
        #region Navigation Properties
        public virtual Product Product { get; set; }

        public ICollection<Flow> Flows { get; set; }

        #endregion
    }

}