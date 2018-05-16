using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Production
{
    [Table("Service", Schema = "Production")]
    public class Service:AuditableEntity
    {
        public int ServiceId { get; set; }

        public int AccountId { get; set; }

        public int ServiceTypeId { get; set; }

        [MaxLength(10)]
        public string Folio { get; set; }

        [Display(Name ="Fecha programada")]
        public DateTime ScheduleDate { get; set; }

        #region Navigation Properties

        public virtual Account Account { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        #endregion
    }
}