using Argos.Models.BaseTypes;
using Argos.Models.Transaction;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.HumanResources
{
    [Table("Commission", Schema = "HumanResources")]
    public class Commission:AuditableEntity
    {
        [Column(Order =0),ForeignKey("Sale")]
        public int CommissionId { get; set; }

        [Column(Order = 1)]
        [Display(Name="Porcentaje")]
        public int Profit { get; set; }

        [Column(Order = 2)]
        [Display(Name = "Monto")]
        public double  Amount { get; set; }

        [Column(Order = 3)]
        public DateTime? PayDate { get; set; }

        [NotMapped]
        public bool IsPayed { get { return PayDate != null ? true : false; } }

        #region Navigation Properties
        public virtual Sale Sale { get; set; }

        #endregion
    }
}