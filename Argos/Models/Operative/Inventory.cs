using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Inventory", Schema = "Operative")]
    public class Inventory:AuditableEntity
    {
        [Column(Order =0)]
        public int InventoryId { get; set; }

        [Column(Order = 2)]
        public int ProductId { get; set; }

        [Column(Order = 3)]
        public int BranchId { get; set; }

        [Column(Order = 4)]
        public double Available { get; set; }

        [Column(Order = 5)]
        public double Reserved { get; set; }

        [Column(Order = 6)]
        [MaxLength(40)]
        public string Row { get; set; }

        [Column(Order = 7)]
        [MaxLength(40)]
        public string Ledge { get; set; }

        #region Navigation Properties
        public virtual Product Product { get; set; }

        public virtual Branch Branch { get; set; }
        #endregion
    }
}