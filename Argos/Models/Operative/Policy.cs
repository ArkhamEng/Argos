using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Policy", Schema = "Operative")]
    public class Policy:AuditableEntity
    {
        [ForeignKey("Account")]
        public int PolicyId { get; set; }

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

        public virtual  Account Account { get; set; }

        public ICollection<SchedulePayment> SchedulePayments { get; set; }
        #endregion
    }
}