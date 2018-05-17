using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class AuditableCatalog:AuditableEntity
    {
        public bool IsActive { get; set; }

        public DateTime? LockEndDate { get; set; }

        [MaxLength(30)]
        public string LockUser { get; set; }

        public virtual bool IsLocked
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

        public void UnLock()
        {
            this.LockEndDate = null;
            this.LockUser = null;
        }
    }
}