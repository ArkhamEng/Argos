using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class TransactionBase:AuditableEntity
    {
        public int EmployeeId { get; set; }

        public int BranchId { get; set; }

        public int TransactionTypeId { get; set; }

        public int StatusId { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual DateTime? DuDate { get; set; }

        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        public double SubTotal { get; set; }

        [Display(Name = "IVA")]
        [DataType(DataType.Currency)]
        public double TaxAmount { get; set; }

        [Display(Name = "Monto Total")]
        [DataType(DataType.Currency)]
        public double DueTotal { get; set; }


        #region Navigation Properties
        public virtual Branch Branch { get; set; }

   
        public virtual Status Status { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TransactionType TransactionType { get; set; }
        #endregion
    }
}