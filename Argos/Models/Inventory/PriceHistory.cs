using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("PriceHistory", Schema = "Inventory")]
    public class PriceHistory
    {
        public int PriceHistoryId { get; set; }

        public int ProductId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual Product Product { get; set; }
    }
}