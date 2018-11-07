using Argos.Models.BaseTypes;
using Argos.Models.Interfaces;
using Argos.Models.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    /// <summary>
    /// Armadoras, aplica para la configuración de autopartes
    /// </summary>
    [Table("Maker", Schema = "Config")]
    public class Maker:AuditableEntity,ISelectable
    {
        public int MakerId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }

        #region Not Mapped

        public string Value
        {
            get
            {
                return this.MakerId.ToString();
            }
        }

        public string Text
        {
            get
            {
                return this.Name;
            }
        }
        #endregion
    }

}