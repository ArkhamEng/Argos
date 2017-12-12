using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Entities
{
    [Table("ContractType", Schema = "Config")]
    public class ContractType
    {
        public int ContractTypeId { get; set; }

        [Required]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public DateTime UpdDate { get; set; }

        public string UpdUser { get; set; }
    }
}