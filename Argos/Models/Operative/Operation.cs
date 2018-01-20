using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Operation", Schema = "Operative")]
    public class Operation:AuditableEntity
    {
        public int OperationId { get; set; }

        public int ServiceId { get; set; }

        [MaxLength(10)]
        public string Folio { get; set; }

        public DateTime RiseDate { get; set; }

        public DateTime ScheduleDate { get; set; }

        #region Navigation Properties

        public virtual Service Service { get; set; }

        #endregion
    }
}