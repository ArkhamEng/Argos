using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Production
{
    [Table("AccountHistory", Schema = "Production")]
    public class AccountHistory:InsAudit
    {
        public int AccountHistoryId { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [MaxLength(30)]
        [Display(Name = "Estatus")]
        public string Status { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Costo Póliza")]
        public double PolicyCost { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(100)]
        public string Comment { get; set; }

        [Display(Name = "Periodo Mantto")]
        public int MaintenancePeriod { get; set; }

        [Display(Name = "Periodo Pago")]
        public int PaymentPeriod { get; set; }

        [Display(Name = "Con Poliza")]
        public bool HasPolicy { get; set; }

        [Display(Name = "Con Mantenimiento")]
        public bool HasMaintenance { get; set; }

        public virtual Account Account { get; set; }

    }
}
