using Argos.Models.HumanResources;
using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Transaction
{
    [Table("Sale", Schema = "Transaction")]
    public class Sale:Transaction
    {
        public int SaleId { get; set; }

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

        public ICollection<SaleDetail> SaleDetails { get; set; }

        
        #endregion
    }
}