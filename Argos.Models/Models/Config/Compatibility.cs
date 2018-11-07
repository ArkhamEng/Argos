using Argos.Models.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("Compatibility", Schema = "Config")]
    public class Compatibility
    {
        public int CompatibilityId { get; set; }

        public int ModelId { get; set; }

        public int ProductId { get; set; }

        public int Year { get; set; }

        #region Navigation Properties
        public virtual Model Model { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }

    
}