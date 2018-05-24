using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("State", Schema = "Config")]
    public class State : ISelectable
    {
        [MaxLength(2)]
        public string StateId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public ICollection<Town> Towns { get; set; }

        #region ISelectable
        public string Value
        {
            get
            {
                return StateId;
            }
        }

        public string Text
        {
            get
            {
                return Name;
            }
        }
        #endregion
    }
}