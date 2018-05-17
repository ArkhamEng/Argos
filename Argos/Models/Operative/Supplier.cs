using Argos.Models.BaseTypes;
using Argos.Models.BusinessEntity;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Supplier", Schema = "Operative")]
    public class Supplier:Person
    {
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