using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Operative;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("Product", Schema = "Inventory")]
    public class Product:AuditableCatalog
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
        [Display(Name = "Porcentaje")]
        public double Profit { get; set; }

        [Column(Order = 7)]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Column(Order = 8)]
        [Display(Name = "Porcentaje mínimo")]
        [Required]
        public double LowestProfit { get; set; }

        [Column(Order = 9)]
        [Display(Name = "Precio mínimo")]
        [DataType(DataType.Currency)]
        public double LowestPrice { get; set; }

        [Column(Order = 10)]
        [Display(Name = "Unidad")]
        [MaxLength(5)]
        public string MeasureUnitId { get; set; }

        /// <summary>
        /// Indica si el producto se almacena en inventario
        /// </summary>
        [Column(Order = 11)]
        public bool IsStockable { get; set; }

        /// <summary>
        /// Indica si el producto requiere rastreo por numero de serie
        /// </summary>
        [Column(Order = 12)]
        public bool IsTrackable { get; set; }

        /// <summary>
        /// Indica si el producto esta disponible para venta
        /// </summary>
        [Column(Order = 13)]
        public bool IsForSale { get; set; }

        /// <summary>
        /// Indica si el producto esta disponible para compra
        /// </summary>
        [Column(Order = 14)]
        public bool IsForPurchase { get; set; }

    
        #region Navigation Properties
        /// <summary>
        /// Unidad de medida
        /// </summary>
        public virtual MeasureUnit MeasureUnit { get; set; }

        /// <summary>
        /// Sub clasificación del producto
        /// </summary>
        public virtual SubCategory SubCategory { get; set; }

        public  ICollection<ProductStock> ProductStocks { get; set; }

        public ICollection<OperationDetail> OperationDetails { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        public ICollection<Compatibility> Compatibilities { get; set; }

        public ICollection<PriceChange> PriceChanges { get; set; }

        public ICollection<ExternalProduct> ExternalProducts { get; set; }

        #endregion

    }
}