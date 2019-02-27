using Argos.Common.Enums;
using Argos.Models.BaseTypes;
using Argos.Models.Business;
using Argos.Models.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Argos.Models.Production
{
    [Table("Account", Schema = "Production")]
    public class Account: ActivableAudit
    {
        public int AccountId { get; set; }

        [Display(Name ="Cliente")]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Display(Name = "Estatus")]
        [ForeignKey("AccountStatus")]
        public StatusAccount AccountStatusId { get; set; }

        [Display(Name = "Tipo de cuenta")]
        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }

        [Display(Name = "Clave de cuenta")]
        [MaxLength(12)]
        [Index("IDX_Code", IsUnique = false)]
        public string Code { get; set; }

        
        [Display(Name = "Costo de la póliza")]
        [DataType(DataType.Currency)]
        public double PolicyCost { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(100)]
        public string Comment { get; set; }

        [Display(Name = "Inicio Contrato")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Fin Contrato")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Ultimo Corte")]
        [Index("IDX_CutOffDate", IsUnique =false)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CutOffDate { get; set; }

        [Display(Name ="Periodo Mantto")]
        public int MaintPeriodId { get; set; }

        [Display(Name = "Periodo Pago")]
        public int PaymentPeriodId { get; set; }

        [Display(Name = "Con póliza")]
        public bool HasPolicy { get; set; }

        [Display(Name = "Ultimo Pago")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastPaymentDate { get; set; }

        [Display(Name = "Ultimo Mantenimiento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastMaintenanceDate { get; set; }

        [Display(Name = "Con mantenimiento")]
        public bool HasMaintenance { get; set; }

       
        public int Sequential { get; set; }


        public virtual Client Client { get; set; }

        public virtual AccountType AccountType { get; set; }
                
        public virtual AccountStatus AccountStatus { get; set; }

        
        public virtual MaintPeriod MaintPeriod { get; set; }

        
        public virtual PaymentPeriod PaymentPeriod { get; set; }

        public ICollection<Occurence> Occurences { get; set; }

        public ICollection<AccountHistory> AccountHistories { get; set; }

        public IEnumerable<Service> Services { get { return this.Occurences.OfType<Service>(); } }

        public IEnumerable<Charge> Charges { get { return this.Occurences.OfType<Charge>(); } }
    }
}
