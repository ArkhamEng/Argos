using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Shipping", Schema = "Operative")]
    public class Shipping
    {
        [Column(Order = 0), ForeignKey("Sale")]
        public int ShippingId { get; set; }

        public int ShipMethodId { get; set; }

        [MaxLength(50)]
        public string DeliveryAgent { get; set; }

        [Display(Name="Fecha de envío")]
        public DateTime? ShipDate { get; set; }

        [Display(Name = "Fecha de recepción")]
        public DateTime? ReceptionDate { get; set; }

        [Display(Name = "Recibido por")]
        public string ReceivedBy { get; set; }

        [Display(Name = "Firma recibido")]
        public string ReceivedSignature { get; set; }

        #region Navigation Properties
        public virtual Sale Sale { get; set; }

        public virtual ShipMethod ShipMethod { get; set; }
        #endregion
    }
}