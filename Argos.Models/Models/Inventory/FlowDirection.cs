using Argos.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("FlowDirection", Schema = "Inventory")]
    public class FlowDirection
    {
        public FlowDirections FlowDirectionId { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }

        public ICollection<Flow> Flows { get; set; }
    }
}