using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Common.Enums
{
    /// <summary>
    /// Status de transacciones (compras y ventas)
    /// </summary>
    public enum StatusAccount:int
    {
        /// <summary>
        /// Cuenta cancelada
        /// </summary>
        Canceled    = -2,

        /// <summary>
        /// Cuenta suspendida
        /// </summary>
        Suspended = -1,

        /// <summary>
        /// Cuenta activa
        /// </summary>
        Active   = 1,

        /// <summary>
        /// Cuenta cerrada
        /// </summary>
        Finished = 2,

    }
}