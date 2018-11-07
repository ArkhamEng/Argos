﻿using Argos.Models.Business;
using Argos.Models.Config;
using Argos.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Sales
{
    [Table("Client", Schema = "Sales")]
    public class Client:Person
    {
        [Display(Name = "Razón Social")]
        [MaxLength(200)]
        [Index("Unq_BusinessName", IsUnique = true)]
        public string BusinessName { get; set; }

        //[Display(Name = "Nombre de la empresa")]
        //[MaxLength(200)]
        //public string Company { get; set; }

        [Display(Name = "Estado del crédito")]
        public StatusCredit CreditStatusId { get; set; }

     
        [Display(Name = "Límite de credito")]
        public double CreditLimit { get; set; }

        [Display(Name = "Días de credito")]
        public int CreditDays { get; set; }

        [Display(Name = "Saldo")]
        public double CreditBalance { get; set; }

        public virtual CreditStatus CreditStatus { get; set; }
    }
}