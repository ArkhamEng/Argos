using Argos.Models.Finance;
using Argos.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Models.Operative;
using Argos.Models.BusinessEntity;
using Argos.Support;

namespace Argos.Models.HumanResources
{
    [Table("Employee", Schema = "HumanResources")]
    public class Employee:Person
    {
        [Display(Name ="Puesto")]
        public int JobPositionId { get; set; }

        [Display(Name ="Sexo")]
        [MaxLength(1)]
        public string Gender { get; set; }

        [Display(Name = "Estado civil")]
        [MaxLength(1)]
        public string MaritalStatus { get; set; }

        [Display(Name = "Contratado en")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name ="Cumpleaños")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [Display(Name="NSS")]
        [MaxLength(11,ErrorMessage ="El número de seguro social no debe exceder 11 digitos")]
        public string SSN { get; set; }

        [Display(Name ="% Comisión")]
        public int Commission { get; set; }

        public string GenderClass
        {
            get { return this.Gender == Cons.MaleChar ? Cons.MaleClass : Cons.FemaleClass; }
        }

        public string GenderName
        {
            get { return this.Gender == Cons.MaleChar ? Cons.Male : Cons.Female; }
        }

        #region Navigation Properties
        public virtual JobPosition JobPosition { get; set; }

        public ICollection<Commission> Commissions { get; set; }

        public ICollection<EmployeeBranch> EmployeeBranches { get; set; }

        public ICollection<CashSession> CashSessions { get; set; }

        public ICollection<Sale> Sales { get; set; }


        #endregion
    }



}