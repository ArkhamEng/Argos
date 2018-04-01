using Argos.Models.HumanResources;
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
        public SelectList AccountTypes { get; set; }

        public SelectList Status { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public AccountSearchViewModel()
        {
            this.AccountTypes   = new List<AccountType>().ToSelectList();
            this.Status         = new List<OperativeStatus>().ToSelectList();
            this.Accounts       = new List<Account>();
        }

    }
}