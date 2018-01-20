using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Finances
{
    [Table("Sale", Schema = "Finances")]
    public class Sale:AuditableEntity
    {
        public int SaleId { get; set; }

        public int ClientId { get; set; }

        public int EmployeeId { get; set; }

        [MaxLength(10)]
        public string Folio { get; set; }

        public DateTime SaleDate { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public double TotalAmount { get; set; }

        #region Navigation Properties

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Commission Commission { get; set; }

        public  ICollection<SaleDetail> SaleDetails { get; set; }

        #endregion

    }
}