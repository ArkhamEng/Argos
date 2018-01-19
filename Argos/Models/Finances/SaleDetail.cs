using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finances
{
    [Table("SaleDetail", Schema = "Finances")]
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }

        public int? AccountId { get; set; }

        public int? ExtensionId { get; set; }

        public int? PolicyId { get; set; }


        public double Quantity { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Monto Parcial")]
        [DataType(DataType.Currency)]
        public double PartialAmount { get; set; }

        #region Navigation Properties

        public virtual Sale Sale { get; set; }

        public virtual Account Account { get; set; }

        public virtual Extension Extension { get; set; }

        public virtual Policy Policy { get; set; }
        #endregion
    }
}