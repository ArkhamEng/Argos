using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("Sale", Schema = "Transaction")]
    public class Sale:TransactionBase
    {
        [Column(Order = 0)]
        public int SaleId { get; set; }

        [Column(Order = 1)]
        public int ClientId { get; set; }

        [Column(Order = 3)]
        [MaxLength(10)]
        public string Folio { get; set; }


        #region Navigation Properties

        public virtual Client Client { get; set; }

        public virtual Commission Commission { get; set; }

        public  ICollection<SaleDetail> SaleDetails { get; set; }

        #endregion

    }
}