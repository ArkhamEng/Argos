using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("SchedulePayment", Schema = "Operative")]
    public class SchedulePayment:AuditableEntity
    {
        public int SchedulePaymentId { get; set; }

        public int? AccountId { get; set; }

        public int? PolicyId { get; set; }

        public int? ExtensionId { get; set; }

        public int? ServiceId { get; set; }

        [Display(Name ="Fecha Limite")]
        public DateTime PaymentDueDate { get; set; }

        [Display(Name = "Fecha de pago")]
        public DateTime PaidDate { get; set; }

        [MaxLength(100,ErrorMessage ="El comentario no debe exceder de 100 caractéres")]
        public string Comment { get; set; }

        [Display(Name="Monto")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public int Status { get; set; }

        #region Navigation Properties
        public virtual Account Account { get; set; }

        public virtual Extension Extension { get; set; }

        public virtual Policy Policy { get; set; }
        #endregion
    }
}