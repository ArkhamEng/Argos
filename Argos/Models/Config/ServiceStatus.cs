using Argos.Models.BaseTypes;
using Argos.Models.Operative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("ServiceStatus", Schema = "Config")]
    public class ServiceStatus:Status
    {
        [Column(Order =0)]
        public int ServiceStatusId { get; set; }

        public override int Id
        {
            get
            {
                return ServiceStatusId;
            }
        }

        #region Navigation Properties
        public ICollection<Service> Services { get; set; }
        #endregion
    }
}