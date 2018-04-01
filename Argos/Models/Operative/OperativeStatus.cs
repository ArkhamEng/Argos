using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Status", Schema = "Operative")]
    public class OperativeStatus
    {
        [Key]
        public int StatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; }

        #region Navigation Properties

        public ICollection<Account> Accounts { get; set; }

        public ICollection<Policy> Policies { get; set; }

        public ICollection<PolicyHistory> PolicyHistories { get; set; }


        #endregion
    }
}