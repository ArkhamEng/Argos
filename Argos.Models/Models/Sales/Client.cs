using Argos.Models.Business;
using Argos.Models.Config;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Argos.Models.Production;

namespace Argos.Models.Sales
{
    [Table("Client", Schema = "Sales")]
    public class Client:Person
    {
        [Display(Name = "Razón Social")]
        [MaxLength(200)]
        public string BusinessName { get; set; }


        [Display(Name = "Estado del crédito")]
        public StatusCredit CreditStatusId { get; set; }

     
        [Display(Name = "Límite de credito")]
        public double CreditLimit { get; set; }

        [Display(Name = "Días de credito")]
        public int CreditDays { get; set; }

        [Display(Name = "Saldo")]
        public double CreditBalance { get; set; }

        public virtual CreditStatus CreditStatus { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}