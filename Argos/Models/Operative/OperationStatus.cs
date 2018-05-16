using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Status", Schema = "Operative")]
    public class OperationStatus:ISelectable
    {
        [Key]
        public OpStatus StatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; }

        #region Navigation Properties

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        #endregion

        #region Not Mapped Properties

        [NotMapped]
        public string Value
        {
            get
            {
                return this.StatusId.ToString();
            }
        }

        [NotMapped]
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