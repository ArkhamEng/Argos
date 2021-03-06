﻿using Argos.Models.BaseTypes;
using Argos.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Category", Schema = "Inventory")]
    public class Category : ActivableAudit, ISelectable
    {
        [Column(Order =0)]
        public int CategoryId { get; set; }

        [Column(Order = 1)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column(Order = 2)]
        [MaxLength(100)]
        public string Description { get; set; }

      

        #region Navigation Property
        public ICollection<SubCategory> ProductSubCategories { get; set; }

        #endregion

        #region Not Mapped Properties
        [NotMapped]
        public string Value
        {
            get
            {
                return this.CategoryId.ToString();
            }
        }

        [NotMapped]
        public string Text
        {
            get
            {
                return this.Name;
            }
        }
        #endregion
        
    }
}