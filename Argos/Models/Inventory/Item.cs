using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("Item", Schema = "Inventory")]
    public class Item
    {
        public int ItemId { get; set; }

        public int ProductId { get; set; }

        public string SerialNumber { get; set; }

        public virtual Product Product { get; set; }

        public ICollection<ItemLocation> ItemLocations { get; set; }

        public ICollection<Movement> Movements { get; set; }

        public ICollection<ItemHistory> ItemHistories { get; set; }

    }
}