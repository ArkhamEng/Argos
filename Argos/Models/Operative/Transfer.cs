using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Transfer", Schema = "Operative")]
    public class Transfer:Operation
    {
        [ForeignKey("DestBranch")]
        public int DestBranchId { get; set; }

        public DateTime DeliveryDate { get; set; }

        [MaxLength(100)]
        public string Receiver { get; set; }

        [MaxLength(100)]
        public string Carrier { get; set; }

        public virtual Branch DestBranch { get; set; }

    }
}