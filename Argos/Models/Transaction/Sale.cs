using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Transaction
{
    [Table("Sale", Schema = "Transaction")]
    public class Sale: BaseTypes.Transaction
    {
        [Column(Order = 0)]
        public int SaleId { get; set; }

        [Column(Order = 1)]
        public int ClientId { get; set; }

        [Column(Order = 2)]
        public int EmployeeId { get; set; }

        [Column(Order = 3)]
        [MaxLength(10)]
        public string SaleCode { get; set; }

        [Column(Order = 4)]
        [Display(Name = "Fecha de venta")]
        public DateTime SaleDate { get; set; }

        [Display(Name ="Requiere envío")]
        public bool ForShipping { get; set; }

        #region Navigation Properties
        public virtual Employee Employee { get; set; }

        public virtual Client Client { get; set; }

        public virtual Commission Commission { get; set; }

        public  ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<Shipping> Shippings { get; set; }

        #endregion

    }
}