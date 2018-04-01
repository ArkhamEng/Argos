using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("PolicyHistory", Schema = "Operative")]
    public class PolicyHistory:AuditableEntity
    {
        public int PolicyHistoryId { get; set; }

        public int PolicyId { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public int MaintFreq { get; set; }

        public int PayFreq { get; set; }


        #region Navigation Properties
        public virtual Policy Policy { get; set; }

        public virtual OperativeStatus Status { get; set; }

        #endregion
    }
}