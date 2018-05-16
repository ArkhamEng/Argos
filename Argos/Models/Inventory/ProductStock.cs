﻿using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Stock", Schema = "Inventory")]
    public class ProductStock:AuditableEntity
    {
        [Column(Order = 0), Key, ForeignKey("Branch")]
        public int BranchId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        [Column(Order = 3)]
        public double Quantity { get; set; }

        [Column(Order = 4)]
        [MaxLength(20)]
        public string SerialNumber { get; set; }


        [Column(Order = 5)]
        [MaxLength(10)]
        public string Shelf { get; set; }

        [Column(Order = 6)]
        [MaxLength(10)]
        public string Bin { get; set; }


        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public virtual Product Product { get; set; }

        public ICollection<Serial> Serials { get; set; }

        public ICollection<StockMovement> StockMovements { get; set; }


        #endregion
    }
}