using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Operative;
using Argos.Models.Transaction;
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
        [MaxLength(3, ErrorMessage = "El nombre corto no debe exceder de 3 caractéres")]
        [Display(Name = "Abreviado")]
        public string ShortName { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "solo se permiten 15 caractérs para el teléfono")]
        [Index("Unq_Phone", IsUnique = true)]
        public string Phone { get; set; }

        /// <summary>
        /// una Sucursales puede tener diversas Ventas, compras y tener asignadas N cuentas,
        /// asi como tambien tener cada una su inventario
        /// </summary>
        #region Navigation Properties
        
        public ICollection<Sale> Sales { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        #endregion
    }
}