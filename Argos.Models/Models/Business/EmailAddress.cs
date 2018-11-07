using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("EmailAddress", Schema = "Business")]
    public class EmailAddress:InsAudit
    {
        [Column(Order = 0),Key,ForeignKey("Entity")]
        [Index("Unq_Email", Order = 0, IsUnique = true)]
        public int EntityId { get; set; }

        [Display(Name = "Tipo")]
        [Column(Order = 1), Key]
        public EmailTypes EmailTypeId { get; set; }

        [MaxLength(150)]
        [Display(Name = "E-mail")]
        [Index("Unq_Email", Order = 1, IsUnique = true)]
        [Required(ErrorMessage = "Se requiere un correo electrónico")]
        [EmailAddress(ErrorMessage = "El e-mail no tiene un formato correcto")]
        public string Email { get; set; }

        public virtual Entity Entity { get; set; }

        public virtual EmailType EmailType { get; set; }
    }
}