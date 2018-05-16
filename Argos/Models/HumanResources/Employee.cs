using Argos.Models.BaseTypes;
using Argos.Models.Finance;
using Argos.Models.Security;
using Argos.Models.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Models.Operative;

namespace Argos.Models.HumanResources
{
    [Table("Employee", Schema = "HumanResources")]
    public class Employee:Person
    {
        [Column(Order = 0)]
        public int EmployeeId { get; set; }

        [Column(Order = 1)]
        [Display(Name ="Puesto")]
        public int JobPositionId { get; set; }

        [Display(Name ="Sexo")]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Display(Name ="Cumpleaños")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name="NSS")]
        [MaxLength(11,ErrorMessage ="El número de seguro social no debe exceder 11 digitos")]
        public string SSN { get; set; }

        [Display(Name ="% Comisión")]
        public int Commission { get; set; }

        #region Navigation Properties
        public virtual JobPosition JobPosition { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<EmployeeUser> EmployeeUsers { get; set; }

        public ICollection<Commission> Commissions { get; set; }

        public ICollection<EmployeeBranch> EmployeeBranches { get; set; }

        public ICollection<CashSession> CashSessions { get; set; }

        #endregion
    }

  

}