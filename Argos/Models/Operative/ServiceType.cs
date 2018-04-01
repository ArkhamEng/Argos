using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("ServiceType", Schema = "Operative")]
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }


        public bool AllowAttachment { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}