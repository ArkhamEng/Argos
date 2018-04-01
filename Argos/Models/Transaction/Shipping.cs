using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("Shipping", Schema = "Transaction")]
    public class Shipping
    {
        public int ShippingId { get; set; }

        public int SaleId { get; set; }

        public int ShipMethodId { get; set; }

        [Display(Name="Fecha de envío")]
        public DateTime? ShipDate { get; set; }

        [Display(Name = "Fecha de recepción")]
        public DateTime? ReceptionDate { get; set; }

        #region Navigation Properties
        public virtual Sale Sale { get; set; }

        public ShipMethod ShipMethod { get; set; }
        #endregion
    }
}