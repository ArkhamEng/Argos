using Argos.Models.BaseTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Models.Enums;

namespace Argos.Models.Operative
{
    [Table("OperationType", Schema = "Operative")]
    public class OperationType:ISelectable
    {
        public OpertionType OperationTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #region Navigation Properties
        public ICollection<Sale> Sales { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        #endregion

        #region Not Mapped Properties

      
        public string Value
        {
            get
            {
                return this.OperationTypeId.ToString();
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