using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.BaseTypes
{
    public abstract class Transaction:AuditableEntity
    {
        public int BranchId { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public virtual DateTime? DuDate { get; set; }

        public int TaxPercentage { get; set; }

        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        public double SubTotal { get; set; }

        [Display(Name = "IVA")]
        [DataType(DataType.Currency)]
        public double TaxAmount { get; set; }

        [Display(Name = "Monto Total")]
        [DataType(DataType.Currency)]
        public double Total { get; set; }


        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public virtual TransStatus Status { get; set; }

        public virtual TransType Type { get; set; }
        #endregion
    }
}