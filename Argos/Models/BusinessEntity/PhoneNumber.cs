using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BusinessEntity
{
    [Table("PhoneNumber", Schema = "BusinessEntity")]
    public class PhoneNumber:AuditableEntity
    {
        public int PhoneNumberId { get; set; }

        public PhoneTypes PhoneTypeId { get; set; }

        public int PersonId { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato admitido es (229) 123 45 67")]
        [MaxLength(15)]
        [Index("Unq_Phone", IsUnique = true)]
        public string Phone { get; set; }

        public virtual Person Person { get; set; }

        public virtual PhoneType PhoneType { get; set; }

    }
}