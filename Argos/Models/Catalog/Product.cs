using Argos.Models.Finances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Catalog
{
    [Table("Product", Schema = "Catalog")]
    public class Product
    {
        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        [MaxLength(30)]
        public string Code { get; set; }

        public string Description { get; set; }

        [Display(Name = "Costo")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        public int Percentage { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        #region Navigation Properties

        public virtual ProductType ProductType { get; set; }

        public ICollection<SaleDetail> SaleDetail { get; set; }

        #endregion
    }
}