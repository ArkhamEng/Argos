using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Argos.Models.HumanResources
{
    [Table("JobPosition", Schema = "HumanResources")]
    public class JobPosition : AuditableEntity,ISelectable
    {
        [Column(Order = 0)]
        public int JobPositionId { get; set; }

        [Column(Order = 1)]
        [MaxLength(20, ErrorMessage ="El nombre del puesto no debe ser mayor a 20 caractéres")]
        public string Name { get; set; }

        [Column(Order = 2)]
        [MaxLength(50, ErrorMessage = "La descripción del puesto no debe ser mayor a 50 caractéres")]
        public string Description { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public string Value { get { return JobPositionId.ToString(); } }
      

        public string Text { get { return Name; } }

    }
}