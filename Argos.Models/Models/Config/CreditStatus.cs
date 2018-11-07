using Argos.Common.Enums;
using Argos.Models.Interfaces;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("CreditStatus", Schema = "Config")]
    public class CreditStatus:ISelectable
    {
        public StatusCredit CreditStatusId { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }

        public ICollection<Supplier> Suppliers { get; set; }

        public ICollection<Client> Clients { get; set; }

        public string Value { get { return ((int)this.CreditStatusId).ToString(); } }
      

        public string Text { get { return this.Name; } }
     
    }
}