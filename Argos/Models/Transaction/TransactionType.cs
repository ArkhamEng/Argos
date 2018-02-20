using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("TransactionType", Schema = "Transaction")]
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Sale> Purchase { get; set; }
    }
}