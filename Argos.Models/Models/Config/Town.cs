﻿using Argos.Models.Business;
using Argos.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("Town", Schema = "Config")]
    public class Town:ISelectable
    {
        [MaxLength(6)]
        public string TownId { get; set; }

        [Display(Name="Estado/Entidad")]
        [MaxLength(2)]
        public string StateId { get; set; }


        [MaxLength(150)]
        public string  Name { get; set; }

        public ICollection<Settlement> Settlements { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public virtual State State { get; set; }

        #region Not Mapped
        public string Value
        {
            get
            {
                return TownId;
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