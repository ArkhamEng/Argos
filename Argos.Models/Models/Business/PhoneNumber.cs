using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("PhoneNumber", Schema = "Business")]
    public class PhoneNumber:ActivableAudit
    {
        [Column(Order = 0), Key]
        public int PhoneNumberId { get; set; }

        [Column(Order =1), ForeignKey("Entity")]
        public int EntityId { get; set; }

        [Display(Name = "Tipo")]
        [Column(Order = 2), ForeignKey("PhoneType")]
        public PhoneTypes PhoneTypeId { get; set; }

        [MaxLength(15)]
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Se requiere un número de teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato admitido es (xxxx) xxx xxx xx")]
        [Column(Order = 3)]
        public string Phone { get; set; }


        public virtual Entity Entity { get; set; }

        public virtual PhoneType PhoneType { get; set; }

    }
}