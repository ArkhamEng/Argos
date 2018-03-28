using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Equivalence", Schema = "Inventory")]
    public class Equivalence
    {
        [Column(Order =0),Key,ForeignKey("Product")]
        public int ProductId { get; set; }

        [Column(Order = 1), Key, ForeignKey("ExternalProduct")]
        public int ExternalProductId { get; set; }

        public DateTime InsDate { get; set; }

        [MaxLength(30)]
        public string InsUser { get; set; }

        #region Navigation Properties
        public virtual Product Product { get; set; }

        public virtual ExternalProduct ExternalProduct { get; set; }
        #endregion
    }
}