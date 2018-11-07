using Argos.Common;
using Argos.Models.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("ProductImage", Schema = "Inventory")]
    public class ProductImage:AuditableEntity
    {
        [Column(Order =0)]
        public int ProductImageId { get; set; }

        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(80)]
        [Column(Order = 2)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        [Column(Order = 3)]
        public string Path { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(Order = 4)]
        public string Type { get; set; }

        [Column(Order = 5)]
        public int Size { get; set; }

        public virtual Product Product { get; set; }

        public ProductImage()
        {
            this.Path = URis.NoImage;
        }
    }
}