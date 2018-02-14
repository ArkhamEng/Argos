using Argos.Models.BaseTypes;
using Argos.Models.Sales;
using Argos.Models.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("Employee", Schema = "Catalog")]
    public class Employee:Person
    {
        [Column(Order = 0)]
        public int EmployeeId { get; set; }

        [Column(Order = 1)]
        [Display(Name ="Puesto")]
        public int JobPositionId { get; set; }

        [Display(Name="NSS")]
        [MaxLength(11,ErrorMessage ="El número de seguro social no debe exceder 11 digitos")]
        public string SSN { get; set; }

        [Display(Name ="% Comisión")]
        [Range(0,100,ErrorMessage ="El porcentaje de comisión debe ser un valor entre 0 y 100")]
        public int Commission { get; set; }

        #region Navigation Properties
        public virtual JobPosition JobPosition { get; set; }

        public ICollection<Sale> Sale { get; set; }

        public virtual ICollection<EmployeeUser> EmployeeUsers { get; set; }
        #endregion
    }

}