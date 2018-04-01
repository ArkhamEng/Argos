using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("Status", Schema = "Transaction")]
    public class TransStatus
    {
        [Key]
        public int StatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; }

        public bool ForSale { get; set; }

        public bool ForPurchase { get; set; }

        #region Navigation Properties

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        #endregion
    }
}