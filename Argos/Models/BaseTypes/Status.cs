using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class Status:ISelectable
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual int Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}