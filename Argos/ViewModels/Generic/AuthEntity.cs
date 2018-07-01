using Argos.Models.BaseTypes;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.ViewModels.Generic
{
    public abstract class AuthEntity
    {
        protected AuditableCatalog Catalog;


        public virtual string EditButton
        {
            get
            {
                if (true || (HttpContext.Current.User.IsInRole("Capturista") && (this.Catalog != null && this.Catalog.IsActive)))
                    return Styles.btnWarning;
                else
                    return Styles.btnWarningDisable;
            }
        }

        public virtual string DeleteButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Capturista") && (this.Catalog != null && this.Catalog.IsActive)))
                    return Styles.btnDanger;
                else
                    return Styles.btnDangerDisable;
            }
        }


        public virtual string ActivateButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Supervisor") && (this.Catalog != null && !this.Catalog.IsActive)))
                    return Styles.btnSuccess;
                else
                    return Styles.btnDangerHidden;
            }
        }


    }
}