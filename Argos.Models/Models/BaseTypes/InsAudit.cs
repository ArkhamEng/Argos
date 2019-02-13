using Argos.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class InsAudit
    {
        [Required]
        [Display(Name = "Fecha Registro")]
        public DateTime InsDate { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Registrrado por")]
        public string InsUser { get; set; }

        public InsAudit()
        {
            this.InsDate = DateTime.Now.ToLocal();

            try
            {
                this.InsUser = HttpContext.Current.User.Identity.Name;
            }
            catch (Exception)
            {
                this.InsUser = "System";
            }
        }
     
    }
}