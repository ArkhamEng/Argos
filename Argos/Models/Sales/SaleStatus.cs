using Argos.Models.BaseTypes;
using Argos.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Sales
{
    [Table("Status", Schema = "Sales")]
    public class SaleStatus : ISelectable
    {
        public StatusSale SaleStatusId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        #region Navigation Properties

        public ICollection<Sale> Sales { get; set; }
        #endregion

        #region ISelectable Implementation
        public string Value
        {
            get { return ((int)SaleStatusId).ToString(); }
        }

        public string Text
        {
            get { return this.Name; }
        }
        #endregion
    }
}