using Argos.Models.BaseTypes;
using Argos.Models.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("SaleStatus", Schema = "Config")]
    public class SaleStatus:Status
    {
        [Column(Order = 0)]
        public int SaleStatusId { get; set; }

        public override int Id
        {
            get
            {
                return SaleStatusId;
            }
        }

        public virtual ICollection<Sale> Sales { get; set; }

    }
}