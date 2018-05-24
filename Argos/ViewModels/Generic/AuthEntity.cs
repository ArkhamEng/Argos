using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.ViewModels.Generic
{
    public abstract class AuthEntity
    {
        protected AuditableCatalog Catalog;

        public virtual bool ActionsAllowed
        {
            get
            {
                return (HttpContext.Current.User.IsInRole("Capturista") ||
                        HttpContext.Current.User.IsInRole("Supervisor"));
            }
        }

        public virtual bool CanEdit
        {
            get
            {
                return (HttpContext.Current.User.IsInRole("Capturista") &&
                        (this.Catalog != null && this.Catalog.IsActive));
            }
        }

        public virtual bool CanDelete
        {
            get
            {
                return (HttpContext.Current.User.IsInRole("Capturista") &&
                        (this.Catalog != null && this.Catalog.IsActive));
            }
        }

        public virtual bool CanActivate
        {
            get
            {
                return (HttpContext.Current.User.IsInRole("Supervisor") &&
                        (this.Catalog != null && !this.Catalog.IsActive));
            }
        }

    }
}