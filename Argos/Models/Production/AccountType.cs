using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Production
{
    [Table("AccountType", Schema = "Production")]
    public class AccountType:ISelectable
    {
        public int AccountTypeId { get; set; }

        [MaxLength(4)]
        public string Code { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public bool LocationRequired { get; set; }

        public bool TrackingRequired { get; set; }

        public bool ContactsRequired { get; set; }

        public ICollection<Account> Accounts { get; set; }

        [NotMapped]
        public string Value { get { return this.AccountTypeId.ToString(); } }
       

        [NotMapped]
        public string Text { get { return Name; } }
       
    }
}