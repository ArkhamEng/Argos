using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BusinessEntity
{
    [Table("EmailAddress", Schema = "BusinessEntity")]
    public class EmailAddress:AuditableEntity
    {
        public int EmailAddressId { get; set; }

        public int PersonId { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El e-mail no tiene un formato correcto")]
        [MaxLength(150)]
        [Index("Unq_Email", IsUnique = true)]
        public string Email { get; set; }



        public virtual Person Person { get; set; }
    }
}