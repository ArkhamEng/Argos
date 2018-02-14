using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("ServiceType", Schema = "Catalog")]
    public class ServiceType:AuditableEntity,ISelectable
    {
        [Column(Order = 0)]
        public int ServiceTypeId { get; set; }

        [Column(Order = 1)]
        [Required]
        [MaxLength(30)]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Column(Order = 2)]
        [Required(ErrorMessage ="Se requier un nombre corto")]
        [MaxLength(4,ErrorMessage ="El nombre corto debe ser de 3 caractéres")]
        [Display(Name = "Nombre corto")]
        public string ShortName { get; set; }

        [Column(Order = 3)]
        [MaxLength(50)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Column(Order = 4)]
        [Required]
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        public bool IsLocatable { get; set; }

        [NotMapped]
        public int Id
        {
            get { return this.ServiceTypeId; }
        }

        #region Navigation Properties
        public ICollection<ServiceAccount> ServiceAccount { get; set; }

        #endregion

    }
}