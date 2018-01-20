using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Security
{
    [Table("UserEmployee", Schema = "Security")]
    public class EmployeeUser:AuditableEntity
    {
        [Column(Order =0),Key,ForeignKey("User")]
        public string UserId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; }

         public virtual Employee Employee { get; set; }
        #endregion
    }
}