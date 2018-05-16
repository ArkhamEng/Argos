using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using Argos.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("PurchaseDetail", Schema = "Operative")]
    public class PurchaseDetail:OperationDetail
    {
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        public double ReceivedQty { get; set; }

        public double RejectedQty { get; set; }

        public double StockedQty { get; set; }

    }
}