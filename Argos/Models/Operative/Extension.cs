using Argos.Models.BaseTypes;
using Argos.Models.Finances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Extension", Schema = "Operative")]
    public class Extension:AuditableEntity
    {
        public int ExtensionId { get; set; }

        public int AccountId { get; set; }

        [Display(Name = "Fecha de Contratación")]
        public DateTime ContractDate { get; set; }


        [Display(Name = "Precio")]
        public double Price { get; set; }

       
        #region Navigation Properties

        public virtual Account Account { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<SchedulePayment> SchedulePayments { get; set; }

        public ICollection<ScheduleService> ScheduleServices { get; set; }

        #endregion
    }
}