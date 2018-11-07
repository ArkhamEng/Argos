using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Business
{
    [Table("Entity", Schema = "Business")]
    public abstract class Entity:AuditableCatalog
    {
        public int EntityId { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }


        public Entity()
        {
            this.PhoneNumbers = new List<PhoneNumber>();
            this.EmailAddresses = new List<EmailAddress>();
            this.Addresses = new List<Address>();
        }

    }
}