using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("DeliveryMethod", Schema = "Transaction")]
    public class DeliveryMethod
    {
        public int DeliveryMethodId { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage ="Se require un nombre")]
        public string   Name { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Se require una descripción")]
        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }

    }
}