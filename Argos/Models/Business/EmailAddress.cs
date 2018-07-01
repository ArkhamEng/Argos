using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Business
{
    [Table("EmailAddress", Schema = "Business")]
    public class EmailAddress:AuditableEntity
    {
        public int EmailAddressId { get; set; }

        [Index("Unq_Email", Order = 0, IsUnique = true)]
        public int EntityId { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El e-mail no tiene un formato correcto")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Se requiere un correo electrónico")]
        [Index("Unq_Email",Order =1, IsUnique = true)]
        public string Email { get; set; }

        public virtual Entity Entity { get; set; }
    }
}