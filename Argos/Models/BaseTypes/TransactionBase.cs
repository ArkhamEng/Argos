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

        public int TransactionStatusId { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public double TotalAmount { get; set; }

        [Display(Name = "Fecha de Venta")]
        public virtual DateTime TransactionDate { get; set; }

        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TransactionType TransactionType { get; set; }
        #endregion
    }
}