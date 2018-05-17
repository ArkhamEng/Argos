using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finance
{
    [Table("PaymentMethod", Schema = "Finance")]
    public class PayMethod:ISelectable
    {
        public PayMethodes PayMethodId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }


        public ICollection<CashMovement> CashMovements { get; set; }

        #region Not Mapped Properties

        [NotMapped]
        public string Value
        {
            get
            {
                return ((int)this.PayMethodId).ToString();
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