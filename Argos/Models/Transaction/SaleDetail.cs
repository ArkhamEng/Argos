using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("SaleDetail", Schema = "Transaction")]
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