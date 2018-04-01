using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("PurchaseDetail", Schema = "Transaction")]
    public class PurchaseDetail:TransactionDetail
    {
        public int PurchaseDetailId { get; set; }

        public int PurchaseId { get; set; }


        #region Navigation Properties

        public virtual Purchase Purchase { get; set; }

        #endregion
    }
}