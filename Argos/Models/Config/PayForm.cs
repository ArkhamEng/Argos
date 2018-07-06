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
    [Table("PayForm", Schema = "Config")]
    public class PayForm:ISelectable
    {
        public PayForms PayFormId { get; set; }

        [MaxLength(8)]
        public string Name { get; set; }

        public ICollection<Purchasing.Purchase> Purchase { get; set; }

        public ICollection<Sales.Sale> Sales { get; set; }

        public string Value { get { return ((int)this.PayFormId).ToString(); } }


        public string Text { get { return this.Name; } }
    }
}