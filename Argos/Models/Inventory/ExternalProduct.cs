using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Inventory
{
    [Table("ExternalProduct", Schema = "Inventory")]
    public class ExternalProduct:AuditableEntity
    {
        public int ExternalProductId { get; set; }

        public int SupplierId { get; set; }

        public string Code { get; set; }

        public string SatCode { get; set; }

        public string Description { get; set; }

        public string TradeMark { get; set; }

        public string Unit { get; set; }

        public double  BuyPrice { get; set; }

        #region Navigation Properties
        public virtual Supplier Supplier { get; set; }

        public ICollection<Equivalence> Equivalences { get; set; }
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