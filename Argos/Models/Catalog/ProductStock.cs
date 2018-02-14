using Argos.Models.BaseTypes;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Catalog
{
    [Table("ProductStock", Schema = "Catalog")]
    public class ProductStock:AuditableEntity
    {
        public int ProductStockId { get; set; }

        public int ProductId { get; set; }

        public int BranchId { get; set; }

        public double Quantity { get; set; }

        public double Reserved { get; set; }

        public virtual Product Product { get; set; }

        public virtual Branch Branch { get; set; }
    }
}