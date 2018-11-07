using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("Settlement", Schema = "Config")]
    public class Settlement
    {
        public int SettlementId { get; set; }

        [MaxLength(6)]
        public string TownId { get; set; }

        [MaxLength(6)]
        [Index("IDX_Code_Name",IsClustered =false,Order =0)]
        public string Code { get; set; }

        [Index("IDX_Code_Name", IsClustered = false, Order = 1)]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Zone { get; set; }

        public DateTime InsDate { get; set; }

        [MaxLength(30)]
        public string InsUser { get; set; }

        public virtual Town Town { get; set; }


    }
}