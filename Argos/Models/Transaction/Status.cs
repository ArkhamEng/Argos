using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("Status", Schema = "Transaction")]
    public class Status : ISelectable
    {
        public TransactionStatus StatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        #region Navigation Properties

        public ICollection<Purchase> Purchase { get; set; }

        public ICollection<Sale> Sales { get; set; }
        #endregion

        #region ISelectable Implementation
        public string Value
        {
            get { return ((int)StatusId).ToString(); }
        }

        public string Text
        {
            get { return this.Name; }
        }
        #endregion
    }
}