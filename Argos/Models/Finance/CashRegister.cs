using Argos.Models.BaseTypes;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finance
{
    /// <summary>
    /// Caja registradora, 
    /// </summary>
    [Table("CashRegister", Schema = "Finance")]
    public class CashRegister:ISelectable
    {
        public int CashRegisterId { get; set; }

        public int BranchId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsOpen { get; set; }


        #region Not Mapped Properties
        
        public string Value { get { return CashRegisterId.ToString(); } }

        public string Text { get { return Name; } }
        #endregion

        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public ICollection<CashSession> CashSessions { get; set; }

        #endregion
    }
}