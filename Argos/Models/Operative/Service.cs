using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Service", Schema = "Operative")]
    public class Service : AuditableEntity
    {
        public int ServiceId { get; set; }

        [Display(Name ="Status")]
        public int StatusId { get; set; }

      
        [Display(Name = "Contratación")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double HirePrice { get; set; }


        #region Navigation Properties
     
        public virtual Status Status { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<ServiceAttachment> ServiceAttachment { get; set; }

        public ICollection<Operation> Operation { get; set; }
        #endregion
    }
}