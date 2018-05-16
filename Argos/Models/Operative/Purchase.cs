using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Purchase", Schema = "Operative")]
    public class Purchase : Operation
    {
        [Index("IDX_Supplier_Bill",1,IsUnique =true)]
        public int SupplierId { get; set; }

        [Index("IDX_Supplier_Bill", 2, IsUnique = true)]
        [MaxLength(10)]
        [Required]
        public string Bill { get; set; }

   
       
        #region Navigation Properties

        public virtual Supplier Supplier { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }


        public ICollection<PurchaseHistory> PurchaseHistories { get; set; }

        #endregion

    }
}