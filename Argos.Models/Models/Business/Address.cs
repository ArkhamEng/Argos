using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("Address", Schema = "Business")]
    public class Address:ActivableAudit
    {
        [Column(Order = 0),Key]
        public int AddressId { get; set; }

        [Column(Order =1),ForeignKey("Entity")]
        public int EntityId { get; set; }

        [Column(Order = 2), ForeignKey("AddressType")]
        [Display(Name ="Tipo")]
        public AddressTypes AddressTypeId { get; set; }

        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "Se requiere el municipo o ciudad")]
        [MaxLength(6)]
        [Column(Order = 3)]
        public string TownId { get; set; }

        [Display(Name = "Calle y número")]
        [Required(ErrorMessage = "Se requiere la calle y número")]
        [MaxLength(80, ErrorMessage = "Solo se permiten 80 caractéres para la calle")]
        [Column(Order = 4)]
        public string Street { get; set; }

        [Display(Name = "Localidad/Colonia")]
        [Required(ErrorMessage = "Se requiere la localidad o colonia")]
        [MaxLength(50, ErrorMessage = "Solo se permiten 80 caractéres para la localidad")]
        [Column(Order = 5)]
        public string Location { get; set; }

        [Display(Name = "C.P.")]
        [MaxLength(10, ErrorMessage = "Solo se permiten 10 caractéres para el CP")]
        [DataType(DataType.PostalCode)]
        [Column(Order = 6)]
        public string ZipCode { get; set; }

        #region Navigation Properties
    
        public virtual Entity Entity { get; set; }

        public virtual AddressType AddressType { get; set; }

        public virtual Town Town { get; set; }
        #endregion

        public override string ToString()
        {
            return  "<a class='text-capitalized'>"+this.Street + "<br/>" + this.Location + "<br/>" + this.Town.State.Name + ", " 
                    + this.Town.Name + "<br/> CP. " + this.ZipCode+"</a>";
        }

    }
}