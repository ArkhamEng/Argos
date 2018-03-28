using Argos.Models.BaseTypes;
using Argos.Models.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Compatibility", Schema = "Inventory")]
    public class Compatibility:AuditableEntity
    {
        [Column(Order = 0), Key, ForeignKey("CarYear")]
        public int CarYearId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        #region Navigation Properties
        public virtual Product Product { get; set; }

        public virtual CarYear CarYear { get; set; }
        #endregion
    }
}