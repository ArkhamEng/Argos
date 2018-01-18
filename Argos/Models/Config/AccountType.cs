using Argos.Models.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Argos.Models.Operative;
using System.Collections.Generic;

namespace Argos.Models.Config
{
    [Table("AccountType", Schema = "Config")]
    public class AccountType:AuditableEntity,ISelectable
    {
        public int AccountTypeId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Se requier un nombre corto")]
        [MaxLength(3,ErrorMessage ="El nombre corto debe ser de 3 caractéres")]
        [Display(Name = "Nombre corto")]
        public string ShortName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Periodo mantto")]
        public int MaintenancePeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia mantto")]
        public int MaintenanceTolerance { get; set; }

        public ICollection<Account> Account { get; set; }

        public int Id
        {
           get { return this.AccountTypeId; }
        }
    }
}