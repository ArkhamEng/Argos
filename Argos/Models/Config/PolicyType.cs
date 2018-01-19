using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("PolicyType", Schema = "Config")]
    public class PolicyType:AuditableEntity,ISelectable
    {
        public int PolicyTypeId { get; set; }

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

        [Required]
        [Display(Name = "Periodo mantto")]
        public int MaintenancePeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia mantto")]
        public int MaintenanceTolerance { get; set; }

        [Required]
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        public AccountType AccountType { get; set; }

        [NotMapped]
        public int Id
        {
            get { return PolicyTypeId; }
        }
    }
}