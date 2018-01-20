using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("PolicyDetail", Schema = "Operative")]
    public class PolicyDetail:AuditableEntity
    {
        [ForeignKey("Service")]
        public int PolicyDetailId { get; set; }

        [Display(Name ="Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Periodo pago")]
        public int PaymentPeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia pago")]
        public int PaymentTolerance { get; set; }

        [Required]
        [Display(Name = "Periodo mantto")]
        public int MaintenancePeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia mantto")]
        public int MaintenanceTolerance { get; set; }

        #region Navigation Properties

        public virtual  Service Service { get; set; }

        #endregion
    }
}