using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Config
{
    [Table("City", Schema = "Config")]
    public class City : ISelectable
    {
        public int CityId { get; set; }

        [Display(Name = "Estado")]
        [Index("IDX_StateId", IsUnique = false)]
        public int StateId { get; set; }

        [MaxLength(15)]
        public string Code { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        #region Navigation Properties
        public virtual State State { get; set; }
        #endregion

        [NotMapped]
        public int Id { get { return this.CityId; } }

    }
}