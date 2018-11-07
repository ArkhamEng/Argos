using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using Argos.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

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