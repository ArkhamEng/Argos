using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("PriceChange", Schema = "Inventory")]
    public class PriceChange
    {
        public int PriceChangeId { get; set; }

        public int ProductId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [DataType(DataType.Currency)]
        public double HighestPrice { get; set; }

        [DataType(DataType.Currency)]
        public double LowestPrice { get; set; }

        public DateTime InsDate { get; set; }

        [MaxLength(30)]
        public string InsUser { get; set; }

      
        public virtual Product Product { get; set; }
    }
}