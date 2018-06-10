using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("ProductStock", Schema = "Inventory")]
    public class ProductStock:AuditableEntity
    {
        [Column(Order = 0)]
        public int ProductStockId { get; set; }

        [Column(Order = 1)]
        public int BranchId { get; set; }

        [Column(Order = 2)]
        public int ProductId { get; set; }

        [Column(Order = 3)]
        public double Quantity { get; set; }

        [Column(Order = 4)]
        [Display(Name ="Cantidad máxima")]
        public double MaxQuantity { get; set; }

        [Column(Order = 5)]
        [Display(Name = "Cantidad mínima")]
        public double MinQuantity { get; set; }

        [Column(Order = 6)]
        [MaxLength(10)]
        [Display(Name = "Fila")]
        public string Shelf { get; set; }

        [Column(Order = 7)]
        [MaxLength(10)]
        [Display(Name = "Anaquel")]
        public string Bin { get; set; }


        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public virtual Product Product { get; set; }

        public ICollection<Serial> Serials { get; set; }

        public ICollection<StockMovement> StockMovements { get; set; }


        #endregion
    }
}