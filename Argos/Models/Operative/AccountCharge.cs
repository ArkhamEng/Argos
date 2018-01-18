using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("AccountCharge", Schema = "Operative")]
    public class AccountCharge
    {
        public int AccountChargeId { get; set; }

        public int AccountId { get; set; }

        public int ChargeType { get; set; }

        public int Status { get; set; }

        public double Amount { get; set; }

        public DateTime ProgrammedDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public int? ServiceId { get; set; }

        public virtual Account Account { get; set; }
    }
}