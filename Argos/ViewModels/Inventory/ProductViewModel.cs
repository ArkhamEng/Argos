using Argos.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Argos.ViewModels.Inventory
{
    [NotMapped]
    public class ProductViewModel:Product
    {
        [NotMapped]
        public SelectList Categories { get; set; }

        [NotMapped]
        public SelectList SubCategories { get; set; }

        [NotMapped]
        public SelectList CarMakes { get; set; }

        [NotMapped]
        public SelectList CarModels { get; set; }

    }
}