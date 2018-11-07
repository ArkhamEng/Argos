using Argos.Common.Enums;
using Argos.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("EmailType", Schema = "Business")]
    public class EmailType: ISelectable
    {
        public EmailTypes EmailTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public string Value { get { return ((int)this.EmailTypeId).ToString(); } }


        public string Text { get { return this.Name; } }
    }
}