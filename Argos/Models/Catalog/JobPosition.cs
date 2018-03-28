using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Argos.Models.Catalog
{
    [Table("JobPosition", Schema = "Catalog")]
    public class JobPosition : AuditableEntity
    {
        [Column(Order =0),Key]
        [MaxLength(20, ErrorMessage ="El nombre del puesto no debe ser mayor a 20 caractéres")]
        public string JobPositionName { get; set; }

        [Column(Order = 1)]
        [MaxLength(50, ErrorMessage = "La descripción del puesto no debe ser mayor a 50 caractéres")]
        public string Description { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}