using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using Argos.Models.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("Client", Schema = "Catalog")]
    public class Client:Person
    {
        [Column(Order = 0)]
        public int ClientId { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(130)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

        public ICollection<Service> Account { get; set; }

        public ICollection<Sale> Sale { get; set; }

    }
}