
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Common.Enums
{
    public enum StatusCredit:int
    {
        Canceled    = -2,
        Suspended   = -1,
        Unasigned   = 1,
        Active      = 2

    }
}