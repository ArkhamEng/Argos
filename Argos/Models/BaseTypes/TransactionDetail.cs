using Argos.Models.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class TransactionDetail
    {
        public int ProductId { get; set; }

        public double Quantity { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public int TaxPercent { get; set; }

        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        public double SubTotal { get; set; }

        [Display(Name = "IVA")]
        [DataType(DataType.Currency)]
        public double TaxAmount { get; set; }

        [Display(Name = "Monto Total")]
        [DataType(DataType.Currency)]
        public double Total { get; set; }

        public virtual Product Product { get; set; }

    }
}