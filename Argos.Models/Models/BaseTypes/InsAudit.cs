using Argos.Common;
using System;
using System.ComponentModel.DataAnnotations;
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