using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    /// <summary>
    /// Status de transacciones (compras y ventas)
    /// </summary>
    public enum StatusPurchase:int
    {
        /// <summary>
        /// Cancelada
        /// </summary>
        Canceled    = -1,

        /// <summary>
        /// transacción en espera de autorización
        /// </summary>
        OnRevision   = 1,

        /// <summary>
        /// transacción autorizada
        /// </summary>
        Autorized = 2,

        /// <summary>
        /// Transacción pagada completamente
        /// </summary>
        Compleated  = 3
    }
}