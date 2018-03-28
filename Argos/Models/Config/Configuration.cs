using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("Configuration", Schema = "Config")]
    public class Configuration
    {
        public int ConfigurationId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Type { get; set; }

        public object Value { get; set; }
    }
}