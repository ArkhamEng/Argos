using Argos.Models.Enums;
using Argos.Models.Operative;
using Argos.Models.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finance
{
    /// <summary>
    /// Movimientos de caja registradora
    /// </summary>
    [Table("FinantialMovement", Schema = "Finance")]
    public class FinantialMovement
    {
        public int FinantialMovementId { get; set; }

        public int? CashSessionId { get; set; }

        public int? SaleId { get; set; }

        [ForeignKey("AppliedNote")]
        public int? CreditNoteId { get; set; }

        public PMethod PayMethodId { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public DateTime InsDate { get; set; }

        [MaxLength(30)]
        public string InsUser { get; set; }

        #region Navigation Properties
        public virtual CashSession CashSession { get; set; }

        public virtual PayMethod PayMethod { get; set; }

        public virtual Sale Sale { get; set; }

        /// <summary>
        /// Nota de crédito derivada de un movimiento de devolución
        /// </summary>
        public virtual CreditNote DerivedNote { get; set; }

        /// <summary>
        /// Nota de crédito que se aplico como pago
        /// </summary>
        public virtual CreditNote AppliedNote { get; set; }

        #endregion
    }

}