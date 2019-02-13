﻿using Argos.Common.Enums;
using Argos.Models.BaseTypes;
using Argos.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Production
{
    [Table("AccountStatus", Schema = "Production")]
    public class AccountStatus: ActivableAudit, ISelectable
    {
        public StatusAccount AccountStatusId { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public string Value
        {
            get
            {
                return ((int)this.AccountStatusId).ToString();
            }
        }

        public string Text
        {
            get
            {
                return this.Name;
            }
        }
    }
}
