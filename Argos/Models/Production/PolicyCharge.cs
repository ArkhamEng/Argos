using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Production
{
    [Table("PolicyCharge", Schema = "Production")]
    public class PolicyCharge:AuditableEntity
    {
        public int PolicyChargeId { get; set; }

        public int PolicyId { get; set; }

        public DateTime ScheduleDate { get; set; }

        public DateTime? PayedDate { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public virtual Policy Policy { get; set; }
    }
}