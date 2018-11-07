
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Sales
{
    [Table("ShipMethod", Schema = "Sales")]
    public class ShipMethod
    {
        public int ShipMethodId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}