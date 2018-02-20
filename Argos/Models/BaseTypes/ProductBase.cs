using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    [Table("Product", Schema = "Catalog")]
    public abstract class ProductBase:AuditableEntity
    {
        [Key]
        public int ProductId { get; set; }        

        [MaxLength(30)]
        [Column(Order = 1)]
        [Required(ErrorMessage ="Debe ingresar el código de producto")]
        public virtual string Code { get; set; }

        public bool IsActive { get; set; }
    }
}