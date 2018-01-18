using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Commission", Schema = "Operative")]
    public class Commission
    {
        public int CommissionId { get; set; }

        [Display(Name="Porcentaje")]
        public int Percentage { get; set; }

        [Display(Name = "Monto")]
        public double  Amount { get; set; }

        public int Status { get; set; }

        [Display(Name ="Fecha de Pago")]
        public DateTime? PaymentDate { get; set; }

        public int AccountId { get; set; }

        #region Navigation Properties
        public virtual Account Account { get; set; }
        #endregion
    }
}