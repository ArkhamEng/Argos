﻿using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Argos.Models.Catalog
{
    [Table("JobPosition", Schema = "Catalog")]
    public class JobPosition : AuditableEntity,ISelectable
    {
        public int JobPositionId { get; set; }

        [Required(ErrorMessage ="Se requiere un nombre para el puesto")]
        [MaxLength(20, ErrorMessage ="El nombre del puesto no debe ser mayor a 20 caractéres")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "La descripción del puesto no debe ser mayor a 50 caractéres")]
        public string Description { get; set; }

        public ICollection<Employee> Employees { get; set; }

        [NotMapped]
        public int Id
        {
            get { return this.JobPositionId; }
        }
    }
}