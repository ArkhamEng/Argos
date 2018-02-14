using Argos.Models.BaseTypes;
using Argos.Models.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("Product", Schema = "Catalog")]
    public class Product:AuditableEntity
    {
        [Column(Order = 0)]
        public int ProductId { get; set; }

        [Column(Order = 1)]
        public int ProductTypeId { get; set; }

        [Column(Order = 2)]
        [MaxLength(30)]
        public string Code { get; set; }

        [Column(Order = 3)]
        public string Description { get; set; }

        [Column(Order = 4)]
        [Display(Name = "Costo")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        [Column(Order = 5)]
        public int Percentage { get; set; }

        [Column(Order = 6)]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Column(Order = 7)]
        public int MiddlePercentage { get; set; }

        [Column(Order = 8)]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double MiddlePrice { get; set; }

        [Column(Order = 9)]
        public int LowerPercentage { get; set; }

        [Column(Order = 10)]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double LowerPrice { get; set; }

        #region Navigation Properties

        public virtual ProductType ProductType { get; set; }

        public ICollection<SaleDetail> SaleDetail { get; set; }

        public ICollection<ProductStock> ProductStocks { get; set; }

        #endregion
    }
}