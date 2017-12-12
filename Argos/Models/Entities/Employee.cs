using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Entities
{
    [Table("Employee", Schema = "Catalog")]
    public class Employee:Person
    {
        public int EmployeeId { get; set; }

        [Display(Name="NSS")]
        public int SSN { get; set; }

    }
}