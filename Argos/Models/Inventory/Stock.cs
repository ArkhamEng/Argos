using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Stock", Schema = "Inventory")]
    public class Stock:AuditableEntity
    {
        [Column(Order =0)]
        public int StockId { get; set; }

        [Column(Order = 2)]
        public int ProductId { get; set; }

        [Column(Order = 3)]
        public int BranchId { get; set; }

        [Column(Order = 4)]
        public double Available { get; set; }

        [Column(Order = 5)]
        public double Reserved { get; set; }

        [Column(Order = 6)]
        public double MaxQuantity { get; set; }

        [Column(Order = 7)]
        public double MinQuantity { get; set; }

        [Column(Order = 8)]
        [MaxLength(100)]
        public string Comment { get; set; }

        [Column(Order = 9)]
        [MaxLength(40)]
        public string Row { get; set; }

        [Column(Order = 10)]
        [MaxLength(40)]
        public string Ledge { get; set; }

        #region Navigation Properties
        public virtual Product Product { get; set; }

        public virtual Branch Branch { get; set; }
        #endregion
    }
}