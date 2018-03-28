using Argos.Models.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("PurchaseDetail", Schema = "Transaction")]
    public class PurchaseDetail
    {
        public int PurchaseDetailId { get; set; }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Monto Parcial")]
        [DataType(DataType.Currency)]
        public double PartialAmount { get; set; }

        #region Navigation Properties

        public virtual Purchase Purchase { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }
}