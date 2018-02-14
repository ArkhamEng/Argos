using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Sales
{
    [Table("Commission", Schema = "Sales")]
    public class Commission:AuditableEntity
    {
        [Column(Order =0),ForeignKey("Sale")]
        public int CommissionId { get; set; }

        [Column(Order = 1)]
        [Display(Name="Porcentaje")]
        public int Percentage { get; set; }

        [Column(Order = 2)]
        [Display(Name = "Monto")]
        public double  Amount { get; set; }

        [Column(Order = 3)]
        public int Status { get; set; }

        #region Navigation Properties
        public virtual Sale Sale { get; set; }

        #endregion
    }
}