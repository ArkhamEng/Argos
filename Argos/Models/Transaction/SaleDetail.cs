using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using Argos.Models.Operative;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("SaleDetail", Schema = "Transaction")]
    public class SaleDetail:TransactionDetail
    {
        public int SaleDetailId { get; set; }

        public int SaleId { get; set; }


        #region Navigation Properties

        public virtual Sale Sale { get; set; }

        #endregion
    }
}