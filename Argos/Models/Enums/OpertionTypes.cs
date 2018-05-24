using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    /// <summary>
    /// tipos de transacciones (compras y ventas)
    /// </summary>
    public enum OpertionTypes : int
    {
        /// <summary>
        /// Cotización, este tipo no descuenta el producto del inventario
        /// </summary>
        Budget  = 1, 
        /// <summary>
        /// Venta de contado
        /// </summary>
        InCash  = 2, 
        /// <summary>
        /// Venta a crédito
        /// </summary>
        Credit  = 3, 
        /// <summary>
        /// Preventa o apartado
        /// </summary>
        Reservation = 4 
    }
}