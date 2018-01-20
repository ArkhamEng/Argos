using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("OperationType", Schema = "Operative")]
    public class OperationType
    {
        public int OperationTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #region Navigation Properties
        public ICollection<Operation> Operation { get; set; }
        #endregion
    }
}