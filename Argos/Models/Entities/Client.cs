using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Entities
{
    [Table("Client", Schema = "Catalog")]
    public class Client:Person
    {
        public int ClientId { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(130)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

    }
}