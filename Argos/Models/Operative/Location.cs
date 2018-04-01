using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Location", Schema = "Operative")]
    public class AccountLocation: LocatableEntity
    {
        [Column(Order = 0), Key, ForeignKey("Account")]
        public int LocationId { get; set; }

        #region Navigation Properties
        public virtual Account Account { get; set; }
        #endregion
    }
}