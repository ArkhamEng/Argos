using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("Detail", Schema = "Business")]
    public abstract class Detail:AuditableEntity
    {
        public int DetailId { get; set; }

        [Display(Name ="Producto")]
        public int ProductId { get; set; }

     
        #region Navigation Properties
        public virtual Product Product { get; set; }

        public ICollection<Flow> Flows { get; set; }

        #endregion
    }

}