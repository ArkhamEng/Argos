using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("TransType", Schema = "Transaction")]
    public class TransType:ISelectable
    {
        public int TransTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool ForSale { get; set; }

        public bool ForPurchase { get; set; }

        #region Navigation Properties
        public ICollection<Sale> Sales { get; set; }

        public ICollection<Purchase> Purchase { get; set; }

        #endregion

        #region Not Mapped Properties

        [NotMapped]
        public string Value
        {
            get
            {
                return this.TransTypeId.ToString();
            }
        }

        [NotMapped]
        public string Text
        {
            get
            {
                return this.Name;
            }
        }
        #endregion
    }
}