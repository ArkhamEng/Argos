using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("EmailAddress", Schema = "Business")]
    public class EmailAddress:ActivableAudit
    {
        [Column(Order = 0), Key]
        public int EmailAddressId { get; set; }

        [Column(Order = 1),ForeignKey("Entity")]
        public int EntityId { get; set; }

        [Display(Name = "Tipo")]
        [Column(Order = 2), ForeignKey("EmailType")]
        public EmailTypes EmailTypeId { get; set; }

        [MaxLength(150)]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Se requiere un correo electrónico")]
        [EmailAddress(ErrorMessage = "El e-mail no tiene un formato correcto")]
        [Column(Order = 3)]
        public string Email { get; set; }

        public virtual Entity Entity { get; set; }

        public virtual EmailType EmailType { get; set; }
    }
}