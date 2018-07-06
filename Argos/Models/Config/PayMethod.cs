﻿using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    public class PayMethod:ISelectable
    {
        public PayMethods PayMethodId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }


        public string Value { get { return ((int)this.PayMethodId).ToString(); } }


        public string Text { get { return this.Name; } }
    }
}