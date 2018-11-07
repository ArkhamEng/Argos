using Argos.Models.BaseTypes;
using Argos.Common;
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
                    return Styles.Buttons.Warning;
                else
                    return Styles.Buttons.Hidden.Warning;
            }
        }

        public virtual string DeleteButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Capturista") && (this.Catalog != null && this.Catalog.IsActive)))
                    return Styles.Buttons.Danger;
                else
                    return Styles.Buttons.Hidden.Danger;
            }
        }


        public virtual string ActivateButton
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Supervisor") && (this.Catalog != null && !this.Catalog.IsActive)))
                    return Styles.Buttons.Success;
                else
                    return Styles.Buttons.Disabled.Success;
            }
        }


    }
}