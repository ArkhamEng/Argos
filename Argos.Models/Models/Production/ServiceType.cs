using Argos.Common.Enums;
using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Production
{
    [Table("ServiceType", Schema = "Production")]
    public class ServiceType: ActivableAudit
    {
        public ServiceTypes ServiceTypeId { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }


        [MaxLength(100)]
        public string Descriptión { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
