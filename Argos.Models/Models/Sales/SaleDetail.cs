using Argos.Models.Business;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Sales
{
    [Table("SaleDetail", Schema = "Sales")]
    public class SaleDetail: Detail
    {
        public int SaleId { get; set; }

        public double OrderQty { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        public double LineTotal { get; set; }

        public double UnitPriceDiscount { get; set; }

        public virtual Sale Sale { get; set; }

    }
}