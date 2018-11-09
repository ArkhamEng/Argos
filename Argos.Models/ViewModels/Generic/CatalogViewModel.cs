using Argos.Models.BaseTypes;
using Argos.Common;
using System.Web;
using Argos.Common.Constants;

namespace Argos.ViewModels.Generic
{
    public abstract class CatalogViewModel<T> where T : AuditableCatalog
    {
        protected AuditableCatalog catalog;

        public virtual bool EditDisabled
        {
            get
            {
                if (true || (HttpContext.Current.User.IsInRole("Capturista") && (this.catalog != null && this.catalog.IsActive)))
                    return false;
                else
                    return true;
            }
        }

        public virtual bool DeleteDisabled
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Capturista") && (this.catalog != null && this.catalog.IsActive)))
                    return false;
                else
                    return true;
            }
        }


        public virtual bool ActivateDisabled
        {
            get
            {
                if ((HttpContext.Current.User.IsInRole("Supervisor") && (this.catalog != null && !this.catalog.IsActive)))
                    return false;
                else
                    return true;
            }
        }

        public virtual string RowState
        {
            get
            {
                if (!this.catalog.IsActive)
                    return Responses.Danger;

                return string.Empty;
            }
        }


    }
}