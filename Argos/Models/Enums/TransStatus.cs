using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    public enum TransStatus:int
    {
        Canceled   = -1,
        InProcess  = 1,
        Registered = 2,
        Payed      = 4,
        Authorized = 5
    }
}