using Argos.Models.BaseTypes;
using Argos.Models.Finance;
using Argos.Models.HumanResources;
using Argos.Models.Inventory;
using Argos.Models.Operative;
using Argos.Models.Production;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    /// <summary>
    /// Sucursales de la compañia
    /// </summary>
    [Table("Branch", Schema = "Config")]
    public class Branch : LocatableEntity
    {
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Se requiere un nombre de sucursal")]
        [MaxLength(20, ErrorMessage = "El nombre de sucursal no debe exceder de 20 caractéres")]
        [Display(Name = "Nombre de Sucursal")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Se requiere un nombre abreviado de 3 caractéres")]
        [MaxLength(4, ErrorMessage = "El nombre corto no debe exceder de 3 caractéres")]
        [Display(Name = "Abreviado")]
        public string ShortName { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "solo se permiten 15 caractérs para el teléfono")]
        [Index("Unq_Phone", IsUnique = true)]
        public string Phone { get; set; }


        #region Navigation Properties

        public ICollection<Operation> Operation { get; set; }

        public ICollection<Location> Locations { get; set; }

        public ICollection<CashRegister> CashRegisters { get; set; }

        public ICollection<EmployeeBranch> EmployeeBranches { get; set; }


        #endregion
    }
}