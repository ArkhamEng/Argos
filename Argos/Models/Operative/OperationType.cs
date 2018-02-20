using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("OperationType", Schema = "Operative")]
    public class OperationType
    {
        public int OperationTypeId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        #region Navigation Properties
        public ICollection<Operation> Operation { get; set; }
        #endregion
    }
}