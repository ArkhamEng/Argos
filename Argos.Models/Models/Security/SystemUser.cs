using Argos.Models.BaseTypes;
using Argos.Models.Business;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Security
{
    [Table("SystemUser", Schema = "Security")]
    public class SystemUser:AuditableEntity
    {
        [Key, ForeignKey("Person")]
        public int SystemUserId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

     
        #region Navigation Properties
        public virtual ApplicationUser User { get; set; }

        public virtual Person Person { get; set; }
        #endregion
    }
}