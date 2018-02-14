using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
   
    public class ChildService : Service
    {
        [ForeignKey("ParentService")]
        public int ParentServiceId { get; set; }

        public virtual Service ParentService { get; set; }
    }


    public class Policy: ChildService
    {
        [Required]
        [Display(Name = "Periodo pago")]
        public int PaymentPeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia pago")]
        public int PaymentTolerance { get; set; }

        [Required]
        [Display(Name = "Periodo mantto")]
        public int MaintenancePeriod { get; set; }

        [Required]
        [Display(Name = "Tolerancia mantto")]
        public int MaintenanceTolerance { get; set; }

    }

    public class Extension:ChildService
    {

    }
}