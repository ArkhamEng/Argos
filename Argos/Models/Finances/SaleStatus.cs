using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Finances
{
    [Table("SaleStatus", Schema = "Finances")]
    public class SaleStatus:Sale
    {
        public int SaleStatusId { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

    }
}