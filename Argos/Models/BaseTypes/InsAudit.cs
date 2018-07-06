using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class InsAudit
    {
        [Required]
        public DateTime InsDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string InsUser { get; set; }

  
        public InsAudit()
        {
            this.InsDate = DateTime.Now.ToLocal();
            this.InsUser = HttpContext.Current.User.Identity.Name;
        }
    }
}