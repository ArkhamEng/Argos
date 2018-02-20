using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("ProductSubCategory", Schema = "Config")]
    public class ProductSubCategory:ISelectable
    {
        public int ProductSubCategoryId { get; set; }

        public int ProductCategoryId { get; set; }

        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public ICollection<Product> Products { get; set; }

        public int Id
        {
            get
            {
                return this.ProductSubCategoryId;
            }
        }
    }
}