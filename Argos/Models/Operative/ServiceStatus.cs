using Argos.Models.BaseTypes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("ServiceStatus", Schema = "Operative")]
    public class ServiceStatus:Status
    {
        public int ServiceStatusId { get; set; }

  
        #region Navigation Properties
        public ICollection<Service> Services { get; set; }
        #endregion
    }
}