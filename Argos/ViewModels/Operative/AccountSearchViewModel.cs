using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Operative;
using Argos.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.ViewModels.Operative
{
    public class AccountSearchViewModel
    {
        public SelectList ServiceCategories { get; set; }

        public SelectList ServiceStatus { get; set; }

        public ICollection<ServiceAccount> Accounts { get; set; }

        public AccountSearchViewModel()
        {
            this.ServiceCategories = new List<ServiceCategory>().ToSelectList();
            this.ServiceStatus = new List<ServiceCategory>().ToSelectList();
            this.Accounts       = new List<ServiceAccount>();
        }

    }
}