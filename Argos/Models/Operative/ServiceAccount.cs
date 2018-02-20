using Argos.Models.Catalog;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    public class ServiceAccount:Service
    {
        [MaxLength(20)]
        public string Code { get; set; }

        [Display(Name="Cliente")]
        public int ClientId { get; set; }

        [Display(Name = "Servicio")]
        public int ServiceTypeId { get; set; }

        [Display(Name = "ServiceCategory")]
        public int ServiceCategoryId { get; set; }

        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }

        #region Navigation Properties
        public virtual Client Client { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ServiceCategory ServiceCategory { get; set; }

        public virtual AccountAddress AccountAddress { get; set; }
        #endregion
    }

}