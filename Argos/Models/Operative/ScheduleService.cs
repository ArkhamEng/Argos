using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("ScheduleService", Schema = "Operative")]
    public class ScheduleService:AuditableEntity
    {
        public int ScheduleServiceId { get; set; }

        public int? AccountId { get; set; }

        public int? ExtensionId { get; set; }

        public int? ClaimId { get; set; }

        [Display(Name = "Fecha Limite")]
        public DateTime ProgrammedDate { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Fecha Final")]
        public DateTime? EndDate { get; set; }

        public int Status { get; set; }


        #region Navigation Properties

        public virtual Account Account { get; set; }

        public virtual Extension Extension { get; set; }

        public virtual Claim Claim { get; set; }

        #endregion
    }
}