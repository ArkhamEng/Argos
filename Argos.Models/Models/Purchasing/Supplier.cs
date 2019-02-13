
using Argos.Models.Business;
using Argos.Common.Enums;
using Argos.Models.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Models.Config;

namespace Argos.Models.Purchasing
{
    [Table("Supplier", Schema = "Purchasing")]
    public class Supplier:Person
    {
        [Display(Name = "Razón Social")]
        [MaxLength(200)]
        public string BusinessName { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(200)]
        public string WebSite { get; set; }

        [Display(Name = "Estado del crédito")]
        [ForeignKey("CreditStatus")]
        public StatusCredit Status { get; set; }

        [Display(Name = "Límite de credito")]
        public double CreditLimit { get; set; }

        [Display(Name = "Días de credito")]
        public int CreditDays { get; set; }

        [Display(Name = "Saldo")]
        public double CreditBalance { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        public virtual CreditStatus CreditStatus { get; set; }
    }
}