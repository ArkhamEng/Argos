using Argos.Models.Inventory;
using Argos.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Detail", Schema = "Operative")]
    public abstract class Detail
    {
        public int DetailId { get; set; }

        public int OperationId { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// Cantidad que afecta al inventario
        /// </summary>
        public double Quantity { get; set; }

        #region Navigation Properties
        public virtual Product Product { get; set; }

        public virtual Operation Operation { get; set; }

        public ICollection<Movement> Movements { get; set; }

        #endregion
    }

}