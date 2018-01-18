using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("Policy", Schema = "Config")]
    public class Policy:AuditableEntity,ISelectable
    {
        public int PolicyId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [Display(Name="Periodo")]
        public int PaymentPeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia")]
        public int PaymentTolerance { get; set; }


        public ICollection<Account> Account { get; set; }

        [NotMapped]
        public int Id
        {
            get { return PolicyId; }
        }
    }
}