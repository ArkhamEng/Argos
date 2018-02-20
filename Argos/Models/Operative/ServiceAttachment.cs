using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("ServiceAttachment", Schema = "Operative")]
    public class ServiceAttachment
    {
        public int ServiceAttachmentId { get; set; }

        public int ServiceId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }

        [Required]
        public string PathFile { get; set; }



        #region Navigation Properties

        public virtual Service Service { get; set; }
        #endregion
    }
}