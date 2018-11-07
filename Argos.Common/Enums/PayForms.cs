using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Common.Enums
{
    /// <summary>
    /// Formas de pago
    /// </summary>
    public enum PayForms:int
    {
        /// <summary>
        /// Contado (una sola exhibición)
        /// </summary>
        Cash   = 1,

        /// <summary>
        /// Crédito (Parcialidades)
        /// </summary>
        Credit = 2,
    }
}