using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using Argos.Models.Production;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Security
{
    [Table("ClientUser", Schema = "Security")]
    public class ClientUser : AuditableEntity
    {
        [Column(Order = 0), Key, ForeignKey("User")]
        public string UserId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Client")]
        public int ClientId { get; set; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; }

        public virtual Client Client { get; set; }
        #endregion
    }
}