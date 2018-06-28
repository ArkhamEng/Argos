using Argos.Models.Operative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Inventory
{
    [Table("Movement", Schema = "Inventory")]
    public class Movement
    {
        public int MovementId { get; set; }

        public int ItemId { get; set; }

        public int DetailId { get; set; }

        public double Quantity { get; set; }

        #region Navigation Property
        public virtual Item Item { get; set; }

        public virtual Detail Detail { get; set; }
        #endregion

    }
}