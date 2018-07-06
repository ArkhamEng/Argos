using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    public enum StatusCredit:int
    {
        Canceled    = -2,
        Suspended   = -1,
        Unasigned   = 1,
        Active      = 2

    }
}