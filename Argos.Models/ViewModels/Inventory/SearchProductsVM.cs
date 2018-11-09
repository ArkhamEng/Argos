using Argos.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Argos.ViewModels.Inventory
{
    public class SearchProductsVM
    {
        public ProductFilters Filter { get; set; }

        public ProductComplement Lists { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }

        public SearchProductsVM()
        {
            this.Products = new List<ProductViewModel>();
            this.Filter   = new ProductFilters();
        }

        public SearchProductsVM(IAppCache cache)
        {
            this.Products = new List<ProductViewModel>();
            this.Lists    = new ProductComplement(cache);
            this.Filter   = new ProductFilters();
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