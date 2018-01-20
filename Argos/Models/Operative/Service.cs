using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Finances;
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

        [ForeignKey("ParentService")]
        public int? ParentServiceId { get; set; }

        public int ServiceTypeId { get; set; }

        public int ServiceStatusId { get; set; }

        public int ClientId { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [Display(Name = "Fecha de Contratación")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public double HirePrice { get; set; }

        #region Navigation Properties
        public virtual  ServiceType ServiceType { get; set; }

        public virtual Client Client { get; set; }

        public virtual ServiceStatus ServiceStatus { get; set; }

        public virtual Service ParentService { get; set; }

        public virtual ServiceAddress ServiceAddress { get; set; }

        public virtual PolicyDetail PolicyDetail { get; set; }

        public virtual ICollection<Service> ChildServices { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<ServiceAttachment> ServiceAttachment { get; set; }

        public ICollection<Operation> Operation { get; set; }
        #endregion
    }
}