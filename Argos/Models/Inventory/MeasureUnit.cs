using Argos.Models.BaseTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("MeasureUnit", Schema = "Inventory")]
    public class MeasureUnit:AuditableEntity
    {
        [MaxLength(5)]
        [Column(Order = 0)]
        public string MeasureUnitId { get; set; }

        [MaxLength(50)]
        [Column(Order = 1)]
        public string Description { get; set; }

     
        public ICollection<Product> Products { get; set; }
    }
}