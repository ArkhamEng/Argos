using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Production
{
    [Table("Status", Schema = "Production")]
    public class OperativeStatus:ISelectable
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

        [NotMapped]
        public string Value { get { return this.StatusId.ToString(); } }

        [NotMapped]
        public string Text { get { return this.Name; } }
       

        #endregion
    }
}