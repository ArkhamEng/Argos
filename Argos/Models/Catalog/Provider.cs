using Argos.Models.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    public class Provider:Person
    {
        public int ProviderId { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(130)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }
    }
}