using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using Argos.Models.HumanResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Sales
{
    [Table("Sale", Schema = "Sales")]
    public class Sale: Transaction
    {
        public int SaleId { get; set; }

        [ForeignKey("SaleStatus")]
        public StatusSale Status { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [MaxLength(12)]
        public string Folio { get; set; }

        public bool IsOnline { get; set; }

        #region Navigation Properties
        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SaleStatus SaleStatus { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

        #endregion
    }
}