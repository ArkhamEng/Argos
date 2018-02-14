using Argos.Models.BaseTypes;
using Argos.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    public class CommissionStatus:Status
    {
        public int CommissionStatusId { get; set; }

        public override int Id
        {
            get
            {
                return CommissionStatusId;
            }
        }

        public ICollection<Commission> Commissions { get; set; }
    }
}