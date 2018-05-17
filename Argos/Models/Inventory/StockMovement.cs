using Argos.Models.Operative;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("StockMovement", Schema = "Inventory")]
    public class StockMovement
    {
        public int StockMovementId { get; set; }

        public int ProductStockId { get; set; }

        public int OperationDetailId { get; set; }

        public int? SerialId { get; set; }

        public double Quantity { get; set; }

        [Required]
        public double LastStock { get; set; }

        [Required]
        public double CurrentStock { get; set; }

        public bool IsEntry { get; set; }

        [Required]
        public DateTime InsDate { get; set; }

        [Required]
        public string InsUser { get; set; }

        public string MovementType
        { get { return IsEntry ? "Entrada" : "Salida"; } }

        #region Navigation Properties
        public virtual ProductStock ProductStock { get; set; }

        public virtual OperationDetail OperationDetail { get; set; }

        public virtual Serial Serial { get; set; }
        #endregion
    }
}