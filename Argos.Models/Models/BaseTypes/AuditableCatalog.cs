using System;
using System.ComponentModel.DataAnnotations;

namespace Argos.Models.BaseTypes
{
    public abstract class AuditableCatalog : AuditableEntity
    {
        public bool IsActive { get; set; }

        public DateTime? LockEndDate { get; set; }

        [MaxLength(30)]
        public string LockUser { get; set; }

        public bool IsLocked
        {
            get
            {
                return (LockEndDate != null && LockEndDate > DateTime.Now.ToLocal()) ?
                        !this.LockUser.Equals(System.Web.HttpContext.Current.User.Identity.Name) : false;
            }
        }

        public AuditableCatalog()
        {
            this.IsActive = true;
        }
    }
}