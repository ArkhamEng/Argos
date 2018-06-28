using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Location", Schema = "Inventory")]
    public class Location:AuditableEntity
    {
        public int LocationId { get; set; }

        public int BranchId { get; set; }

        public string Name { get; set; }

        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public ICollection<ItemLocation> ItemLocations  { get; set; }

        public ICollection<Movement> ItemHistories { get; set; }

        #endregion
    }
}