using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Operative;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("SupplierProduct", Schema = "Inventory")]
    public class SupplierProduct:AuditableEntity
    {
        [Column(Order = 0),Key,ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Code { get; set; }

        public string SatCode { get; set; }

        public double  Cost { get; set; }

        #region Navigation Properties
        public virtual Supplier Supplier { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }

    [Table("ExternalProductQueu", Schema = "Inventory")]
    public class ExternalProductQueu 
    {
        public int ExternalProductQueuId { get; set; }

        public int SupplierId { get; set; }

        public string Code { get; set; }

        public string SatCode { get; set; }

        public string Description { get; set; }

        public string TradeMark { get; set; }

        public string Unit { get; set; }

        public double BuyPrice { get; set; }

    }
}