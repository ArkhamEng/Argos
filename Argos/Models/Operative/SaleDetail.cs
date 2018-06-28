using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("SaleDetail", Schema = "Operative")]
    public class SaleDetail : Detail
    {
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        public double PriceDiscount { get; set; }

        public int SpecialOfferId { get; set; }

    }
}