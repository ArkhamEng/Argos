using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Business
{
    [Table("PhoneNumber", Schema = "Business")]
    public class PhoneNumber:AuditableEntity
    {
        /*public int PhoneNumberId { get; set; }

        public PhoneTypes PhoneTypeId { get; set; }

        [Index("Unq_Phone", Order = 0, IsUnique = true)]
        public int EntityId { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato admitido es (xxxx) xxx xxx xx")]
        [MaxLength(15)]
        [Required(ErrorMessage ="Se requiere un numero de teléfono")]
        [Index("Unq_Phone",Order = 1, IsUnique = true)]
        public string Phone { get; set; }*/

        [Column(Order =0),Key,ForeignKey("Entity")]
        public int EntityId { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato admitido es (xxxx) xxx xxx xx")]
        [MaxLength(15)]
        [Required(ErrorMessage = "Se requiere un numero de teléfono")]
        [Column(Order = 1), Key]
        public string Phone { get; set; }

        public PhoneTypes PhoneTypeId { get; set; }


        public virtual Entity Entity { get; set; }

        public virtual PhoneType PhoneType { get; set; }

    }
}