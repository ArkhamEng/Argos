using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Account", Schema = "Operative")]
    public class Account : AuditableEntity
    {
        public int AcountId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Fecha de Cancelación")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Costo de póliza")]
        public double ContractCost { get; set; }

        [Display(Name = "Costo de póliza")]
        public double PolicyCost { get; set; }

       
        [Display(Name = "Calle y numero")]
        [MaxLength(80, ErrorMessage = "Solo se permiten 80 caractéres para calle y número")]
        public string Street { get; set; }

        [Display(Name = "Localidad/Colonia")]        
        [MaxLength(50, ErrorMessage = "Solo se permiten 80 caractéres para la localidad")]
        public string Location { get; set; }

        [Display(Name = "C.P.")]
        [MaxLength(10, ErrorMessage = "Solo se permiten 10 caractéres para el CP")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        

        public int AccountTypeId { get; set; }

        public int? PolicyId { get; set; }

        public int? CityId { get; set; }

        public int ClientId { get; set; }

        public int EmployeeId { get; set; }

        #region Navigation Properties
        public City City { get; set; }

        public virtual AccountType AccountType { get; set; }

        public virtual Policy Policy { get; set; }

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public ICollection<Commission> Commissions { get; set; }
        #endregion
    }
}