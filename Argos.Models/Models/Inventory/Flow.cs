using Argos.Models.BaseTypes;
using Argos.Models.Business;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Flow", Schema = "Inventory")]
    public class Flow:InsAudit
    {
        public int FlowId { get; set; }

        public int ItemId { get; set; }

        public int DetailId { get; set; }

        [ForeignKey("FlowDirection")]
        public FlowDirections Direction { get; set; }

        public double Quantity { get; set; }

        #region Navigation Property
        public virtual Item Item { get; set; }

        public virtual Detail Detail { get; set; }

        public virtual FlowDirection FlowDirection { get; set; }
        #endregion

    }
}