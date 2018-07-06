using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Business
{
    [Table("Address", Schema = "Business")]
    public class Address:InsAudit
    {
        [Column(Order =0),Key, ForeignKey("Entity")]
        public int EntityId { get; set; }

        [Column(Order = 1), Key,ForeignKey("AddressType")]
        [Display(Name ="Tipo")]
        public AddressTypes AddressTypeId { get; set; }

        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "Se requiere el municipo o ciudad")]
        [MaxLength(6)]
        public string TownId { get; set; }

        [Display(Name = "Calle y número")]
        [Required(ErrorMessage = "Se requiere la calle y número")]
        [MaxLength(80, ErrorMessage = "Solo se permiten 80 caractéres para la calle")]
        public string Street { get; set; }

        [Display(Name = "Localidad/Colonia")]
        [Required(ErrorMessage = "Se requiere la localidad o colonia")]
        [MaxLength(50, ErrorMessage = "Solo se permiten 80 caractéres para la localidad")]
        public string Location { get; set; }

        [Display(Name = "C.P.")]
        [MaxLength(10, ErrorMessage = "Solo se permiten 10 caractéres para el CP")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        #region Navigation Properties
        public Town Town { get; set; }
    
        public virtual Entity Entity { get; set; }

        public virtual AddressType AddressType { get; set; }
        #endregion

    }
}