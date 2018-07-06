using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("CreditStatus", Schema = "Config")]
    public class CreditStatus:ISelectable
    {
        public StatusCredit CreditStatusId { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }

        public ICollection<Purchasing.Supplier> Suppliers { get; set; }

        public ICollection<Sales.Client> Clients { get; set; }

        public string Value { get { return ((int)this.CreditStatusId).ToString(); } }
      

        public string Text { get { return this.Name; } }
     
    }
}