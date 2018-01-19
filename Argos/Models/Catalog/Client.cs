using Argos.Models.BaseTypes;
using Argos.Models.Finances;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("Client", Schema = "Catalog")]
    public class Client:Person
    {
        public int ClientId { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(130)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

        public ICollection<Account> Account { get; set; }

        public ICollection<Sale> Sale { get; set; }

    }
}