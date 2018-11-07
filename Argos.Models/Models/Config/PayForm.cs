using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using Argos.Models.Interfaces;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("PayForm", Schema = "Config")]
    public class PayForm:ISelectable
    {
        public PayForms PayFormId { get; set; }

        [MaxLength(8)]
        public string Name { get; set; }

        public ICollection<Purchase> Purchase { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public string Value { get { return ((int)this.PayFormId).ToString(); } }


        public string Text { get { return this.Name; } }
    }
}