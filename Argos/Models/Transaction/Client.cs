using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("Client", Schema = "Transaction")]
    public class Client:Person
    {
        [Column(Order = 0)]
        public int ClientId { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(200)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

        public ICollection<Account> Account { get; set; }

        public ICollection<Sale> Sale { get; set; }

    }
}