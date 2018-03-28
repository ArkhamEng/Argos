using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("PackageDetail", Schema = "Inventory")]
    public class PackageDetail
    {
        [Column(Order =0), Key, ForeignKey("Package")]
        public int PackageId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public virtual Product Package { get; set; }

        public virtual Product Product { get; set; }
    }
}