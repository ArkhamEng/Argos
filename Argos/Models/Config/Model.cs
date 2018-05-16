using Argos.Models.BaseTypes;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    /// <summary>
    /// Modelos de autos, aplica para la configuración de autopartes
    /// </summary>
    [Table("Model", Schema = "Config")]
    public class Model:AuditableEntity
    {
        public int ModelId { get; set; }

        public int MakerId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual Maker Maker { get; set; }

        public ICollection<Compatibility> Compatibilities { get; set; }


    }
}