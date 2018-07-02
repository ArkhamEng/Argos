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
        [Column(Order = 0),Key,ForeignKey("Entity")]
        public int EntityId { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El e-mail no tiene un formato correcto")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Se requiere un correo electrónico")]
        [Column(Order = 1), Key]
        public string Email { get; set; }

        public virtual Entity Entity { get; set; }
    }
}