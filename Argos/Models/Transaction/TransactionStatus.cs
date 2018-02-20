using Argos.Models.BaseTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("TransactionStatus", Schema = "Transaction")]
    public class TransactionStatus:Status
    {
        [Column(Order = 0)]
        public int TransactionStatusId { get; set; }

        [NotMapped]
        public override int Id { get{return TransactionStatusId;} }

        #region navigation Properties
        public virtual ICollection<Purchase> Purchase { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
        #endregion
    }
}