using Argos.Common;
using Argos.Common.Constants;
using Argos.Models.BaseTypes;
using Argos.Models.Business;
using Argos.Models.Config;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Argos.Models.Inventory
{
    [Table("Product", Schema = "Inventory")]
    public class Product:AuditableCatalog
    {
        [Column(Order =0)]
        public int ProductId { get; set; }

        [Column(Order = 1)]
        [Display(Name = "Sub Categoría")]
        public int SubCategoryId { get; set; }

        [Column(Order = 2)]
        [Display(Name = "Unidad")]
        [MaxLength(5)]
        public string MeasureUnitId { get; set; }

        [Column(Order = 3)]
        [MaxLength(30)]
        [Required]
        [Index("IDX_Code_Description_TradeMark",IsUnique =false,Order =0)]
        public string Code { get; set; }

        [Column(Order = 4)]
        [MaxLength(250)]
        [Required]
        [Index("IDX_Code_Description_TradeMark", IsUnique = false, Order = 1)]
        public string Description { get; set; }

        [Column(Order = 5)]
        [MaxLength(50)]
        [Required]
        [Index("IDX_Code_Description_TradeMark", IsUnique = false, Order = 2)]
        public string TradeMark { get; set; }

        [Column(Order =6)]
        [MaxLength(30)]
        [Required]
        [Display(Name = "Clave SAT")]
        public string SatCode { get; set; }

        [Column(Order = 7)]
        [Display(Name = "Precio de compra")]
        [DataType(DataType.Currency)]
        public double BuyPrice { get; set; }

        [Column(Order = 8)]
        [Display(Name = "Porcentaje")]
        public double Profit { get; set; }

        [Column(Order = 9)]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Column(Order = 10)]
        [Display(Name = "Porcentaje mínimo")]
        [Required]
        public double LowestProfit { get; set; }

        [Column(Order = 11)]
        [Display(Name = "Precio mínimo")]
        [DataType(DataType.Currency)]
        public double LowestPrice { get; set; }


        /// <summary>
        /// Indica si el producto se almacena en inventario
        /// </summary>
        [Column(Order = 12)]
        [Display(Name = "Almacenable")]
        public bool IsStockable { get; set; }

        /// <summary>
        /// Indica si el producto requiere rastreo por numero de serie
        /// </summary>
        [Column(Order = 13)]
        [Display(Name = "Rastreable")]
        public bool IsTrackable { get; set; }

        /// <summary>
        /// Indica si el producto o servicio es para venta
        /// </summary>
        [Column(Order = 14)]
        [Display(Name ="Para venta")]
        public bool IsForSale { get; set; }

        /// <summary>
        /// Indica si el producto o servicio esta disponible para compra
        /// </summary>
        [Column(Order = 15)]
        [Display(Name = "Para compra")]
        public bool IsForPurchase { get; set; }

        [Column(Order = 16)]
        [Display(Name = "Stock Máximo")]
        public double MaxQuantity { get; set; }

        [Column(Order = 17)]
        [Display(Name = "Stock mínimo")]
        public double MinQuantity { get; set; }

        [Display(Name = "Precio mínimo con IVA")]
        public double TaxedMinPrice { get; set; }

        [Display(Name = "Precio máximo con IVA")]
        public double TaxedMaxPrice { get; set; }

        #region Navigation Properties

        public virtual SubCategory SubCategory { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }

        public virtual ICollection<ProductChange> ProductChanges { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<Compatibility> Compatibilities { get; set; }

        public virtual ICollection<Detail> Details { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        #endregion

        public Product()
        {
            this.ProductImages = new List<ProductImage>();
        }

        [NotMapped]
        public string Image
        {
            get
            {
                return this.ProductImages.Count > Numbers.Zero ? this.ProductImages.First().Path : URis.NoImage;
            }
        }
    }
}