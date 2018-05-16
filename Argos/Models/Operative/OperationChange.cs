using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("OperationChange", Schema = "Operative")]
    public abstract class OperationChange
    {
        public int OperationChangeId { get; set; }
     
        [ForeignKey("Status")]
        public OpStatus StatusId { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(50)]
        public string User { get; set; }

        #region Navigation Properties
        public virtual OperationStatus Status { get; set; }

        #endregion
    }

    public class SaleHistory: OperationChange
    {
        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }
    }

    public class PurchaseHistory : OperationChange
    {
        public int PurchaseId { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}