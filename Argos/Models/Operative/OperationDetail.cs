using Argos.Models.Inventory;
using Argos.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("OperationDetail", Schema = "Operative")]
    public abstract class OperationDetail
    {
        public int OperationDetailId { get; set; }

        [ForeignKey("Operation")]
        public int OperationId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public double Quantity { get; set; }

        #region Navigation Properties
        public virtual Product Product { get; set; }

        public virtual Operation Operation { get; set; }

        public ICollection<StockMovement> StockMovements { get; set; }
        #endregion
    }

}