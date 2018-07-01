using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("PriceChange", Schema = "Inventory")]
    public class ProductChange
    {
        public int ProductChangeId { get; set; }

        public int ProductId { get; set; }

        public double Profit { get; set; }

        public double LowetsProfit { get; set; }

        [DataType(DataType.Currency)]
        public double BuyPrice { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DataType(DataType.Currency)]
        public double LowestPrice { get; set; }

        public DateTime InsDate { get; set; }

        [MaxLength(30)]
        public string InsUser { get; set; }

      
        public virtual Product Product { get; set; }
    }
}