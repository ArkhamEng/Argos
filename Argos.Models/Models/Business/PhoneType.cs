using Argos.Models.BaseTypes;
using Argos.Common.Enums;
using Argos.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Business
{
    [Table("PhoneType", Schema = "Business")]
    public class PhoneType:ISelectable
    {
        public PhoneTypes PhoneTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }


        public string Value { get { return ((int)this.PhoneTypeId).ToString(); } }


        public string Text { get { return this.Name; } }
    }
}