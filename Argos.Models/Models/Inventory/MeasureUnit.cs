using Argos.Models.BaseTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Argos.Models.Interfaces;

namespace Argos.Models.Inventory
{
    [Table("MeasureUnit", Schema = "Inventory")]
    public class MeasureUnit:AuditableEntity,ISelectable
    {
        [MaxLength(5)]
        [Column(Order = 0)]
        public string MeasureUnitId { get; set; }

        [MaxLength(15)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [MaxLength(50)]
        [Column(Order = 2)]
        public string Description { get; set; }

     
        public ICollection<Product> Products { get; set; }

        #region NotMapped Entities
        [NotMapped]
        public string Value
        {
           get { return MeasureUnitId; }
        }

        [NotMapped]
        public string Text
        {
            get { return Name; }
        }
        #endregion
    }
}