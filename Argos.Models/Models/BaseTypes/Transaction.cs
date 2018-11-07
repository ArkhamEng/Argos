using Argos.Models.Config;
using Argos.Common.Enums;
using Argos.Models.Sales;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.BaseTypes
{
    public abstract class Transaction
    {
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public int ShipMethodId { get; set; }

        [Display(Name = "Forma de pago")]
        public PayForms PayFormId { get; set; }

        [MaxLength(8)] 
        public string PaymentType { get; set; }

        [Display(Name = "Fecha Orden")]
        [DataType(DataType.Date)]
        public DateTime OrderDate  { get; set; }


        [Display(Name = "Fecha Expira")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Fecha Entrega")]
        [DataType(DataType.Date)]
        public DateTime ShipDate { get; set; }

        [Display(Name = "Sub Total")]
        public double  SubTotal { get; set; }

        [Display(Name = "IVA")]
        public double TaxAmt { get; set; }

        [Display(Name = "Envío")]
        public double Freight { get; set; }

        [Display(Name = "Total")]
        public double TotalDue { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ShipMethod ShipMethod { get; set; }

        public virtual PayForm PayForm { get; set; }

    }
}