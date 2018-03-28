using Argos.Models.BaseTypes;
using Argos.Models.Transaction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("Supplier", Schema = "Catalog")]
    public class Supplier:Person
    {
        public int SupplierId { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(200)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(200)]
        public string WebSite { get; set; }

        public ICollection<Purchase> Purchase { get; set; }
    }
}