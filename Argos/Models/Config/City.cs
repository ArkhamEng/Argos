using Argos.Models.BaseTypes;
using Argos.Models.BusinessEntity;
using Argos.Models.HumanResources;
using Argos.Models.Operative;
using Argos.Models.Production;
using Argos.Models.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("City", Schema = "Config")]
    public class City:ISelectable
    {
        public int CityId { get; set; }

        [Display(Name="Estado/Entidad")]
        public int StateId { get; set; }

        [MaxLength(30)]
        public string Code { get; set; }

        [MaxLength(150)]
        public string  Name { get; set; }

        public virtual State State { get; set; }

        public ICollection<Locality> Localities { get; set; }

        public ICollection<Person> Persons { get; set; }

        public ICollection<AccountLocation> Locations { get; set; }

        #region Not Mapped
        [NotMapped]
        public string Value
        {
            get
            {
                return CityId.ToString();
            }
        }

        [NotMapped]
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