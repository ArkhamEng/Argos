using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Purchasing
{
    [Table("Status", Schema = "Purchasing")]
    public class PurchaseStatus : ISelectable
    {
        public StatusPurchase PurchaseStatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        #region Navigation Properties

        public ICollection<Purchase> Purchase { get; set; }
        #endregion

        #region ISelectable Implementation
        public string Value
        {
            get { return ((int)PurchaseStatusId).ToString(); }
        }

        public string Text
        {
            get { return this.Name; }
        }
        #endregion
    }
}