﻿using Argos.Models.BusinessEntity;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Client", Schema = "Operative")]
    public class Client:Person
    {
      
        [Display(Name = "Razón Social")]
        [MaxLength(200)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

        public ICollection<Account> Account { get; set; }

        public ICollection<Sale> Sale { get; set; }

    }
}