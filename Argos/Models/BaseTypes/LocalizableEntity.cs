using Argos.Models.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.BaseTypes
{
    public abstract class LocatableEntity:AuditableEntity
    {
        [Display(Name = "Municipio")]
        [Required(ErrorMessage ="Se requiere el municipo o ciudad")]
        public  int CityId { get; set; }

        [Display(Name = "Calle y numero")]
        [Required(ErrorMessage = "Se requiere la calle y número")]
        [MaxLength(80, ErrorMessage = "Solo se permiten 80 caractéres para calle y número")]
        public  string Street { get; set; }

        [Display(Name = "Localidad/Colonia")]
        [Required(ErrorMessage = "Se requiere la localidad o colonia")]
        [MaxLength(50, ErrorMessage = "Solo se permiten 80 caractéres para la localidad")]
        public  string Location { get; set; }

        [Display(Name = "C.P.")]
        [MaxLength(10,ErrorMessage ="Solo se permiten 10 caractéres para el CP")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        #region Navigation Properties
        public  City City { get; set; }
        #endregion
    }
}