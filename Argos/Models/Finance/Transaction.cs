using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finance
{
    [Table("Transaction", Schema = "Finance")]
    public class Transaction:CashMovement
    {
        public int OperationId { get; set; }

        public virtual Operation Operation { get; set; }

        public virtual CreditNote CreditNote { get; set; }
    }
}