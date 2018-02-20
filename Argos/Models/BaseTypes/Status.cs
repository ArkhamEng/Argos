using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class Status:ISelectable
    {
        [MaxLength(20)]
        public string Code { get; set; }

        [NotMapped]
        public abstract int Id { get; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}