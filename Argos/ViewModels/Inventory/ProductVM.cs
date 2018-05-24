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

        public ProductStock Stock { get; set; }

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

        
        public ProductVM ()
        {
            this.Product        = new Product();
            this.Complement     = new ProductComplement();
            this.Stock          = new ProductStock();            
        }

        public ProductVM(ApplicationDbContext db)
        {
            this.Product = new Product();
            this.Complement = new ProductComplement(db);
            this.Stock = new ProductStock();
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
            this.Categories = db.Categories.ToSelectList();
            this.Makers = db.Makers.ToSelectList();
            this.Units = db.MeasureUnits.ToSelectList();

            this.Models = new List<ISelectable>().ToSelectList();
            this.SubCategories = new List<ISelectable>().ToSelectList();
        }
    }
}