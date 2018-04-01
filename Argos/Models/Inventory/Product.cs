using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Operative;
using Argos.Models.Transaction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Product", Schema = "Inventory")]
    public class Product:AuditableEntity
    {
        [Column(Order = 0)]
        public int ProductId { get; set; }

        [Column(Order = 1),ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

        [Column(Order = 2)]
        [MaxLength(30)]
        [Required]
        public string Code { get; set; }

        [Column(Order = 3)]
        [MaxLength(250)]
        [Required]
        public string Description { get; set; }

        [Column(Order = 4)]
        [MaxLength(50)]
        [Required]
        public string TradeMark { get; set; }

        [Column(Order = 5)]
        [Display(Name = "Costo")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        [Column(Order = 6)]
        public int HighestProfit { get; set; }

        [Column(Order = 7)]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double HighestPrice { get; set; }

        [Column(Order = 8)]
        [Display(Name = "Precio Minimo")]
        [Required]
        public int LowestProfit { get; set; }

        [Column(Order = 9)]
        [Display(Name = "Precio Minimo")]
        [DataType(DataType.Currency)]
        public double LowestPrice { get; set; }

        [Column(Order = 10)]
        [Display(Name = "Unidad")]
        [MaxLength(5)]
        public string MeasureUnitId { get; set; }

        [Column(Order = 11)]
        public bool StockRequired { get; set; }


        #region Navigation Properties
        public virtual MeasureUnit MeasureUnit { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public ICollection<SaleDetail> SaleDetail { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        public ICollection<Compatibility> Compatibilities { get; set; }

        public ICollection<Equivalence> Equivalences { get; set; }

        public ICollection<PriceHistory> PriceHistories { get; set; }

        [InverseProperty("Package")]
        public virtual ICollection<PackageDetail> PackageDetails { get; set; }

        #endregion

        #region Not Mapped
        [NotMapped]
        public double Available { get; set; }

        [NotMapped]
        public string Row { get; set; }

        [NotMapped]
        public string Ledge { get; set; }

        [NotMapped]
        public ICollection<CarModel> ModelCompatibilities { get; set; }

        #endregion
    }
}