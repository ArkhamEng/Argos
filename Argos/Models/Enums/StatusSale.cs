using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    /// <summary>
    /// Status de transacciones (compras y ventas)
    /// </summary>
    public enum StatusSale:int
    {
        /// <summary>
        /// Cancelada
        /// </summary>
        Canceled    = -2,

        /// <summary>
        /// PreCancelada
        /// </summary>
        PreCanceled = -1,

        /// <summary>
        /// transacción en espera de autorización
        /// </summary>
        Reserved   = 1,

        /// <summary>
        /// transacción autorizada
        /// </summary>
        Delivered = 2,

        /// <summary>
        /// Transacción pagada completamente
        /// </summary>
        PaidOut  = 3,

        /// <summary>
        /// Transacción pagada completamente
        /// </summary>
        Compleated = 4
    }
}