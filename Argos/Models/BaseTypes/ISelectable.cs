using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public interface ISelectable
    {
        string Value { get; }
        
        string Text { get; }
    }


    public class Selectable : ISelectable
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }
}