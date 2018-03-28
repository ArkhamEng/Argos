using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("State", Schema = "Config")]
    public class State : ISelectable
    {
        public int StateId { get; set; }

        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        #region Not Mapped
        public string Value
        {
            get
            {
                return StateId.ToString();
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