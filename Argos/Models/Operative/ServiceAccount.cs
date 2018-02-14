using Argos.Models.Catalog;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    public class ServiceAccount:Service
    {
        [Display(Name="Cliente")]
        public int ClientId { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [Display(Name = "Servicio")]
        public int ServiceTypeId { get; set; } 

        #region Navigation Properties
        public virtual Client Client { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        public virtual AccountAddress AccountAddress { get; set; }
        #endregion
    }

}