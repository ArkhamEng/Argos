using Argos.Models.Operative;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("PurchaseDetail", Schema = "Transaction")]
    public class PurchaseDetail:Detail
    {
        public int PurchaseId { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        public double OrderQty { get; set; }

        public double LineTotal { get; set; }

        public double ReceivedQty { get; set; }

        public double RejectedQty { get; set; }

        public double StockQty { get; set; }

        public virtual Purchase Purchase { get; set; }

    }
}