using Argos.Models.Catalog;
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

        public int? ServiceId { get; set; }

        public int? ProductId { get; set; }


        public double Quantity { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Monto Parcial")]
        [DataType(DataType.Currency)]
        public double PartialAmount { get; set; }

        #region Navigation Properties

        public virtual Sale Sale { get; set; }

        public virtual Service Service { get; set; }

        public virtual Product Product { get; set; }


        #endregion
    }
}