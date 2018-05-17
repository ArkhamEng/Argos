using Argos.Models.Enums;
using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finance
{
    [Table("CashMovement", Schema = "Finance")]
    public class CashMovement
    {
        public int CashMovementId { get; set; }

        public int CashSessionId { get; set; }

        public PayMethodes PayMethodId { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public DateTime InsDate { get; set; }

        public string InsUser { get; set; }

        public virtual CashSession CashSession { get; set; }

    }
}