using Argos.Common;
using Argos.Common.Constants;
using Argos.Common.Interfaces;
using Argos.Models;
using Argos.Models.Interfaces;
using Argos.Models.Inventory;
using Argos.ViewModels.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Argos.ViewModels.Inventory
{
    public class ProductVM:AuthEntity
    {
        public Product Product
        {
            get { return (Product)Catalog; } 
            set { Catalog = value; }
        }

        public ItemStorage Stock { get; set; }

        public ProductComplement Complement { get; set; }

        public string TradeMark
        {
            get
            {
                return this.Product.TradeMark ?? Labels.NotApply;
            }
        }

        public string ImagePath
        {
            get
            {
                if (this.Product.ProductImages != null && this.Product.ProductImages.Count > 0)
                    return this.Product.ProductImages.First().Path;
                else
                    return URis.NoImage;
            }
        }

        /// <summary>
        /// indica la clase a aplicar sobre la fila en el catálogo
        /// </summary>
        public string RowState
        {
            get
            {
                if (!this.Product.IsActive)
                    return Styles.Colors.Danger;

                if (this.Product.IsStockable && (this.Stock == null ||  this.Stock.Quantity == 0))
                    return Styles.Colors.Warning;

                return string.Empty;
            }
        }

        /// <summary>
        /// Espacios de imagenes disponibles
        /// </summary>
        public int Slots
        {
            get { return (Numbers.Config.ImagesPerProduct - this.Images.Count); }
        }

        /// <summary>
        /// Imagenes guardadas en base de datos
        /// </summary>
        public List<ProductImage> Images
        {
            get
            {
                return (this.Product.ProductImages ?? new List<ProductImage>()).ToList();
            }
        }

        /// <summary>
        /// Ids de imagenes a borrar de la base de datos
        /// </summary>
        public List<int> ToDelete { get; set; }

        /// <summary>
        /// Nuevas imagenes para guardar
        /// </summary>
        public List<HttpPostedFileBase> NewImages { get; set; }

        public ProductVM ()
        {
            this.Product        = new Product();
            this.Complement     = new ProductComplement();
            this.Stock          = new ItemStorage();
            this.ToDelete       = new List<int>();
            this.NewImages      = new List<HttpPostedFileBase>();
        }

        public ProductVM(IAppCache cache)
        {
            this.Product = new Product();
            this.Stock = new ItemStorage();
            this.Product.ProductImages = new List<ProductImage>();
            this.Complement = new ProductComplement(cache);
        }
    }

    public class  ProductComplement
    {
        public SelectList Categories { get; set; }

        public SelectList SubCategories { get; set; }

        public SelectList Makers { get; set; }

        public SelectList Units { get; set; }

        public SelectList Models { get; set; }

        [Display(Name="Categorias")]
        public int CategoryId { get; set; }

        public ProductComplement()
        {
            this.Categories    = new List<ISelectable>().ToSelectList();
            this.SubCategories = new List<ISelectable>().ToSelectList();
            this.Makers        = new List<ISelectable>().ToSelectList();
            this.Models        = new List<ISelectable>().ToSelectList();
            this.Units         = new List<ISelectable>().ToSelectList();
        }

        public ProductComplement(IAppCache cache)
        {
            this.Categories = cache.Categories.ToSelectList();
            this.Makers     = cache.Makers.ToSelectList();
            this.Units      = cache.MeasureUnits.ToSelectList();

            this.Models = new List<ISelectable>().ToSelectList();
            this.SubCategories = new List<ISelectable>().ToSelectList();
        }
    }
}