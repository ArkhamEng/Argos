using Argos.Models.BaseTypes;
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
    /// Nota de credito generada por devolución 
    /// </summary>
    [Table("CreditNote", Schema = "Finance")]
    public class CreditNote:AuditableEntity
    {
        /// <summary>
        /// Llave foranea primaria hacia el movimiento origen
        /// </summary>
        [Key,ForeignKey("Transaction")]
        public int CreditNoteId { get; set; }

        /// <summary>
        /// Folio de la nota de crédito
        /// </summary>
        [MaxLength(10)]
        [Index("IDX_Code_Indent",1,IsUnique =false)]
        public string Code { get; set; }

        /// <summary>
        /// Identificación usada por el cliente para crear la nota
        /// </summary>
        [Index("IDX_Code_Indent",2, IsUnique = false)]
        [MaxLength(15)]
        public string Ident { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        /// <summary>
        /// Fecha de expiración de la nota de crédito
        /// </summary>
        public DateTime DueDate { get; set; }

        [Index("IDX_Sequential", 1, IsUnique =false)]
        public int Year { get; set; }

        [Index("IDX_Sequential", 2, IsUnique = false)]
        public int Sequential { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}