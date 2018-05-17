using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finance
{
    /// <summary>
    /// Historial de aperturas de caja
    /// </summary>
    [Table("CashSession", Schema = "Finance")]
    public class CashSession
    {
        public int CashSessionId { get; set; }

        public int CashRegisterId { get; set; }

        public int PersonId { get; set; }

        /// <summary>
        /// Monto depositado en la apertura
        /// </summary>
        [DataType(DataType.Currency)]
        public double OpeningAmount { get; set; }

        /// <summary>
        /// Fecha y hora de apertura de caja
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime OpeningDate { get; set; }

        /// <summary>
        /// Acumulado al cierre de caja
        /// </summary>
        [DataType(DataType.Currency)]
        public double ClosingAmount { get; set; }

        /// <summary>
        /// Fecha y hora de cierre de caja
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ClosingDate { get; set; }

        public string ClosingUser { get; set; }

        #region Navigation Properties
        public virtual CashRegister CashRegister { get; set; }

        public virtual Employee Employee { get; set; }

        public ICollection<CashMovement> CashMovements { get; set; }
        #endregion


        public CashSession()
        {
            this.OpeningDate = DateTime.Now.ToLocal();
            this.PersonId    = HttpContext.Current.User.Identity.ToEmployee().PersonId;
        }

    }
}