using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Catalog
{
    [Table("ProductType", Schema = "Catalog")]
    public class ProductType
    {
        public int ProductTypeId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        #region Navigation Property
        public ICollection<Product> Product { get; set; }
        #endregion
    }
}