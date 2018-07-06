using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    /// <summary>
    /// Metodos de pago
    /// </summary>
    public enum PayMethods:int
    {
        /// <summary>
        /// Efectivo
        /// </summary>
        Cash         = 1,
        /// <summary>
        /// Tarjeta de crédito
        /// </summary>
        Card         = 2,
        /// <summary>
        /// Cheque
        /// </summary>
        Check        = 3,
        /// <summary>
        /// Trasferencia electrónica
        /// </summary>
        Transference = 4,
        /// <summary>
        /// Nota de crédito
        /// </summary>
        CreditNote   = 5,
        /// <summary>
        /// Cupon promocional
        /// </summary>
        Coupon       = 6
    }

    
}