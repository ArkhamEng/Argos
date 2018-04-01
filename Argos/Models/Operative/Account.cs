using Argos.Models.BaseTypes;
using Argos.Models.Config;
using Argos.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Operative
{
    [Table("Account", Schema = "Operative")]
    public class Account : AuditableEntity
    {
        public int AccountId { get; set; }

        [Display(Name ="Tipo de cuenta")]
        public int AccountTypeId { get; set; }

        [Display(Name ="Cliente")]
        public int ClientId { get; set; }

        [Display(Name ="Status")]
        public int StatusId { get; set; }

        [MaxLength(12)]
        public string Code { get; set; }

        [Display(Name = "Contratación")]
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Precio contrato")]
        [DataType(DataType.Currency)]
        public double HirePrice { get; set; }


        #region Navigation Properties
        public virtual Client Client { get; set; }

        public AccountType AccountType { get; set; }

        public virtual OperativeStatus Status { get; set; }

        public virtual AccountLocation Location { get; set; }

        public virtual Policy Policy { get; set; }

        public ICollection<AccountFile> AccountFile { get; set; }

        public ICollection<Service> Services { get; set; }

   
        #endregion
    }
}