using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("ServiceType", Schema = "Config")]
    public class ServiceType:AuditableEntity,ISelectable
    {
        public int ServiceTypeId { get; set; }

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
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        [NotMapped]
        public int Id
        {
            get { return this.ServiceTypeId; }
        }

        #region Navigation Properties
        public ICollection<Service> Service { get; set; }

        #endregion

    }
}