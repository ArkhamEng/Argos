using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("Serial",Schema ="Inventory")]
    public class Serial:AuditableEntity
    {
        public int SerialId { get; set; }

        public int ProductStockId { get; set; }

        [MaxLength(30)]
        public string SerialNumber { get; set; }

        public virtual ProductStock ProductStock { get; set; }

        public ICollection<StockMovement> StockMovement { get; set; }

    }
}