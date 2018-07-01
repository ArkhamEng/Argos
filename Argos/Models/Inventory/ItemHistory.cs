using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("ItemHistory", Schema = "Inventory")]
    public class ItemHistory:AuditableEntity
    {
        public int ItemHistoryId { get; set; }

        public int ItemId { get; set; }

        public int LocationId { get; set; }

        public double Quantity { get; set; }

        public virtual Item Item { get; set; }

        public virtual Storage Storage { get; set; }
    }
}