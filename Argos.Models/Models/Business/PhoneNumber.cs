using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("PhoneNumber", Schema = "Business")]
    public class PhoneNumber:InsAudit
    {
        [Column(Order =0),Key,ForeignKey("Entity")]
        [Index("Unq_Phone", Order = 0, IsUnique = true)]
        public int EntityId { get; set; }

        [Display(Name = "Tipo")]
        [Column(Order = 1), Key]
        public PhoneTypes PhoneTypeId { get; set; }

        [MaxLength(15)]
        [Display(Name = "Teléfono")]
        [Index("Unq_Phone", Order = 1, IsUnique = true)]
        [Required(ErrorMessage = "Se requiere un número de teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato admitido es (xxxx) xxx xxx xx")]
        public string Phone { get; set; }


        public virtual Entity Entity { get; set; }

        public virtual PhoneType PhoneType { get; set; }

    }
}