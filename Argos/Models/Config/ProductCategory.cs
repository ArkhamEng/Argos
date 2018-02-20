using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("ProductCategory", Schema = "Config")]
    public class ProductCategory : AuditableEntity, ISelectable
    {
        [Column(Order =0)]
        public int ProductCategoryId { get; set; }

        [Column(Order = 1)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column(Order = 2)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Column(Order = 3)]
        [Required]
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        #region Navigation Property
        public ICollection<ProductSubCategory> ProductSubCategories { get; set; }

        public int Id
        {
            get
            {
                return ProductCategoryId;
            }
        }
        #endregion
    }
}