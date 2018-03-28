using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{

    [Table("Purchase", Schema = "Transaction")]
    public class Purchase : TransactionBase
    {
        [Column(Order = 0)]
        public int PurchaseId { get; set; }

        [Column(Order = 1)]
        public int SupplierId { get; set; }

        [Column(Order = 2)]
        [MaxLength(10)]
        public string Bill { get; set; }

        [Column(Order = 3)]
        [Display(Name ="Fecha de Compra")]
        public override DateTime OrderDate { get; set; }
       
        #region Navigation Properties

        public virtual Supplier Supplier { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }



        #endregion

    }
}