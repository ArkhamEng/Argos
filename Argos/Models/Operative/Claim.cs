using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Claim", Schema = "Operative")]
    public class Claim:AuditableEntity
    {
        public int ClaimId { get; set; }

        [MaxLength(10)]
        public string Folio { get; set; }

        public DateTime ClaimDate { get; set; }

        #region Navigation Properties

        public virtual Account Account { get; set; }

        public ICollection<ScheduleService> ScheduleService { get; set; }

        #endregion
    }
}