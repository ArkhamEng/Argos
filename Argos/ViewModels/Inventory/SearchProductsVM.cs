using Argos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.ViewModels.Inventory
{
    public class SearchProductsVM
    {
        public ProductFilters Filter { get; set; }

        public ProductComplement Lists { get; set; }

        public ICollection<ProductVM> Products { get; set; }

        public SearchProductsVM(ApplicationDbContext db)
        {
            this.Products = new List<ProductVM>();
            this.Lists = new ProductComplement(db);
            this.Filter = new ProductFilters();
        }
    }

  
    public class ProductFilters
    {
        [Display(Name ="Categorías")]
        public int? CategoryId { get; set; }

        public int? ProductId { get; set; }

        [Display(Name = "Sub Categorías")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "Descripción")]
        public string Text { get; set; }

        public int? MakerId { get; set; }

        public int? ModelId { get; set; }

        public int? Year { get; set; }
    }
}