using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Support;
using Argos.Models.Enums;
using Argos.Models.BaseTypes;
using Argos.Models.Finance;
using Argos.Models.BusinessEntity;

namespace Argos.Models.Operative
{
    /// <summary>
    /// Clase base para operaciones de Transferencia, Compra y venta
    /// </summary>
    [Table("Operation", Schema = "Operative")]
    public abstract class Operation:AuditableEntity
    {
        public int OperationId { get; set; }

        public int BranchId { get; set; }

        public DateTime OperationDate { get; set; }

        public DateTime? LockEndDate { get; set; }

        [MaxLength(30)]
        public string LockUser { get; set; }

        public virtual bool IsLocked
        {
            get
            {
                if (LockEndDate != null)
                {
                    if (LockEndDate.Value >= DateTime.Now.ToLocal() && LockUser != HttpContext.Current.User.Identity.Name)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public ICollection<OperationDetail> OperationDetails { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        #endregion
    }

}