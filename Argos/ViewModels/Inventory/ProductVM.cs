using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using Argos.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using Argos.ViewModels.Generic;
using Argos.Models;
using System.ComponentModel.DataAnnotations;

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
                return this.Product.TradeMark ?? Cons.NotApply;
            }
        }

        public string ImagePath
        {
            get
            {
                if (this.Product.ProductImages != null && this.Product.ProductImages.Count > 0)
                    return this.Product.ProductImages.First().Path;
                else
                    return Cons.NoImage;
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
                    return Cons.ResponseDanger;

                if (this.Product.IsStockable && (this.Stock == null ||  this.Stock.Quantity == 0))
                    return Cons.ResponseWarning;

                return string.Empty;
            }
        }

        /// <summary>
        /// Espacios de imagenes disponibles
        /// </summary>
        public int Slots
        {
            get { return (Cons.ImagesPerProduct - this.Images.Count); }
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

        public ProductVM(ApplicationDbContext db)
        {
            this.Product = new Product();
            this.Complement = new ProductComplement(db);
            this.Stock = new ItemStorage();
            this.Product.ProductImages = new List<ProductImage>();
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
            this.Categories = new List<ISelectable>().ToSelectList();
            this.SubCategories = new List<ISelectable>().ToSelectList();
            this.Makers = new List<ISelectable>().ToSelectList();
            this.Models = new List<ISelectable>().ToSelectList();
            this.Units = new List<ISelectable>().ToSelectList();
        }

        public ProductComplement(ApplicationDbContext db)
        {
            this.Categories = db.Categories.OrderBy(c=> c.Name).ToSelectList();
            this.Makers     = db.Makers.OrderBy(c => c.Name).ToSelectList();
            this.Units      = db.MeasureUnits.OrderBy(c => c.Name).ToSelectList();

            this.Models = new List<ISelectable>().ToSelectList();
            this.SubCategories = new List<ISelectable>().ToSelectList();
        }
    }
}