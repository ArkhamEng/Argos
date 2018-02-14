using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class AuditableEntity
    {
        [Required]
        public DateTime InsDate { get; set; }

        [Required]
        public DateTime UpdDate { get; set; }

        [Required]
        public string InsUser { get; set; }

        [Required]
        public string UpdUser { get; set; }

        public AuditableEntity()
        {
            this.InsDate = DateTime.Now.ToLocal();
            this.UpdDate = DateTime.Now.ToLocal();
            this.UpdUser = HttpContext.Current.User.Identity.Name;
            this.InsUser = HttpContext.Current.User.Identity.Name;
        }
    }
}