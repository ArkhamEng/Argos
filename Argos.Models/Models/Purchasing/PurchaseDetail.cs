using Argos.Models.Business;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Purchasing
{
    [Table("PurchaseDetail", Schema = "Purchasing")]
    public class PurchaseDetail:Detail
    {
        public int PurchaseId { get; set; }

        [Display(Name = "Costo")]
        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        [Display(Name = "Ordenado")]
        public double OrderQty { get; set; }

        [Display(Name = "Total")]
        public double LineTotal { get; set; }

        [Display(Name = "Recibido")]
        public double ReceivedQty { get; set; }

        [Display(Name = "Rechazado")]
        public double RejectedQty { get; set; }

        [Display(Name = "Almacenado")]
        public double StockQty { get; set; }

        public virtual Purchase Purchase { get; set; }

    }
}