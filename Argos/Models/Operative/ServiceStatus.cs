using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("ServiceStatus", Schema = "Operative")]
    public class ServiceStatus:Status
    {
        [Column(Order = 0)]
        public int ServiceStatusId { get; set; }

        [NotMapped]
        public override int Id { get { return this.ServiceStatusId; } }

        public ICollection<Service> Services { get; set; }
    }
}