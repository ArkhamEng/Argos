using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Catalog
{
    [Table("Employee", Schema = "Catalog")]
    public class Employee:Person
    {
        public int EmployeeId { get; set; }

        [Display(Name ="Puesto")]
        public int JobPositionId { get; set; }

        [Display(Name="NSS")]
        public int SSN { get; set; }

        [Display(Name ="% Comisión")]
        [Range(0,100,ErrorMessage ="El porcentaje de comisión debe ser un valor entre 0 y 100")]
        public int Commission { get; set; }

        #region Navigation Properties
        public virtual JobPosition JobPosition { get; set; }

        public ICollection<Account> Account { get; set; }

        #endregion
    }

}