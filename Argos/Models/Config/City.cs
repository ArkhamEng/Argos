using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
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

        public int StateId { get; set; }

        [MaxLength(150)]
        public string  Name { get; set; }

        public virtual State State { get; set; }

        public ICollection<Locality> Localities { get; set; }

        public ICollection<Client> Clients { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Supplier> Suppliers { get; set; }

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