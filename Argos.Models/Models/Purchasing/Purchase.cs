using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Purchasing
{
    [Table("Purchase", Schema = "Purchasing")]
    public class Purchase : Transaction
    {
        public int PurchaseId { get; set; }

        [Display(Name ="Proveedor")]
        [ForeignKey("Supplier")]
        [Index("IDX_Supplier_Bill", 0, IsUnique = true)]
        public int SupplierId { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("PurchaseStatus")]
        public StatusPurchase Status { get; set; }

        [Display(Name = "Factura")]
        [Index("IDX_Supplier_Bill", 1, IsUnique = true)]
        [MaxLength(10)]
        [Required]
        public string Bill { get; set; }

   
        #region Navigation Properties

        public Supplier Supplier { get; set; }

        public PurchaseStatus PurchaseStatus { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
     
        #endregion

    }
}