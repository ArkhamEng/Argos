using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.HumanResources
{
    /// <summary>
    /// Sucursales de empleados
    /// </summary>
    [Table("EmployeeBranch", Schema = "HumanResources")]
    public class EmployeeBranch
    {
        [Column(Order = 0),Key]
        public int BranchId { get; set; }

        [Column(Order = 1), Key]
        public int EmployeeId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime InsDate { get; set; }

        [MaxLength(50)]
        public string   InsUser { get; set; }

        #region Navigation Properties
        public virtual Branch Branch { get; set; }

        public virtual Employee Employee { get; set; }

        #endregion
    }
}