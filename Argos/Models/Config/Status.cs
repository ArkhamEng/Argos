using Argos.Models.Operative;
using Argos.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("Status", Schema = "Config")]
    public  class Status
    {
        public int StatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public  string Description { get; }

        public string Type { get; set; }

        #region Navigation Properties

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<Service> Services { get; set; }
        #endregion
    }
}