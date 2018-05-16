using Argos.Models.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.ViewModels.Operative
{
    [NotMapped]
    public class AccountViewModel
    {
        public SelectList AccountTypes { get; set; }

        public SelectList AccountStatus { get; set; }

        public Account Account { get; set; }

        public PolicyViewModel PolicyViewModel { get; set; }

        public LocationViewModel LocationViewModel { get; set; }

        public AccountViewModel(Account account)
        {
            this.Account = account;
            this.LocationViewModel = new LocationViewModel(account.Location);
            this.PolicyViewModel   = new PolicyViewModel(account.Policy);
        }
    }

    [NotMapped]
    public class PolicyViewModel
    {
        public SelectList PolicyStatus { get; set; }

        public Policy Policy { get; set; }

        public PolicyViewModel(Policy policy)
        {
            this.Policy = policy;
        }
    }

    [NotMapped]
    public class LocationViewModel
    {
        public SelectList States { get; set; }

        public SelectList Cities { get; set; }

        public AccountLocation Location { get; set; }

        public LocationViewModel(AccountLocation location)
        {
            this.Location = location;
        }
    }
}