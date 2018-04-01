using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class AuditableEntity
    {
        public bool IsActive { get; set; }

        [Required]
        public DateTime InsDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string InsUser { get; set; }

        [Required]
        public DateTime? UpdDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string UpdUser { get; set; }

        public DateTime? LockEndDate { get; set; }

        [MaxLength(30)]
        public string LockUser { get; set; }

        [NotMapped]
        public bool IsLocked
        {
            get
            {
                if (LockEndDate != null)
                {
                    if (LockEndDate.Value >= DateTime.Now.ToLocal() && LockUser != HttpContext.Current.User.Identity.Name)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public AuditableEntity()
        {
            this.IsActive = true;
            this.InsDate = DateTime.Now.ToLocal();
            this.UpdDate = DateTime.Now.ToLocal();
            this.UpdUser = HttpContext.Current.User.Identity.Name;
            this.InsUser = HttpContext.Current.User.Identity.Name;
        }
    }
}