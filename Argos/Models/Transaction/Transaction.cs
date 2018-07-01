using Argos.Models.Config;
using Argos.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    
    public abstract class Transaction
    {
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public DateTime OrderDate  { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime ShipDate { get; set; }

        public double  SubTotal { get; set; }

        public double TaxAmt { get; set; }

        public double Freight { get; set; }

        public double TotalDue { get; set; }

        public TransactionStatus StatusId { get; set; }

        public virtual Status Status { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ShipMethod ShipMethod { get; set; }

    }
}