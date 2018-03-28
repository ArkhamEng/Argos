using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("Locality", Schema = "Config")]
    public class Locality
    {
        public int LocalityId { get; set; }

        public int CityId { get; set; }

        [MaxLength(5)]
        public string ZipCode { get; set; }

        [MaxLength(150)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        public virtual City City { get; set; }
    }
}