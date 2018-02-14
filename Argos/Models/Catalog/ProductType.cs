using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Catalog
{
    [Table("ProductType", Schema = "Catalog")]
    public class ProductType : AuditableEntity, ISelectable
    {
        [Column(Order =0)]
        public int ProductTypeId { get; set; }

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
        public ICollection<Product> Product { get; set; }

        public int Id
        {
            get
            {
                return ProductTypeId;
            }
        }
        #endregion
    }
}