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
        public SelectList ServiceTypes { get; set; }

        public SelectList ServiceStatus { get; set; }

        public ICollection<ServiceAccount> Accounts { get; set; }

        public AccountSearchViewModel()
        {
            this.ServiceTypes = new List<ServiceType>().ToSelectList();
            this.ServiceStatus = new List<ServiceStatus>().ToSelectList();
            this.Accounts       = new List<ServiceAccount>();
        }

    }
}