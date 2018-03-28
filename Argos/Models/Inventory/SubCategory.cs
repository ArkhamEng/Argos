using Argos.Models.BaseTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("SubCategory", Schema = "Inventory")]
    public class SubCategory:ISelectable
    {
        public int SubCategoryId { get; set; }

        public int CategoryId { get; set; }

        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Product> Products { get; set; }


        #region Not Mapped Properties
        [NotMapped]
        public string Value
        {
            get
            {
                return this.SubCategoryId.ToString();
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