﻿using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("ItemLocation", Schema = "Inventory")]
    public class ItemLocation:AuditableEntity
    {
        [Column(Order =0),Key]
        public int ItemId { get; set; }

        [Column(Order = 1), Key]
        public int LocationId { get; set; }

        public double Quantity { get; set; }

        public virtual Item Item { get; set; }

        public virtual Location Location { get; set; }

    }
}