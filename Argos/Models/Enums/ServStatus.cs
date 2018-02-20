using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.Enums
{
    public enum ServStatus:int
    {
        [Display(Name="Cancelado")]
        Canceled   = -1,

        [Display(Name = "Capturado")]
        Opened    = 1,

        [Display(Name = "Autorizado")]
        Authorized = 2,

        [Display(Name = "Activo")]
        Active     = 3,
    }
}