using Argos.Models.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("TransferDetail", Schema = "Operative")]
    public class TransferDetail:OperationDetail
    {
        [MaxLength(100)]
        public string Comment { get; set; }

        public double QtySend { get; set; }

        public double QtyOnSite { get; set; }
    }
}