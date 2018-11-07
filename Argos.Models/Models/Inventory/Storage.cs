using Argos.Models.BaseTypes;
using Argos.Models.Config;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Storage", Schema = "Inventory")]
    public class Storage:AuditableEntity
    {
        public int StorageId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public string Name { get; set; }

        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public ICollection<ItemStorage> ItemLocations  { get; set; }

        public ICollection<ItemHistory> ItemHistories { get; set; }

        #endregion
    }
}