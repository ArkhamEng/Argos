using Argos.Models.Business;
using Argos.Models.HumanResources;
using Argos.Models.Inventory;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    /// <summary>
    /// Sucursales de la compañia
    /// </summary>
    [Table("Branch", Schema = "Config")]
    public class Branch : Entity
    {
        [Required(ErrorMessage = "Se requiere un nombre de sucursal")]
        [MaxLength(20, ErrorMessage = "El nombre de sucursal no debe exceder de 20 caractéres")]
        [Display(Name = "Nombre de Sucursal")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Se requiere un nombre abreviado de 3 caractéres")]
        [MaxLength(4, ErrorMessage = "El nombre corto no debe exceder de 3 caractéres")]
        [Display(Name = "Abreviado")]
        public string ShortName { get; set; }


        #region Navigation Properties

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Storage> Locations { get; set; }

        public ICollection<EmployeeBranch> EmployeeBranches { get; set; }


        #endregion
    }
}